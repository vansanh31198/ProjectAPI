using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Application.Services;
using ProjectAPI.Application.Services.Abstractions;
using ProjectAPI.Domain.Entities;
using ProjectAPI.Event;

namespace ProjectAPI.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetProductAll")]
    public async Task<IResult> Get()
    {
        var products = await _productService.GetAllProduct();

        return TypedResults.Ok(new { data = products.ToList() });
    }

    [HttpGet("GetProductDetail")]
    public async Task<IResult> GetProductDetail(int productID)
    {
        var product = await _productService.GetProductDetail(productID);

        List<string> testArr = new List<string>();

        ProcessGetProduct processGetProduct = new ProcessGetProduct();

        string confirmProcess = "Tiến hành lấy hàng";
        string tranferProcess = "Giao hàng";
        string finishProcess = "Hoàn thành";

        processGetProduct.OnGetProductProcess += () =>
        {
            testArr.Add(confirmProcess);
        };

        processGetProduct.OnGetProductProcess += () =>
        {
            testArr.Add(tranferProcess);
        };

        processGetProduct.OnGetProductProcess += () =>
        {
            testArr.Add(finishProcess);
        };

        processGetProduct.ProcessHandle();

        return TypedResults.Ok(new { data = product , process = testArr });
    }
}
