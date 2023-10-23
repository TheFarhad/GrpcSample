namespace GrpcClient.Core.Contract.AppService.DTOs;

public class Person : PersonCreateCommand
{
    public long Id { get; set; }
}
