namespace AltenProject.Services;
using AutoMapper;
using AltenProject.Repositories;
using AltenProject.Entities;
using AltenProject.Dtos;

public interface IProductService
{
    List<ProductEntity> GetProductsEntities();
    List<ProductDto> GetProductsDtos();
    ProductEntity GetProductEntityById(int productId);
    ProductDto GetProductDtoById(int productId);
    void AddProduct(AddProductDto product);
    void DeleteProductById(int productId);
    void UpdateProduct(UpdateProductDto productDto);
}

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    private IQueryable<ProductEntity> GetProductsQueryable()
    {
        return _productRepository.GetAll();
    }

    public List<ProductEntity> GetProductsEntities()
    {
        List<ProductEntity> products = GetProductsQueryable().ToList();
        return products;
    }

    public List<ProductDto> GetProductsDtos()
    {
        List<ProductDto> products = _mapper.Map<List<ProductDto>>(GetProductsEntities());
        return products;
    }

    public ProductEntity GetProductEntityById(int productId)
    {
        ProductEntity? product = GetProductsQueryable().Where(product => product.Id == productId).FirstOrDefault()
            ?? throw new InputInvalidException("error_unknown_product");
        return product;
    }

    public ProductDto GetProductDtoById(int productId)
    {
        ProductDto product = _mapper.Map<ProductDto>(GetProductEntityById(productId));
        return product;
    }

    public void DeleteProductById(int productId)
    {
        // Find product entity by id
        ProductEntity? product = GetProductEntityById(productId);

        _productRepository.Delete(product);

        if (!_productRepository.Save())
        {
            throw new InputInvalidException("error_delete_product_save_fail");
        }
    }

    public void AddProduct(AddProductDto product)
    {
        // Create product entity from the dto
        ProductEntity productEntity = _mapper.Map<ProductEntity>(product);

        productEntity.CreatedAt = DateTime.UtcNow;

        _productRepository.Add(productEntity);

        if (!_productRepository.Save())
        {
            throw new InputInvalidException("error_add_product_save_fail");
        }
    }

    public void UpdateProduct(UpdateProductDto productDto)
    {
        // Find product entity by id
        ProductEntity productEntity = GetProductEntityById(productDto.Id);

        // Map DTO properties to the entity
        _mapper.Map(productDto, productEntity);

        // Update the product with the changes
        _productRepository.Update(productEntity);

        // Save the changes
        if (!_productRepository.Save())
        {
            throw new InputInvalidException("error_update_product_save_fail");
        }
    }
}