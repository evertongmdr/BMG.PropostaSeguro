using BMG.Propostas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMG.Propostas.Infra.Data.Mappings
{
    public class PropostaMapping : IEntityTypeConfiguration<Proposta>
    {
        public void Configure(EntityTypeBuilder<Proposta> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NumeroProposta).IsRequired();
            builder.HasIndex(p => p.NumeroProposta).IsUnique();

            builder.Property(p => p.Titulo).IsRequired().HasMaxLength(200);

            builder.Property(p => p.Descricao).IsRequired().HasMaxLength(1000);

            builder.Property(p => p.CriadoPorUsuarioId).IsRequired();
        }
    }
}
