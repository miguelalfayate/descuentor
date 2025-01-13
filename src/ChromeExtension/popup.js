// Esperar a que el DOM esté completamente cargado
document.addEventListener('DOMContentLoaded', async () => {
    // Verificar autenticación al cargar
    async function checkAuth() {
        const isAuthenticated = await authService.isAuthenticated();
        console.log('isAuthenticated');
        if (!isAuthenticated) {
            window.location.href = 'login.html';
            return;
        }
    }

    // Verificar autenticación al iniciar
    await checkAuth();

    const captureBtn = document.getElementById('captureBtn');
    const logoutBtn = document.getElementById('logoutBtn');
    const resultDiv = document.getElementById('result');

    // Verificar el host al cargar la página
    const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });
    const host = new URL(tab.url).hostname.replace('www.', '');
    console.log(host);

    // Verificar si la tienda online es soportada
    // const tiendaActual = tiendasOnline[host];
    // console.log(tiendaActual);

    // if (!tiendaActual) {
    //     captureBtn.disabled = true;
    //     resultDiv.innerHTML = `
    //         <div style="color: red;">
    //             ✗ Tienda online no soportada
    //         </div>`;
    // }
    // else {
    //     captureBtn.disabled = false;
    // }

    // const tiendasAceptadas = Object.values(tiendasOnline).map(tienda => tienda.host);
    //const esTiendaAceptada = Object.values(tiendasOnline).some(tienda => host.includes(tienda.host))
    const esTiendaAceptada = Object.keys(tiendasOnline).some(tienda => host.includes(tienda))

    //const esTiendaAceptada = tiendasAceptadas.includes(host);
    console.log(esTiendaAceptada);

    if (!esTiendaAceptada) {
        captureBtn.disabled = true;
        resultDiv.innerHTML = `
            <div style="color: red;">
                ✗ Tienda online no soportada
            </div>`;
    } else {
        captureBtn.disabled = false;
    }

    // Manejo de captura de producto al hacer clic en el botón
    if (captureBtn) {
        captureBtn.addEventListener('click', async () => {

            //const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });
            try {

                // Obtener token actual
                const accessToken = await authService.getAccessToken();
                if (!accessToken) {
                    throw new Error('No autorizado');
                }

                const datosProducto = await obtenerDatos(host, tab.id);
                console.log(datosProducto.result);


                var precio = datosProducto.result.precioInicial;
                console.log(precio);
                datosProducto.result.precioInicial = parseFloat(datosProducto.result.precioInicial.replace(".", "").replace(",", ".").replace("€", "").trim());

                if (!datosProducto) {
                    throw new Error('No se encontró ningún producto');
                }

                // Llamada a la API
                const response = await fetch('http://localhost:5095/api/productos', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${accessToken}`
                    },
                    body: JSON.stringify(datosProducto.result)
                });

                console.log(response);

                if (response.ok) {

                    resultDiv.innerHTML = `
                    <div style="color: green;">✓ Producto capturado correctamente</div>
                    <div style="margin-top: 10px;">
                        <strong>Título:</strong> ${datosProducto.result.nombre}<br>
                        <strong>Precio:</strong> ${datosProducto.result.precioInicial}<br>
                        <strong>Descripción:</strong> ${datosProducto.result.descripcion}<br>
                        <strong>Imagen:</strong> ${datosProducto.result.urlImagen}<br>
                        <strong>URL:</strong> ${datosProducto.result.url}<br>
                        <strong>Tienda:</strong> ${datosProducto.result.tiendaOnlineId}
                    </div>`;
                } else {
                    throw new Error('Error al guardar el producto');
                }

            } catch (error) {
                if (error.message === 'No autorizado') {
                    window.location.href = 'login.html';
                }
                else {
                    resultDiv.innerHTML = `
              <div style="color: red;">
                ✗ Error: ${error.message}
              </div>`;
                }
            }
        });
    }

    // Manejo del logout
    if (logoutBtn) {
        logoutBtn.addEventListener('click', async () => {
            await authService.logout();
            window.location.href = 'login.html';
        });
    }
});






