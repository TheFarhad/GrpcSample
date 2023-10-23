namespace GrpcClient.Core.Contract.AppService.DTOs;

public class PersonSearchByNmeQuery
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 1;
    public bool NeededTotalCount { get; set; } = true;
    public bool SortAscending { get; set; } = true;
    public String SortBy { get; set; } = "Id";
}


