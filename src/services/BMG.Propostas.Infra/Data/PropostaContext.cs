using BMG.Core.Data;
using BMG.Propostas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BMG.Propostas.Infra.Data
{
    public class PropostaContext : DbContext, IUnitOfWork
    {
        public PropostaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Proposta> Propostas { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

    }
}
