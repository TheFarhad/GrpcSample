namespace GrpcServer.Infra.Data.Sql.Command.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sky.App.Infra.Data.Sql.Command.Configuration;
using Core.Domain.Aggregates.Source;

public class PersonConfig : IEntityConfig<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.FirstName).HasMaxLength(100);
        builder.Property(_ => _.LastName).HasMaxLength(150);
        builder.Property(_ => _.Biography).HasMaxLength(500);

        builder
            .HasMany(_ => _.Phones)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}