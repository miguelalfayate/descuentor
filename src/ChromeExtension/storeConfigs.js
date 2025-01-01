// Configuración de selectores para diferentes tiendas
const storeConfigs = {
    // Amazon
    'amazon.es': {
      title: '#productTitle',
      price: '#priceblock_ourprice, .a-price .a-offscreen',
      description: '#productDescription p',
      image: '#landingImage'
    },
    // eBay
    'ebay.com': {
      title: 'h1.x-item-title__mainTitle',
      price: '.x-price-primary .x-price-display__price',
      description: '.x-item-description',
      image: '.ux-image-carousel-item img'
    },
    // AliExpress
    'aliexpress.com': {
      title: '.product-title-text',
      price: '.product-price-value',
      description: '.product-description',
      image: '.magnifier-image'
    },
    // El Corte Inglés
    'elcorteingles.es': {
      title: '.product-title h1',
      price: '.price-current',
      description: '.product-description',
      image: '.js-zoom-primary_image'
    }
  };
  
  // Función para obtener la configuración basada en el dominio
  function getConfigForDomain(url) {
    const domain = new URL(url).hostname.toLowerCase();
    
    // Buscar configuración exacta
    if (storeConfigs[domain]) {
      return storeConfigs[domain];
    }
    
    // Buscar configuración por dominio parcial
    return Object.entries(storeConfigs).find(([storeDomain]) => 
      domain.includes(storeDomain)
    )?.[1];
  }