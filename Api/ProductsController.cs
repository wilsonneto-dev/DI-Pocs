using Microsoft.AspNetCore.Mvc;

namespace Api;
[ApiController]
[Route("[controller]")]
[ServiceFilter(typeof(LogFilter))]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        ILogger<ProductsController> logger,
        INameService<ProductsController> nameService,
        IService service,
        IGetProductsUseCase useCase)
    {

        // logger.LogInformation("Controller... Service Id: {Id}", service.GetId());

        useCase.Execute();

        return Ok(new
        {
            Success = true,
            Id = service.GetId(),
            Name = nameService.GetName(this)
        });
    }
}
