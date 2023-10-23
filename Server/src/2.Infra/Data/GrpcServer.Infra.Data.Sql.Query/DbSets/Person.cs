namespace GrpcServer.Infra.Data.Sql.Query.DbSets;

using System.ComponentModel.DataAnnotations;

public class Person
{
    [Key]
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string Biography { get; set; }
    public bool IsRemoved { get; set; }
    public List<Phone> Phones { get; set; } = new();
}