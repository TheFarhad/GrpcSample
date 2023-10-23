namespace GrpcServer.Infra.Data.Sql.Query.Repositories;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sky.Kernel.Extentions;
using Sky.App.Infra.Data.Sql.Query;
using DbSets;
using Contexts;
using Core.Contract.Infra.Query;
using Core.Contract.Services.Query;

public class PersonQueryRepository : QueryRepository<GrpcServerQueryDbContext>, IPersonQueryRepository
{
    public PersonQueryRepository(GrpcServerQueryDbContext context) : base(context) { }

    public async Task<PersonSearchByIdPayload> GetByIdAsync(PersonSearchByIdQuery source)
    {
        var result = default(PersonSearchByIdPayload);
        if (source.Id != 0)
        {
            var person = await Context
                .People
                .Include(_ => _.Phones)
                .FirstOrDefaultAsync(_ => _.Id == source.Id);

            if (person is not null) result = ToPersonSearchItem(person) as PersonSearchByIdPayload;
        }
        return result;
    }

    public async Task<PersonSearchByNamePayload> ListAsync(PersonSearchByNameQuery source)
    {
        var result = new PersonSearchByNamePayload();
        var query = Context.People.AsNoTracking();

        if (source.NeededTotalCount) result.Total = query.Count();

        if (source.FirstName.IsNotEmpty())
            query = query.Where(_ => _.FirstName.Contains(source.FirstName));

        if (source.LastName.IsNotEmpty())
            query = query.Where(_ => _.LastName.Contains(source.LastName));

        result.Items = await query
            .Include(_ => _.Phones)
            .Skip(source.Skip)
            .Take(source.Size)
            .OrderBy(source.SortBy, source.SortAscending)
            .Select(_ => ToPersonSearchItem(_))
            .ToListAsync();

        return result;
    }

    private static PersonSearchItem ToPersonSearchItem(Person source) =>
        new PersonSearchItem
        {
            Id = source.Id,
            FirstName = source.FirstName,
            LastName = source.LastName,
            NationalCode = source.NationalCode,
            Biography = source.Biography,
            Phones = source.Phones.Select(_ => _.Value).ToList()
        };
}
