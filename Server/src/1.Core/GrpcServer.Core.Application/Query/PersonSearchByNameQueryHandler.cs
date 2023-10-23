namespace GrpcServer.Core.Application.Query;

using Sky.App.Core.Service.Query;
using Sky.App.Core.Contract.Services.Query;
using Contract.Infra.Query;
using Contract.Services.Query;

public class PersonSearchByNameQueryHandler : QueryHandler<PersonSearchByNameQuery, PersonSearchByNamePayload>
{
    private readonly IPersonQueryRepository _repository;

    public PersonSearchByNameQueryHandler(IPersonQueryRepository repository) =>
        _repository = repository;

    public override async Task<QueryResult<PersonSearchByNamePayload>> HandleAsync(PersonSearchByNameQuery source)
    {
        var payload = await _repository.ListAsync(source);
        return await OK(payload);
    }
}