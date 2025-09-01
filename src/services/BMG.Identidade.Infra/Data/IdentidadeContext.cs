using BMG.Core.Data;
using BMG.Identidade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BMG.Identidade.Infra.Data
{

    public class IdentidadeContext : DbContext, IUnitOfWork
    {
        public IdentidadeContext(DbContextOptions<IdentidadeContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

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
