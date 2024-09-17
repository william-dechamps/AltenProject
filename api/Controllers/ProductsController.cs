namespace AltenProject.Controllers;
using AltenProject.Dtos;
using AltenProject.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(
        IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("getproducts")]
    public ActionResult GetProducts()
    {
        List<ProductDto> products = _productService.GetProductsDtos();
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public ActionResult GetProductById(int productId)
    {
        try
        {
            ProductDto product = _productService.GetProductDtoById(productId);
            return Ok(product);
        }
        catch (InputInvalidException erreur)
        {
            return BadRequest(erreur.Message);
        }
    }

    [HttpDelete("{productId}")]
    public ActionResult DeleteProductById(int productId)
    {
        try
        {
            _productService.DeleteProductById(productId);
            return Ok();
        }
        catch (InputInvalidException erreur)
        {
            return BadRequest(erreur.Message);
        }
    }

    [HttpPost("")]
    public ActionResult AddProduct(AddProductDto product)
    {
        try
        {
            _productService.AddProduct(product);
            return Ok();
        }
        catch (InputInvalidException erreur)
        {
            return BadRequest(erreur.Message);
        }
    }

    [HttpPatch("")]
    public ActionResult UpdateProduct(UpdateProductDto product)
    {
        try
        {
            _productService.UpdateProduct(product);
            return Ok();
        }
        catch (InputInvalidException erreur)
        {
            return BadRequest(erreur.Message);
        }
    }
}