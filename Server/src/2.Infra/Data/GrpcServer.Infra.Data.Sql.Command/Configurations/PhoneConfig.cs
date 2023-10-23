namespace GrpcServer.Infra.Data.Sql.Command.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.App.Infra.Data.Sql.Command.Configuration;
using Core.Domain.Aggregates.References;

public class PhoneConfig : IEntityConfig<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.ToTable("Phones");
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.Value).HasMaxLength(11);
    }
}
