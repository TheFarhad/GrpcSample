namespace GrpcServer.Core.Contract.Services.Query;

using Sky.App.Core.Contract.Services.Query;

public class PersonSearchByNameQuery : PageQuery<PersonSearchByNamePayload>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class PersonSearchByNamePayload : QueryPayload<PersonSearchItem> { }

public class PersonSearchItem
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string Biography { get; set; }
    public List<string> Phones = new();
}
