using Entidades;
using Microsoft.Extensions.Logging;

namespace Infraestructura.Impl
{
    public class CablemodemRepository : BaseRepository<Cablemodem>, ICablemodemRepository
    {
        public CablemodemRepository(CablemodemContext context, ILogger<CablemodemRepository> logger) : base(context, logger)
        {
        }
    }
}
