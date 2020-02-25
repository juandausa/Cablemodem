using Newtonsoft.Json;

namespace Entidades
{
    [JsonObject("models")]
    public class Modelo
    {
        public Modelo(string fabricante, string nombre, string versionSoftware)
        {
            if (string.IsNullOrWhiteSpace(fabricante))
            {
                throw new System.ArgumentException("Fabricante no puede estar en blanco", nameof(fabricante));
            }

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new System.ArgumentException("Nombre no puede estar en blanco", nameof(nombre));
            }

            if (string.IsNullOrWhiteSpace(versionSoftware))
            {
                throw new System.ArgumentException("Version de software no puede estar en blanco", nameof(versionSoftware));
            }

            Fabricante = fabricante;
            Nombre = nombre;
            VersionSoftware = versionSoftware;
        }

        /// <summary>
        /// Fabricante
        /// </summary>
        [JsonProperty("vendor")]
        public string Fabricante { get; set; }

        /// <summary>
        /// Nombre del modelo
        /// </summary>
        [JsonProperty("name")]
        public string Nombre { get; set; }

        /// <summary>
        /// Versión del software
        /// </summary>
        [JsonProperty("soft")]
        public string VersionSoftware { get; set; }

        public override string ToString()
        {
            return string.Format("Fabricate: {0}, Nombre: {1}, VersionSoftware: {2}", this.Fabricante, this.Nombre, this.VersionSoftware);
        }
    }
}
