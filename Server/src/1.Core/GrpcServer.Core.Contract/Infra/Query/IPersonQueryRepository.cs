namespace GrpcServer.Core.Contract.Infra.Query;

using Sky.App.Core.Contract.Infra.Query;
using Services.Query;

public interface IPersonQueryRepository : IQueryRepository
{
    Task<PersonSearchByNamePayload> ListAsync(PersonSearchByNameQuery query);
    Task<PersonSearchByIdPayload> GetByIdAsync(PersonSearchByIdQuery query);
}
