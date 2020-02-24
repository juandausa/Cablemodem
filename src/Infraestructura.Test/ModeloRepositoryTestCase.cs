using Base.Test;
using Entidades;
using FluentAssertions;
using Infraestructura.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Infraestructura.Test
{
    [TestClass]
    public class ModeloRepositoryTestCase : BaseRepositoryTestCase<Modelo>
    {
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
            using var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json"));
            var modelos = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre);
            modelos.Should().HaveCount(1);
            AreEquals(modeloPorPersistir, modelos.First());
        }

        [TestMethod]
        public void ModeloCreado_ActualizarParametro_ObtenerModelo_DevuelveParametroActualizado()
        {
            var modeloPorPersistir = CreateEntity();
            const string valorActualizado = "v2.0";
            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloPorActualizar = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre).First();
                modeloPorActualizar.VersionSoftware = valorActualizado;
                repository.Update(modeloPorActualizar);
            }

            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloActualizado = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre).First();
                modeloActualizado.VersionSoftware.Should().Be(valorActualizado);
            }
        }

        [TestMethod]
        public void ModeloCreado_Borrar_ObtenerModelo_NoDevuelveModelo()
        {
            var modeloPorPersistir = CreateEntity();
            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloPorRemover = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre).First();
                repository.Delete(modeloPorRemover);
            }

            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloActualizado = repository.Search(m => m.Nombre == modeloPorPersistir.Nombre).Should().BeEmpty();
            }
        }

        [TestMethod]
        public void ModeloInexistente_ObtenerModelo_NoDevuelveModelo()
        {
            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloActualizado = repository.Search(m => m.Nombre == Guid.NewGuid().ToString()).Should().BeEmpty();
            }
        }

        [TestMethod]
        public void ModeloCreadoEnArchivoEjemplo_ObtenerModelo_DevuelveModelo()
        {
            using var repository = new ModeloRepository(Path.Combine("Datos", "testModels.json"));
            repository.Search(m => m.Nombre == "DPC9989").Should().NotBeEmpty();
            repository.Search(m => m.Nombre == "DPC3825").Should().NotBeEmpty();
        }

        [TestMethod]
        public void ModeloInexistenteEnArchivoEjemplo_ObtenerModelo_NoDevuelveModelo()
        {
            using var repository = new ModeloRepository(Path.Combine("Datos", "testModels.json"));
            repository.Search(m => m.Nombre == "DPC381125").Should().BeEmpty();
        }


        public override Modelo CreateEntity()
        {
            return CreateEntity("emptyModels.json");
        }

        private static Modelo CreateEntity(string filePath)
        {
            var modelo = new Modelo()
            {
                Nombre = Guid.NewGuid().ToString(),
                VersionSoftware = "v1.0",
                Fabricante = "Cisco"
            };
            var repository = new ModeloRepository(Path.Combine("Datos", filePath));
            return repository.Save(modelo);
        }
    }
}
