namespace GrpcServer.Infra.Data.Sql.Query.DbSets;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Phone
{
    [Key]
    public long Id { get; set; }
    public string Value { get; set; }

    [ForeignKey("Person")]
    public long PersonId { get; set; }
    public Person Person { get; set; }
}
