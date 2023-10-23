namespace GrpcServer.Core.Domain.Aggregates.Source;

using Sky.App.Core.Domain.Aggregate.Entity;
using References;

public class Person : Source
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalCode { get; private set; }
    public string Biography { get; private set; }
    private List<Phone> _phones = new();
    public IReadOnlyList<Phone> Phones => _phones.AsReadOnly();
    public bool IsRemoved { get; private set; }

    private Person() { }
    private Person(string firstName, string lastName, string nationalCode, string biography, List<Phone> phones)
    {
        // validation and invariant...

        FirstName = firstName;
        LastName = lastName;
        NationalCode = nationalCode;
        Biography = biography;
        _phones = phones;
    }

    public static Person Instance(string firstName, string lastName, string nationalCode, string biography, List<Phone> phones) =>
        new(firstName, lastName, nationalCode, biography, phones);

    public void Edit(string biography) => Biography = biography;

    public void Remove() => IsRemoved = true;
    public void Restore() => IsRemoved = false;
}
