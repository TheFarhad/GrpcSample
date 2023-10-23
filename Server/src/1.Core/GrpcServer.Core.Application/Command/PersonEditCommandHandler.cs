namespace GrpcServer.Core.Application.Command;

using System.Threading.Tasks;
using Sky.App.Core.Service.Command;
using Sky.App.Core.Contract.Services.Command;
using Contract.Infra.Command;
using Contract.Services.Command;

public class PersonEditCommandHandler : CommandHandler<PersonEditCommand, PersonEditPayload>
{
    private readonly IPersonCommandRepository _repository;

    public PersonEditCommandHandler(IPersonCommandRepository repository) =>
        _repository = repository;

    public override async Task<CommandResult<PersonEditPayload>> HandleAsync(PersonEditCommand Source)
    {
        var model = await _repository.GetAsync(Source.Id);
        if (model is null) Result = await NotFound();
        else
        {
            model.Edit(Source.Biography);
            await _repository.SaveAsync();
            Result = await OK(new PersonEditPayload { Success = true });
        }
        return Result;
    }
}
