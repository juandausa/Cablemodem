using Base.Test;
using Entidades;
using FluentAssertions;
using Infraestructura.Impl;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infraestructura.Test
{
    [TestClass]
    public class ModeloRepositoryTestCase : BaseRepositoryTestCase<Modelo>
    {
        public static ILogger<ModeloRepository> Logger { get; private set; }
        public static IAppSettings AppSettings { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Logger = new Mock<ILogger<ModeloRepository>>().Object;
            var appSettings = new Mock<IAppSettings>();
            appSettings.Setup(mock => mock.JsonStorageFilePath).Returns(Path.Combine("Datos", "emptyModels.json"));
            AppSettings = appSettings.Object;
        }

        [TestMethod]
        public void NoExisteModelo_Save_SeGuardaModelo()
        {
            var modeloPorPersistir = CreateEntity();
            modeloPorPersistir.Should().NotBeNull();
            modeloPorPersistir.Nombre.Should().NotBeNullOrEmpty();
            modeloPorPersistir.Fabricante.Should().NotBeNullOrEmpty();
            modeloPorPersistir.VersionSoftware.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void ModeloCreado_Search_SeGuardaModelo()
        {
            var modeloPorPersistir = CreateEntity();
            using var repository = new ModeloRepository(AppSettings, Logger);
            var modelos = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre);
            modelos.Should().HaveCount(1);
            AreEquals(modeloPorPersistir, modelos.First());
        }

        [TestMethod]
        public void ModeloCreado_ActualizarParametro_SaerchByNombre_DevuelveParametroActualizado()
        {
            var modeloPorPersistir = CreateEntity();
            const string valorActualizado = "v2.0";
            using (var repository = new ModeloRepository(AppSettings, Logger))
            {
                var modeloPorActualizar = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre).First();
                modeloPorActualizar.VersionSoftware = valorActualizado;
                repository.Update(modeloPorActualizar);
            }

            using (var repository = new ModeloRepository(AppSettings, Logger))
            {
                var modeloActualizado = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre).First();
                modeloActualizado.VersionSoftware.Should().Be(valorActualizado);
            }
        }

        [TestMethod]
        public void ModeloCreado_Borrar_SaerchByNombre_NoDevuelveModelo()
        {
            var modeloPorPersistir = CreateEntity();
            using (var repository = new ModeloRepository(AppSettings, Logger))
            {
                var modeloPorRemover = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre).First();
                repository.Delete(modeloPorRemover);
            }

            using (var repository = new ModeloRepository(AppSettings, Logger))
            {
                var modeloActualizado = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre).Should().BeEmpty();
            }
        }

        [TestMethod]
        public void ModeloInexistente_SaerchByNombre_NoDevuelveModelo()
        {
            using (var repository = new ModeloRepository(AppSettings, Logger))
            {
                var modeloActualizado = repository.Search(m => m.Nombre == Guid.NewGuid().ToString()).Should().BeEmpty();
            }
        }

        [TestMethod]
        public void ModeloCreadoEnArchivoEjemplo_SaerchByNombre_DevuelveModelo()
        {
            Mock<IAppSettings> appSettings = CreateTestModelAppSettings();
            using var repository = new ModeloRepository(appSettings.Object, Logger);
            repository.Search(m => m.Nombre == "DPC9989").Should().NotBeEmpty();
            repository.Search(m => m.Nombre == "DPC3825").Should().NotBeEmpty();
        }

        [TestMethod]
        public void ModeloInexistenteEnArchivoEjemplo_SaerchByNombre_NoDevuelveModelo()
        {
            Mock<IAppSettings> appSettings = CreateTestModelAppSettings();
            using var repository = new ModeloRepository(appSettings.Object, Logger);
            repository.Search(m => m.Nombre == "DPC381125").Should().BeEmpty();
        }

        [TestMethod]
        public void ModeloExistenteEnArchivoEjemplo_SaerchByFabricanteAndContainsNombre_DevuelveModelo()
        {
            Mock<IAppSettings> appSettings = CreateTestModelAppSettings();
            using var repository = new ModeloRepository(appSettings.Object, Logger);
            var coleccionModelos = new List<string>() { "DPC3825" };
            repository.Search(m => m.Fabricante == "Cisco" && coleccionModelos.Contains(m.Nombre)).Should().HaveCount(1);
            repository.Search(m => m.Fabricante == "Cisco" && coleccionModelos.Contains(m.Nombre)).First().Fabricante.Should().Be("Cisco");
            repository.Search(m => m.Fabricante == "Cisco" && coleccionModelos.Contains(m.Nombre)).First().Nombre.Should().Be("DPC3825");
        }

        public override Modelo CreateEntity()
        {
            var modelo = new Modelo("Cisco", Guid.NewGuid().ToString(), "v1.0");
            var repository = new ModeloRepository(AppSettings, Logger);
            return repository.Save(modelo);
        }

        private static Mock<IAppSettings> CreateTestModelAppSettings()
        {
            var appSettings = new Mock<IAppSettings>();
            appSettings.Setup(mock => mock.JsonStorageFilePath).Returns(Path.Combine("Datos", "testModels.json"));
            return appSettings;
        }

    }
}
