namespace GrpcClient.Core.Contract.Infra;

using AppService.DTOs;

public interface IPersonRepository
{
    IAsyncEnumerable<long> Create(List<PersonCreateCommand> command);
    Task<Person> GetById(long id);
    IAsyncEnumerable<Person> List(PersonSearchByNmeQuery query);
    Task<bool> Edit(PersonEditCommand command);
    Task<bool> Remove(long id);
}
