using BMG.Contratacao.Domain.Entities;
using BMG.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BMG.Contratacao.Infra.Data
{
    public class ContratacaoContext : DbContext, IUnitOfWork
    {
        public ContratacaoContext(DbContextOptions<ContratacaoContext> options) : base(options)
        {
        }

        public DbSet<ContratacaoSeguro> Contratacoes { get; set; }

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
