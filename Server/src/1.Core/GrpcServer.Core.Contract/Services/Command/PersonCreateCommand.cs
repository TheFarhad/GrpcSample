namespace GrpcServer.Core.Contract.Services.Command;

using Sky.App.Core.Contract.Services.Command;

public class PersonCreateCommand : ICommand<PersonCreatePayload>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string Biography { get; set; }
    public List<string> Phones = new();
}

public class PersonCreatePayload
{
    public long Id { get; set; }
}
