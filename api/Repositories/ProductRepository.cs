namespace AltenProject.Repositories;
using AltenProject.Entities;

public interface IProductRepository : IRepository<ProductEntity>
{
    
}

public class ProductRepository : IProductRepository
{
    private readonly AltenProjectDbContext _altenProjectDbContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="altenProjectDbContext"></param>
    public ProductRepository(AltenProjectDbContext altenProjectDbContext)
    {
        _altenProjectDbContext = altenProjectDbContext;
    }

    /// <summary>
    /// Get a single product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Product entity</returns>
    public ProductEntity? GetSingle(int id)
    {
        return _altenProjectDbContext.Products.Where(product => product.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Add a product
    /// </summary>
    /// <param name="product"></param>
    public void Add(ProductEntity product)
    {
        _altenProjectDbContext.Products.Add(product);
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Product entity updated</returns>
    public ProductEntity Update(ProductEntity product)
    {
        _altenProjectDbContext.Products.Update(product);
        return product;
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>Products Queryable</returns>
    public IQueryable<ProductEntity> GetAll()
    {
        return _altenProjectDbContext.Products;
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="product"></param>
    public void Delete(ProductEntity product)
    {
        _altenProjectDbContext.Products.Remove(product);
    }

    /// <summary>
    /// Save changes
    /// </summary>
    /// <returns></returns>
    public bool Save()
    {
        return _altenProjectDbContext.SaveChanges() > 0;
    }
}