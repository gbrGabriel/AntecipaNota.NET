using AntecipaNotaNET.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntecipaNotaNET.Infrastructure.Configurations;

public class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
{
    public void Configure(EntityTypeBuilder<NotaFiscal> builder)
    {
        builder.HasKey(e => e.Numero);

        builder.Property(e => e.Numero).ValueGeneratedNever();

        builder.Property(e => e.Desagio).HasPrecision(18, 5);

        builder.Property(e => e.ValorBruto).HasPrecision(18, 5);

        builder.Property(e => e.ValorLiquido).HasPrecision(18, 5);
    }
}
