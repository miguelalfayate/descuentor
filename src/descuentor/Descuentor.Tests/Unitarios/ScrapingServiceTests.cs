using Moq;
using Descuentor.Dominio.Interfaces;
using Descuentor.Infraestructura.Servicios;

namespace Descuentor.Tests.Unitarios
{
    public class ScrapingServiceTests
    {
        [Fact]
        public async Task ObtenerPrecio_DebeRetornarPrecioCorrecto()
        {
            // Ordenar
            var mockFactory = new Mock<ITiendaOnlineFactory>();
            mockFactory.Setup(f => f.ObtenerSelectorPrecio(It.IsAny<string>()))
                .Returns("span[class*='ItemDetailPrice']");
            var scrapingService = new ScrapingService(mockFactory.Object);

            // Actuar
            var resultado = await scrapingService.ObtenerPrecios(
                new Dictionary<int, string> { { 1, "https://es.wallapop.com/item/apple-watch-ultraregalo-3-correas-apple-1087651499" } });

            // Assert
            Assert.True(resultado.ContainsKey(1));
            Assert.True(resultado[1] > 0);
        }
    }
}