namespace GrpcClient.Core.AppService;

using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Contract.Infra;
using Contract.AppService.DTOs;
using Contract.AppService.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IPersonRepository repository, ILogger<PersonService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Create(List<PersonCreateCommand> command)
    {
        await foreach (var _ in _repository.Create(command))
            _logger.LogInformation("Person created by id {id} at time {time}", _, DateTime.Now.ToString());

        await Task.CompletedTask;
    }

    public async Task<bool> Edit(PersonEditCommand command) =>
        await _repository.Edit(command);

    public async Task<Person> GetById(long id) =>
        await _repository.GetById(id);

    public async Task<List<Person>> List(PersonSearchByNmeQuery query)
    {
        var result = new List<Person>();
        await foreach (var _ in _repository.List(query)) result.Add(_);
        return result;
    }

    public async Task<bool> Remove(long id) =>
        await _repository.Remove(id);
}
