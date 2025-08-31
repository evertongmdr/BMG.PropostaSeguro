using BMG.Core.Data;
using BMG.Propostas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BMG.Propostas.Infra.Data
{
    public class PropostaContext : DbContext, IUnitOfWork
    {
        public PropostaContext(DbContextOptions<PropostaContext> options) : base(options)
        {
        }

        public DbSet<Proposta> Propostas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }

    }
}
