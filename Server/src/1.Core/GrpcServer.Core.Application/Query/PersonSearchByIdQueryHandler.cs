namespace GrpcServer.Core.Application.Query;

using Sky.App.Core.Service.Query;
using Sky.App.Core.Contract.Services.Query;
using Contract.Infra.Query;
using Contract.Services.Query;

public class PersonSearchByIdQueryHandler : QueryHandler<PersonSearchByIdQuery, PersonSearchByIdPayload>
{
    private readonly IPersonQueryRepository _repository;

    public PersonSearchByIdQueryHandler(IPersonQueryRepository repository) =>
        _repository = repository;

    public override async Task<QueryResult<PersonSearchByIdPayload>> HandleAsync(PersonSearchByIdQuery source)
    {
        var payload = await _repository.GetByIdAsync(source);
        return await OK(payload);
    }
}
