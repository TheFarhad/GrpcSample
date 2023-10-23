namespace GrpcServer.Core.Contract.Infra.Command;

using Sky.App.Core.Contract.Infra.Command;
using Domain.Aggregates.Source;

public interface IPersonCommandRepository : ICommandRepository<Person>
{
}
