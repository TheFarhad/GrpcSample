namespace GrpcClient.Core.Contract.AppService.DTOs;

public class PersonCreateCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string Biography { get; set; }
    public List<string> Phones { get; set; }
}