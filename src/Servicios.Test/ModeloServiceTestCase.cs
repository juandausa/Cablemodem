using Entidades;
using Infraestructura;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Servicios.Impl;

namespace Servicios.Test
{
    [TestClass]
    public class ModeloServiceTestCase
    {
        public Mock<ILogger<ModeloService>> MockLogger { get; private set; }
        public Mock<IModeloRepository> MockModeloRepository { get; private set; }
        public ModeloService ModeloService { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.MockLogger = new Mock<ILogger<ModeloService>>();
            this.MockModeloRepository = new Mock<IModeloRepository>();
            this.ModeloService = new ModeloService(this.MockModeloRepository.Object, this.MockLogger.Object);
        }

        [TestMethod]
        public void SinModelos_ExistenModelosParaFabricante_DevuelveFalso()
        {
            var modelo = CreateModelo();
            this.MockModeloRepository.Setup(mock => mock.Save(modelo)).Returns(modelo);
            this.ModeloService.AgregarModelo(modelo);
            this.MockModeloRepository.Verify(mock => mock.Save(modelo), Times.Once());
        }

        public Modelo CreateModelo(string fabricante = "Moto")
        {
            return new Modelo("modelo", fabricante, "1.1.1");
        }
    }
}
