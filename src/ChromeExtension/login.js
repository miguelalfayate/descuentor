import { authService } from './authService.js';

document.getElementById('loginForm').addEventListener('submit', async (e) => {
  e.preventDefault();
  
  const email = document.getElementById('email').value;
  const password = document.getElementById('password').value;
  const errorElement = document.getElementById('errorMessage');

  try {
    await authService.login(email, password);
    // Redirigir al popup principal
    window.location.href = 'popup.html';
  } catch (error) {
    errorElement.style.display = 'block';
    errorElement.textContent = 'Error al iniciar sesión. Verifica tus credenciales.';
  }
});