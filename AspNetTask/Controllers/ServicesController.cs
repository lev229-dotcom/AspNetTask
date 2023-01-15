using AspNetTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetTask.Controllers;

[ApiController]
[Route("[controller]")]
public class ServicesController : ControllerBase
{
    private readonly ServiceObjectService serviceObjectService;

    public ServicesController(ServiceObjectService serviceObjectService)
    {
        this.serviceObjectService = serviceObjectService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<Guid> Create(string name, int amount)
    {
         return await serviceObjectService.CreateServiceObject(name, amount);
    }

    [HttpPut("Update/{id}")]
    public async Task<string> Update(string name, int amount, Guid id)
    {
        return await serviceObjectService.UpdateServiceObject(name, amount, id);
    }

    [HttpGet("Booking/{id}")]
    public async Task<string> Update(Guid id, int amount)
    {
        var result = await serviceObjectService.Booking(id, amount);

        if (!result.Item1)
            return "Превышен остаток";
        else
            return $"{result.Item2}";
    }

}
