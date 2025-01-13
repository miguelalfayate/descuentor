// Configuración de selectores para diferentes tiendas

// const tiendasAceptadas = ['amazon.es', 'es.wallapop.com'];

// TODO: Implementar métodos para cada tienda online. Es mejor.

const tiendasOnline = {
    'amazon.es': {
        nombre: '#productTitle',
        //precioActual: 'span[class*="a-price-whole"]',
        precioActual: 'span[class*="a-offscreen"]',
        descripcion: '#productDescription p',
        urlImagen: '#landingImage',
        tiendaOnlineId: 1
    },
    'es.wallapop.com': {
        nombre: 'h1',
        precioActual: 'span[class*="ItemDetailPrice"]',
        descripcion: 'section[class*="description"]',
        urlImagen: '.ItemDetailCarousel__initialBackground img[slot="carousel-content"]',
        tiendaOnlineId: 2
    }
};

async function obtenerDatos(host, idPestana) {

    const tiendaActual = tiendasOnline[host];

    // Ejecutar script en la página para extraer información
    const [resultado] = await chrome.scripting.executeScript({
        target: { tabId: idPestana },
        function: (tiendaActual) => {
            return {
                url: window.location.href,
                nombre: document.querySelector(tiendaActual.nombre)?.innerText,
                precioInicial: document.querySelector(tiendaActual.precioActual)?.innerText + "",
                descripcion: document.querySelector(tiendaActual.descripcion)?.innerText,
                urlImagen: document.querySelector(tiendaActual.urlImagen)?.src,
                tiendaOnlineId: tiendaActual.tiendaOnlineId
            }
        },
        args: [tiendaActual]
    });

    return resultado;

}




// async function obtenerDatosAmazon(idPestana) {

//     // Ejecutar script en la página para extraer información
//     const [resultado] = await chrome.scripting.executeScript({
//         target: { tabId: idPestana },
//         function: () => {
//             return {
//                 url: window.location.href,
//                 nombre: document.querySelector('#productTitle')?.innerText,
//                 precioActual: document.querySelector('#priceblock_ourprice, .a-price .a-offscreen')?.innerText,
//                 descripcion: document.querySelector('#productDescription p')?.innerText,
//                 urlImagen: document.querySelector('#landingImage')?.src,
//                 tiendaOnlineId: 1
//             }
//         }
//     });

//     return resultado;

// }

// async function obtenerDatosWallapop(idPestana) {

//     // Ejecutar script en la página para extraer información
//     const [resultado] = await chrome.scripting.executeScript({
//         target: { tabId: idPestana },
//         function: () => {
//             return {
//                 url: window.location.href,
//                 nombre: document.querySelector('h1')?.innerText,
//                 precioActual: document.querySelector('span[class*="ItemDetailPrice"]')?.innerText,
//                 descripcion: document.querySelector('section[class*="description"]')?.innerText,
//                 urlImagen: document.querySelector('.ItemDetailCarousel__initialBackground img[slot="carousel-content"]')?.src,
//                 tiendaOnlineId: 2
//             }
//         }
//     });

//     return resultado;

// }