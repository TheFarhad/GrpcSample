namespace GrpcServer.Infra.Data.Sql.Query.Contexts;

using Microsoft.EntityFrameworkCore;
using Sky.App.Infra.Data.Sql.Query;
using DbSets;

public class GrpcServerQueryDbContext : QueryDbContext
{
    public DbSet<Person> People => Set<Person>();
    public DbSet<Phone> Phones => Set<Phone>();

    public GrpcServerQueryDbContext(DbContextOptions<GrpcServerQueryDbContext> options) : base(options)
    { }
}
