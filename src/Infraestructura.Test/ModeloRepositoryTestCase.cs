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
            modeloPorPersistir.Name.Should().NotBeNullOrEmpty();
            modeloPorPersistir.Vendor.Should().NotBeNullOrEmpty();
            modeloPorPersistir.Soft.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void ModeloCreado_Search_SeGuardaModelo()
        {
            var modeloPorPersistir = CreateEntity();
            using var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json"));
            var modelos = repository.Search(m => m.Name == modeloPorPersistir.Name);
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
                var modeloPorActualizar = repository.Search(m => m.Name == modeloPorPersistir.Name).First();
                modeloPorActualizar.Soft = valorActualizado;
                repository.Update(modeloPorActualizar);
            }

            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloActualizado = repository.Search(m => m.Name == modeloPorPersistir.Name).First();
                modeloActualizado.Soft.Should().Be(valorActualizado);
            }
        }

        [TestMethod]
        public void ModeloCreado_Borrar_ObtenerModelo_NoDevuelveModelo()
        {
            var modeloPorPersistir = CreateEntity();
            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloPorRemover = repository.Search(m => m.Name == modeloPorPersistir.Name).First();
                repository.Delete(modeloPorRemover);
            }

            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloActualizado = repository.Search(m => m.Name == modeloPorPersistir.Name).Should().BeEmpty();
            }
        }

        [TestMethod]
        public void ModeloInexistente_ObtenerModelo_NoDevuelveModelo()
        {
            using (var repository = new ModeloRepository(Path.Combine("Datos", "emptyModels.json")))
            {
                var modeloActualizado = repository.Search(m => m.Name == Guid.NewGuid().ToString()).Should().BeEmpty();
            }
        }

        public override Modelo CreateEntity()
        {
            return CreateEntity("emptyModels.json");
        }

        private static Modelo CreateEntity(string filePath)
        {
            var modelo = new Modelo()
            {
                Name = Guid.NewGuid().ToString(),
                Soft = "v1.0",
                Vendor = "Cisco"
            };
            var repository = new ModeloRepository(Path.Combine("Datos", filePath));
            return repository.Save(modelo);
        }
    }
}
