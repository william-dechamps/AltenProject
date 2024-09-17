namespace AltenProject.Repositories;
using AltenProject.Entities;

public interface IProductRepository : IRepository<ProductEntity>
{

}

public class ProductRepository : IProductRepository
{
    private readonly AltenProjectDbContext _altenProjectDbContext;

    public ProductRepository(AltenProjectDbContext altenProjectDbContext)
    {
        _altenProjectDbContext = altenProjectDbContext;
    }

    public ProductEntity? GetSingle(int id)
    {
        return _altenProjectDbContext.Products.Where(product => product.Id == id).FirstOrDefault();
    }

    public void Add(ProductEntity product)
    {
        _altenProjectDbContext.Products.Add(product);
    }

    public ProductEntity Update(ProductEntity product)
    {
        _altenProjectDbContext.Products.Update(product);
        return product;
    }

    public IQueryable<ProductEntity> GetAll()
    {
        return _altenProjectDbContext.Products;
    }

    public bool Save()
    {
        return _altenProjectDbContext.SaveChanges() > 0;
    }

    public void Delete(ProductEntity product)
    {
        _altenProjectDbContext.Products.Remove(product);
    }
}