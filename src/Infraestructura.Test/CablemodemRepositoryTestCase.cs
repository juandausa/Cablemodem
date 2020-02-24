using Base.Test;
using Entidades;
using FluentAssertions;
using Infraestructura.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Infraestructura.Test
{
    [TestClass]
    public class CablemodemRepositoryTestCase : BaseRepositoryTestCase<Cablemodem>
    {
        [TestMethod]
        public void NoExisteCablemodem_Save_SeGuardaClave()
        {
            var cablemodemPorPersistir = CreateEntity();
            cablemodemPorPersistir.Ip.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void CablemodemCreado_Search_ObtieneCablemodem()
        {
            var cablemodemPorPersistir = CreateEntity();
            using (var context = new CablemodemContext(Options))
            {
                ICablemodemRepository reporitory = new CablemodemRepository(context);
                var cablemodem = reporitory.Search(c => c.Ip == cablemodemPorPersistir.Ip);
                cablemodem.Should().HaveCount(1);
                this.AreEquals(cablemodemPorPersistir, cablemodem.First());
            };
        }

        [TestMethod]
        public void CablemodemInexistente_Search_NoDevuelveCablemodem()
        {
            using (var context = new CablemodemContext(Options))
            {
                ICablemodemRepository reporitory = new CablemodemRepository(context);
                var cablemodem = reporitory.Search(c => c.Ip == Guid.NewGuid().ToString()).Should().BeEmpty();
            };
        }

        [TestMethod]
        public void CablemodemCreado_ActualizarParametro_ObtenerCablemodem_DevuelveParametroActualizado()
        {
            var cablemodemPorPersistir = CreateEntity();
            using (var context = new CablemodemContext(Options))
            {
                ICablemodemRepository reporitory = new CablemodemRepository(context);
                var cablemodem = reporitory.Search(c => c.Ip == cablemodemPorPersistir.Ip).First();
                cablemodem.Fabricante = "Cisco";
                reporitory.Update(cablemodem);
            };

            using (var context = new CablemodemContext(Options))
            {
                ICablemodemRepository reporitory = new CablemodemRepository(context);
                var cablemodem = reporitory.Search(c => c.Ip == cablemodemPorPersistir.Ip).First();
                cablemodem.Fabricante.Should().Be("Cisco");
            };
        }

        [TestMethod]
        public void CablemodemCreado_Borrar_ObtenerCablemodem_NoDevuelveCablemodem()
        {
            var cablemodemPorPersistir = CreateEntity();
            using (var context = new CablemodemContext(Options))
            {
                ICablemodemRepository reporitory = new CablemodemRepository(context);
                var cablemodem = reporitory.Search(c => c.Ip == cablemodemPorPersistir.Ip).First();
                reporitory.Delete(cablemodem);
            };

            using (var context = new CablemodemContext(Options))
            {
                ICablemodemRepository reporitory = new CablemodemRepository(context);
                var cablemodem = reporitory.Search(c => c.Ip == cablemodemPorPersistir.Ip);
                cablemodem.Should().BeEmpty();
            };
        }

        public override Cablemodem CreateEntity()
        {
            var cablemodem = new Cablemodem()
            {
                Ip = "1.2.3.4",
                MacAddress = "91:75:1a:ec:9a:c7",
                Fabricante = "Moto",
                Modelo = "modelo",
                VersionSoftware = "1.1.1"
            };

            using (var context = new CablemodemContext(Options))
            {
                ICablemodemRepository reporitory = new CablemodemRepository(context);
                reporitory.Save(cablemodem);
            }

            return cablemodem;
        }
    }
}
