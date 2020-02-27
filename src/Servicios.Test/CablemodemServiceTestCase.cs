using Entidades;
using FluentAssertions;
using Infraestructura;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Servicios.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Servicios.Test
{
    [TestClass]
    public class CablemodemServiceTestCase
    {
        public Mock<ILogger<CablemodemService>> MockLogger { get; private set; }
        public Mock<ICablemodemRepository> MockCablemodemRepository { get; private set; }
        public Mock<IModeloRepository> MockModeloRepository { get; private set; }
        public CablemodemService CablemodemService { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.MockLogger = new Mock<ILogger<CablemodemService>>();
            this.MockCablemodemRepository = new Mock<ICablemodemRepository>();
            this.MockModeloRepository = new Mock<IModeloRepository>();
            this.CablemodemService = new CablemodemService(this.MockCablemodemRepository.Object, this.MockModeloRepository.Object, this.MockLogger.Object);
        }

        [TestMethod]
        public void SinCablemodems_ExistenCablemodemsParaFabricante_DevuelveFalso()
        {
            var fabricante = "Cisco";
            this.MockCablemodemRepository.Setup(mock => mock.Any(It.IsAny<Expression<Func<Cablemodem, bool>>>())).Returns(false);
            this.CablemodemService.PoseeCablemodemsDelFabricante(fabricante).Should().BeFalse();
        }

        [TestMethod]
        public void CablemodemsDelFabricante_ExistenCablemodemsParaFabricante_DevuelveVerdadero()
        {
            var fabricante = "Cisco";
            this.MockCablemodemRepository.Setup(mock => mock.Any(It.IsAny<Expression<Func<Cablemodem, bool>>>())).Returns(true);
            this.CablemodemService.PoseeCablemodemsDelFabricante(fabricante).Should().BeTrue();
        }

        [TestMethod]
        public void CablemodemsDelFabricanteSinModelosRegistrados_GetNoVerificados_DevuelveCablemodem()
        {
            var fabricante = "Moto";
            var cablemodems = CreateCablemodems(fabricante);
            this.MockCablemodemRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Cablemodem, bool>>>())).Returns(cablemodems);
            this.MockModeloRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Modelo, bool>>>())).Returns(new List<Modelo>());
            IEnumerable<Cablemodem> enumerable = this.CablemodemService.GetNoVerificados(fabricante);
            enumerable.Should().HaveCount(1);
            enumerable.Should().Contain(cablemodems.First());
        }

        [TestMethod]
        public void CablemodemsDelFabricanteSinRegistrarMismaVersionSoftware_GetNoVerificados_DevuelveCablemodem()
        {
            var fabricante = "Moto";
            var cablemodems = CreateCablemodems(fabricante);
            this.MockCablemodemRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Cablemodem, bool>>>())).Returns(cablemodems);
            this.MockModeloRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Modelo, bool>>>())).Returns(new List<Modelo>() { new Modelo(cablemodems.First().Fabricante, cablemodems.First().Modelo, "0.1") });
            IEnumerable<Cablemodem> enumerable = this.CablemodemService.GetNoVerificados(fabricante);
            enumerable.Should().HaveCount(1);
            enumerable.Should().Contain(cablemodems.First());
        }

        [TestMethod]
        public void CablemodemsDelFabricanteRegistrar_GetNoVerificados_DevuelveCablemodem()
        {
            var fabricante = "Moto";
            var cablemodems = CreateCablemodems(fabricante);
            this.MockCablemodemRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Cablemodem, bool>>>())).Returns(cablemodems);
            this.MockModeloRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Modelo, bool>>>())).Returns(new List<Modelo>() { new Modelo(cablemodems.First().Fabricante, cablemodems.First().Modelo, cablemodems.First().VersionSoftware) });
            IEnumerable<Cablemodem> enumerable = this.CablemodemService.GetNoVerificados(fabricante);
            enumerable.Should().HaveCount(0);
        }

        [TestMethod]
        public void CablemodemsDelFabricanteRegistrados_GetVerificados_DevuelveCablemodem()
        {
            var fabricante = "Moto";
            var cablemodems = CreateCablemodems(fabricante);
            this.MockCablemodemRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Cablemodem, bool>>>())).Returns(cablemodems);
            this.MockModeloRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Modelo, bool>>>())).Returns(new List<Modelo> { new Modelo(cablemodems.First().Fabricante, cablemodems.First().Modelo, cablemodems.First().VersionSoftware) });
            IEnumerable<Cablemodem> enumerable = this.CablemodemService.GetVerificados(fabricante);
            enumerable.Should().HaveCount(1);
            enumerable.Should().Contain(cablemodems.First());
        }

        [TestMethod]
        public void VariosCablemodemsDelFabricanteRegistrados_GetVerificados_DevuelveCablemodem()
        {
            var fabricante = "Cisco";
            var cablemodems = CreateCablemodems(fabricante);
            var cablemodem = new Cablemodem("91:75:1a:ec:9a:c8", "1.2.3.5")
            {
                Fabricante = fabricante,
                Modelo = "otroModelo",
                VersionSoftware = "1.1.1"
            };
            this.MockCablemodemRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Cablemodem, bool>>>())).Returns(cablemodems);
            this.MockModeloRepository.Setup(mock => mock.Search(It.IsAny<Expression<Func<Modelo, bool>>>())).Returns(new List<Modelo> { new Modelo(cablemodems.First().Fabricante, cablemodems.First().Modelo, cablemodems.First().VersionSoftware) });
            IEnumerable<Cablemodem> enumerable = this.CablemodemService.GetVerificados(fabricante);
            enumerable.Should().HaveCount(1);
            enumerable.Should().Contain(cablemodems.First());
        }

        public IEnumerable<Cablemodem> CreateCablemodems(string fabricante = "Moto")
        {
            var cablemodems = new List<Cablemodem>();
            var cablemodem = new Cablemodem("91:75:1a:ec:9a:c7", "1.2.3.4")
            {
                Fabricante = fabricante,
                Modelo = "modelo",
                VersionSoftware = "1.1.1"
            };

            cablemodems.Add(cablemodem);
            return cablemodems;
        }
    }
}
