namespace GrpcClient.Core.Contract.AppService.Services;

using DTOs;

public interface IPersonService
{
    Task Create(List<PersonCreateCommand> command);
    Task<Person> GetById(long id);
    Task<List<Person>> List(PersonSearchByNmeQuery query);
    Task<bool> Edit(PersonEditCommand command);
    Task<bool> Remove(long id);
}
