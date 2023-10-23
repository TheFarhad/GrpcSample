namespace GrpcServer.Core.Application.Command;

using System.Threading.Tasks;
using Sky.App.Core.Service.Command;
using Sky.App.Core.Contract.Services.Command;
using Contract.Infra.Command;
using Domain.Aggregates.Source;
using Contract.Services.Command;
using Domain.Aggregates.References;

public class PersonCreateCommandHandler : CommandHandler<PersonCreateCommand, PersonCreatePayload>
{
    private readonly IPersonCommandRepository _repository;

    public PersonCreateCommandHandler(IPersonCommandRepository repository) =>
        _repository = repository;

    public override async Task<CommandResult<PersonCreatePayload>> HandleAsync(PersonCreateCommand Source)
    {
        var model = Person.Instance(Source.FirstName, Source.LastName, Source.NationalCode, Source.Biography, Source.Phones.Select(_ => Phone.Instance(_)).ToList());

        await _repository.AddAsync(model);
        await _repository.SaveAsync();
        return await OK(new PersonCreatePayload { Id = model.Id });
    }
}