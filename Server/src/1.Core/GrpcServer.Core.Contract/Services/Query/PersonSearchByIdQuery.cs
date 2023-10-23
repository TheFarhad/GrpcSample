namespace GrpcServer.Core.Contract.Services.Query;

using Sky.App.Core.Contract.Services.Query;

public class PersonSearchByIdQuery : IQuery<PersonSearchByIdPayload>
{
    public long Id { get; set; }
}

public class PersonSearchByIdPayload : PersonSearchItem { }
