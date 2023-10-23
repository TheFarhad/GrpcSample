namespace GrpcServer.API.Services.v1;

using System.Threading.Tasks;
using Sky.App.Core.Contract.Extentions;
using Sky.App.Core.Contract.Services.Query;
using Sky.App.Core.Contract.Services.Command;
using Protos.v1;
using Grpc.Core;
using Core.Contract.Services.Query;
using static Protos.v1.PersonService;
using Core.Contract.Services.Command;

public class PersonGrpcService : PersonServiceBase
{
    private readonly HttpContext _httpContext;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public PersonGrpcService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _commandDispatcher = _httpContext.CommandDispatcher();
        _queryDispatcher = _httpContext.QueryDispatcher();
    }

    public override async Task Create(IAsyncStreamReader<PersonCreateRequest> requestStream, IServerStreamWriter<PersonCreateReply> responseStream, ServerCallContext context)
    {
        await foreach (var _ in requestStream.ReadAllAsync())
        {
            var command = new PersonCreateCommand
            {
                FirstName = _.FirstName,
                LastName = _.LastName,
                NationalCode = _.NationalCode,
                Biography = _.Biography,
                Phones = _.Phones.ToList()
            };
            var response = await _commandDispatcher.DispatchAsync<PersonCreateCommand, PersonCreatePayload>(command);

            await responseStream.WriteAsync(new PersonCreateReply
            {
                Id = response.Payload.Id
            });
        }
        await Task.CompletedTask;
    }

    public override async Task<PersonReply> GetById(PersonByIdRequest request, ServerCallContext context)
    {
        var model = await _queryDispatcher.DispatchAsync<PersonSearchByIdQuery, PersonSearchByIdPayload>(new PersonSearchByIdQuery
        {
            Id = request.Id,
        });
        var person = model.Payload;
        var result = new PersonReply
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            NationalCode = person.NationalCode,
            Biography = person.Biography
        };
        result.Phone.AddRange(person.Phones);
        return result;
    }

    public override async Task<PersonEditReply> Edit(PersonEditRequest request, ServerCallContext context)
    {
        var response = await _commandDispatcher.DispatchAsync<PersonEditCommand, PersonEditPayload>(new PersonEditCommand
        {
            Id = request.Id,
        });
        var success = response.Payload is not null ? response.Payload.Success : false;
        var result = new PersonEditReply { Success = success };
        return result;
    }

    public override async Task<PersonRemoveReply> Remove(PersonByIdRequest request, ServerCallContext context)
    {
        var response = await _commandDispatcher.DispatchAsync<PersonRemoveCommand, PersonRemovePayload>(new PersonRemoveCommand
        {
            Id = request.Id,
        });
        var success = response.Payload is not null ? response.Payload.Success : false;
        var result = new PersonRemoveReply { Success = success };
        return result;
    }

    public override async Task List(PersonSearchListByNameRequest request, IServerStreamWriter<PersonReply> responseStream, ServerCallContext context)
    {
        var models = await _queryDispatcher.DispatchAsync<PersonSearchByNameQuery, PersonSearchByNamePayload>(new PersonSearchByNameQuery
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Page = request.Page,
            Size = request.Size,
            NeededTotalCount = request.NeededTotaoCount,
            SortAscending = request.SortAscending,
            SortBy = request.SortBy
        });

        foreach (var _ in models.Payload.Items)
        {
            var response = new PersonReply
            {
                Id = _.Id,
                FirstName = _.FirstName,
                LastName = _.LastName,
                NationalCode = _.NationalCode,
                Biography = _.Biography
            };
            response.Phone.AddRange(_.Phones);
            await responseStream.WriteAsync(response);
        }
        await Task.CompletedTask;
    }
}
