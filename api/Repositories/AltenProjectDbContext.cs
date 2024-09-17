namespace AltenProject.Repositories;
using Microsoft.EntityFrameworkCore;

public class AltenProjectDbContext : DbContext
{
    public AltenProjectDbContext(DbContextOptions<AltenProjectDbContext> options)
       : base(options)
    {

    }
}