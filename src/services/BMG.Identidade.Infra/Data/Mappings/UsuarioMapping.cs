using BMG.Identidade.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMG.Identidade.Infra.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Login)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS").IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Login).IsUnique();

            builder.Property(x => x.Nome).IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.TipoUsuario).IsRequired();
        }
    }
}
