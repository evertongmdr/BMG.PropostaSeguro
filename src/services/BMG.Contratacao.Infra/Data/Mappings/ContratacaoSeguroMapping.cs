using BMG.Contratacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BMG.Contratacao.Infra.Data.Mappings
{
    public class ContratacaoSeguroMapping : IEntityTypeConfiguration<ContratacaoSeguro>
    {
        public void Configure(EntityTypeBuilder<ContratacaoSeguro> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PropostaId)
                .IsRequired();

            builder.Property(x => x.ContratenteId)
              .IsRequired();

            builder.Property(x => x.DataContratacao)
              .IsRequired();
        }
    }
}
