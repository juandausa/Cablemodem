namespace WebAPI.ViewModels
{
    public class Cablemodem
    {
        public Cablemodem(Entidades.Cablemodem cablemodem)
        {
            this.Ip = cablemodem.Ip;
            this.MacAddress = cablemodem.MacAddress;
            this.Modelo = cablemodem.Modelo;
            this.VersionSoftware = cablemodem.VersionSoftware;
        }

        public string MacAddress { get; set; }
        public string Ip { get; set; }
        public string Modelo { get; set; }
        public string VersionSoftware { get; set; }
    }
}
