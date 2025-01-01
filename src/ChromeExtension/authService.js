// Gestión de autenticación y tokens
class AuthService {
    constructor() {
      this.API_URL = 'http://localhost:5095/identity';
      this.TOKEN_REFRESH_INTERVAL = 5 * 60 * 1000; // 5 minutos
    }
  
    // Iniciar sesión
    async login(email, password) {
      try {
        const response = await fetch(`${this.API_URL}/login`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ email, password })
        });
  
        if (!response.ok) {
          throw new Error('Error de autenticación');
        }
  
        const { accessToken, refreshToken } = await response.json();
        
        // Guardar tokens
        await this.saveTokens(accessToken, refreshToken);
        
        // Iniciar el proceso de refresh automático
        this.startTokenRefresh();
        
        return true;
      } catch (error) {
        console.error('Error en login:', error);
        throw error;
      }
    }
  
    // Guardar tokens en chrome.storage
    async saveTokens(accessToken, refreshToken) {
      await chrome.storage.local.set({
        'accessToken': accessToken,
        'refreshToken': refreshToken,
        'tokenTimestamp': Date.now()
      });
    }
  
    // Obtener access token
    async getAccessToken() {
      const { accessToken } = await chrome.storage.local.get('accessToken');
      return accessToken;
    }
  
    // Refrescar token usando refresh token
    async refreshTokens() {
      try {
        const { refreshToken } = await chrome.storage.local.get('refreshToken');
        if (!refreshToken) throw new Error('No hay refresh token');
  
        const response = await fetch(`${this.API_URL}/refresh`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({ refreshToken })
        });
  
        if (!response.ok) throw new Error('Error al refrescar token');
  
        const { accessToken: newAccessToken, refreshToken: newRefreshToken } = await response.json();
        await this.saveTokens(newAccessToken, newRefreshToken);
  
        return true;
      } catch (error) {
        console.error('Error refreshing token:', error);
        await this.logout();
        throw error;
      }
    }
  
    // Iniciar refresh automático de token
    startTokenRefresh() {
      setInterval(async () => {
        try {
          await this.refreshTokens();
        } catch (error) {
          console.error('Error en refresh automático:', error);
        }
      }, this.TOKEN_REFRESH_INTERVAL);
    }
  
    // Cerrar sesión
    async logout() {
      await chrome.storage.local.remove(['accessToken', 'refreshToken', 'tokenTimestamp']);
    }
  
    // Verificar si el usuario está autenticado
    async isAuthenticated() {
      const { accessToken, tokenTimestamp } = await chrome.storage.local.get(['accessToken', 'tokenTimestamp']);
      if (!accessToken) return false;
  
      // Verificar si el token ha expirado (ejemplo: 1 hora)
      const TOKEN_EXPIRY = 60 * 60 * 1000; // 1 hora
      const isExpired = Date.now() - tokenTimestamp > TOKEN_EXPIRY;
      
      if (isExpired) {
        try {
          await this.refreshTokens();
          return true;
        } catch {
          return false;
        }
      }
  
      return true;
    }
  }
  
  export const authService = new AuthService();