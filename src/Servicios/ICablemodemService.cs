using Entidades;
using System.Collections.Generic;

namespace Servicios
{
    public interface ICablemodemService
    {
        IEnumerable<Cablemodem> GetNoVerificados(string fabricante = "");
        IEnumerable<Cablemodem> GetVerificados(string fabricante = "");
    }
}
