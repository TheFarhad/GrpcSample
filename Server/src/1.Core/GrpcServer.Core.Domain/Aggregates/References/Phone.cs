namespace GrpcServer.Core.Domain.Aggregates.References;

using Sky.App.Core.Domain.Aggregate.Entity;

public class Phone : Reference
{
    public string Value { get; private set; }

    private Phone() { }
    private Phone(string vlaue) => Value = vlaue;

    public static Phone Instance(string value) => new(value);
}
