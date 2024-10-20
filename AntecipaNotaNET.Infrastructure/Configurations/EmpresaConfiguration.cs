using AntecipaNotaNET.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AntecipaNotaNET.Infrastructure.Configurations;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Ramo).HasMaxLength(50);

        builder.Property(e => e.Cnpj).HasMaxLength(20);

        builder.Property(e => e.Faturamento).HasPrecision(18, 5);

        builder.Property(e => e.Limite).HasPrecision(18, 5);

        builder.Property(e => e.Nome).HasMaxLength(255);
    }
}
