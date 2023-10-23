namespace GrpcServer.Infra.Data.Sql.Command.Contexts;

using Microsoft.EntityFrameworkCore;
using Sky.App.Infra.Data.Sql.Command;
using Core.Domain.Aggregates.Source;

public class GrpcServerCommandDbContext : CommandDbContext
{
    public DbSet<Person> People => Set<Person>();

    private GrpcServerCommandDbContext() { }
    public GrpcServerCommandDbContext(DbContextOptions<GrpcServerCommandDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
