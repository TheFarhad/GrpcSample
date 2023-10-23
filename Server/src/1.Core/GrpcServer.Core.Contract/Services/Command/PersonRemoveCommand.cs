namespace GrpcServer.Core.Contract.Services.Command;

using Sky.App.Core.Contract.Services.Command;

public class PersonRemoveCommand : ICommand<PersonRemovePayload>
{
    public long Id { get; set; }
}

public class PersonRemovePayload
{
    public bool Success { get; set; }
}
