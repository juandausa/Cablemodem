namespace Entidades
{
    public class Cablemodem
    {
        /// <summary>
        /// MAC address del cablemódem.
        /// </summary>
        public string MacAddress { get; set; }
        /// <summary>
        /// Ip del cablemódem.
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Modelo.
        /// </summary>
        public string Modelo { get; set; }
        /// <summary>
        /// Fabricante.
        /// </summary>
        public string Fabricante { get; set; }
        /// <summary>
        /// Versión del software.
        /// </summary>
        public string VersionSoftware { get; set; }

        public override string ToString()
        {
            return string.Format("MacAddress: {0}, IP: {1}, Modelo: {2}, Fabricate: {3}, VersionSoftware: {4}", this.MacAddress, this.Ip, this.Modelo, this.Fabricante, this.VersionSoftware);
        }
    }
}
