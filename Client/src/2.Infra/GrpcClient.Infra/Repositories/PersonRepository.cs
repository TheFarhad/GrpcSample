namespace GrpcClient.Infra.Repositories;

using System.Threading.Tasks;
using System.Collections.Generic;
using Protos.v1;
using Grpc.Core;
using Core.Contract.Infra;
using static Protos.v1.PersonService;
using Core.Contract.AppService.DTOs;

public class PersonRepository : IPersonRepository
{
    private readonly PersonServiceClient _personService;

    public PersonRepository(PersonServiceClient personService) =>
          _personService = personService;

    public async IAsyncEnumerable<long> Create(List<PersonCreateCommand> command)
    {
        var request = _personService.Create();

        foreach (var _ in command)
        {
            var pcr = new PersonCreateRequest
            {
                FirstName = _.FirstName,
                LastName = _.LastName,
                NationalCode = _.NationalCode,
                Biography = _.Biography,
            };
            pcr.Phones.AddRange(_.Phones);
            await request.RequestStream.WriteAsync(pcr);
        }

        await request.RequestStream.CompleteAsync();

        while (await request.ResponseStream.MoveNext())
        {
            yield return request.ResponseStream.Current.Id;
        }
    }

    public async Task<bool> Edit(PersonEditCommand command)
    {
        var request = await _personService.EditAsync(new PersonEditRequest
        {
            Id = command.Id,
            Biography = command.Biography,
        });
        return request.Success;
    }

    public async Task<Person> GetById(long id)
    {
        var request = await _personService.GetByIdAsync(new PersonByIdRequest { Id = id });
        var result = new Person
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            NationalCode = request.NationalCode,
            Biography = request.Biography,
            Phones = new List<string>(request.Phone)
        };
        return result;
    }

    public async IAsyncEnumerable<Person> List(PersonSearchByNmeQuery query)
    {
        var request = _personService.List(new PersonSearchListByNameRequest
        {
            FirstName = query.FirstName,
            LastName = query.LastName,
            Size = query.Size,
            NeededTotaoCount = query.NeededTotalCount,
            Page = query.Page,
            SortAscending = query.SortAscending,
            SortBy = query.SortBy
        });

        while (await request.ResponseStream.MoveNext())
        {
            var person = request.ResponseStream.Current;
            yield return new Person
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                NationalCode = person.NationalCode,
                Biography = person.Biography,
                Phones = new List<string>(person.Phone)
            };
        }
    }

    public async Task<bool> Remove(long id)
    {
        var request = await _personService.RemoveAsync(new PersonByIdRequest { Id = id });
        return request.Success;
    }
}
