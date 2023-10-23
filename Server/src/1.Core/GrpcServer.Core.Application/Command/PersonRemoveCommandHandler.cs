namespace GrpcServer.Core.Application.Command;

using System.Threading.Tasks;
using Sky.App.Core.Service.Command;
using Sky.App.Core.Contract.Services.Command;
using Contract.Infra.Command;
using Contract.Services.Command;

public class PersonRemoveCommandHandler : CommandHandler<PersonRemoveCommand, PersonRemovePayload>
{
    private readonly IPersonCommandRepository _repository;

    public PersonRemoveCommandHandler(IPersonCommandRepository repository) =>
        _repository = repository;

    public override async Task<CommandResult<PersonRemovePayload>> HandleAsync(PersonRemoveCommand Source)
    {
        var model = await _repository.GetAsync(Source.Id);
        if (model is null) Result = await NotFound();
        else
        {
            model.Remove();
            await _repository.SaveAsync();
            Result = await OK(new PersonRemovePayload { Success = true });
        }
        return Result;
    }
}
