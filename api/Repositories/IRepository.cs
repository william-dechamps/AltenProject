namespace AltenProject.Repositories;
using AltenProject.Entities;

public interface IRepository<T> where T : IEntityId
{
    T? GetSingle(int id);
    void Add(T item);
    T Update(T item);
    IQueryable<T> GetAll();
    bool Save();
    void Delete(T item);
}