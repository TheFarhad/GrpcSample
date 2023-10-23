namespace GrpcServer.Infra.Data.Sql.Command.Configurations;

using Sky.App.Infra.Data.Sql.Command;
using Contexts;
using Core.Contract.Infra.Command;
using Core.Domain.Aggregates.Source;

public class PersonRepository : CommandRepository<Person, GrpcServerCommandDbContext>, IPersonCommandRepository
{
    public PersonRepository(GrpcServerCommandDbContext source) : base(source) { }

}
