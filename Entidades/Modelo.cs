using Newtonsoft.Json;

namespace Entidades
{
    [JsonObject("models")]
    public class Modelo
    {
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
