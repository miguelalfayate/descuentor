// Importar configuraciones de tiendas
import { getConfigForDomain } from './storeConfigs.js';

// Verificar autenticación al cargar
async function checkAuth() {
    const isAuthenticated = await authService.isAuthenticated();
    if (!isAuthenticated) {
      window.location.href = 'login.html';
      return;
    }
  }
  
  // Verificar autenticación al iniciar
  checkAuth();

document.getElementById('captureBtn').addEventListener('click', async () => {
  const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });

  try {
    // Obtener token actual
    const accessToken = await authService.getAccessToken();
    if (!accessToken) {
      throw new Error('No autorizado');
    }

    // Ejecutar script en la página para extraer información
    const result = await chrome.scripting.executeScript({
      target: { tabId: tab.id },
      files: ['storeConfigs.js'], // Inyectar configuraciones
    });

    const productInfo = await chrome.scripting.executeScript({
      target: { tabId: tab.id },
      function: getProductInfo,
    });

    const data = productInfo[0].result;
    
    if (!data) {
      throw new Error('Tienda no soportada');
    }

    // Actualizar la llamada a la API para incluir el token
    const response = await fetch('http://localhost:5095/api/Productos', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${accessToken}`
        },
        body: JSON.stringify(productInfo)
      });

    if (response.ok) {
      document.getElementById('result').innerHTML = `
        <div style="color: green;">✓ Producto capturado correctamente</div>
        <div style="margin-top: 10px;">
          <strong>Título:</strong> ${data.title}<br>
          <strong>Precio:</strong> ${data.price}<br>
        </div>`;
    } else {
      throw new Error('Error al guardar el producto');
    }

} catch (error) {
    if (error.message === 'No autorizado') {
      window.location.href = 'login.html';
    } else {
      document.getElementById('result').innerHTML = `
        <div style="color: red;">
          ✗ Error: ${error.message}
        </div>`;
    }
  }
});

// Agregar botón de logout
document.getElementById('logoutBtn')?.addEventListener('click', async () => {
    await authService.logout();
    window.location.href = 'login.html';
  });

// Función que se ejecuta en el contexto de la página
function getProductInfo() {
  const url = window.location.href;
  const config = getConfigForDomain(url);
  
  if (!config) {
    return null;
  }

  // Función auxiliar para extraer texto seguro
  const safeQuerySelector = (selector) => {
    try {
      const element = document.querySelector(selector);
      return element ? element.textContent.trim() : '';
    } catch {
      return '';
    }
  };

  // Función para obtener URL de imagen
  const getImageUrl = (selector) => {
    const img = document.querySelector(selector);
    return img ? (img.src || img.getAttribute('data-src') || '') : '';
  };

  return {
    url: url,
    nombre: safeQuerySelector(config.title),
    precioActual: safeQuerySelector(config.price),
    descripcion: safeQuerySelector(config.description),
    urlImagen: getImageUrl(config.image),
    tiendaOnlineId: new URL(url).hostname
  };
}