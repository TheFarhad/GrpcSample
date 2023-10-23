namespace GrpcServer.Core.Contract.Services.Command;

using Sky.App.Core.Contract.Services.Command;

public class PersonEditCommand : ICommand<PersonEditPayload>
{
    public long Id { get; set; }
    public string Biography { get; set; }
}

public class PersonEditPayload
{
    public bool Success { get; set; }
}


