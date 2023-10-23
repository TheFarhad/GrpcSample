namespace GrpcClient.Endpoint.Pages;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Contract.AppService.DTOs;
using Core.Contract.AppService.Services;

public class IndexModel : PageModel
{
    private readonly IPersonService _service;

    public IndexModel(IPersonService service) =>
         _service = service;

    public async void OnGetAsync()
    {
        var People = await _service.List(new PersonSearchByNmeQuery
        {
            FirstName = "",
            LastName = "",
            Size = 3
        });

        //var remove = await _service.Remove(4);
    }

    public async void OnPostAsync()
    {
    }
}