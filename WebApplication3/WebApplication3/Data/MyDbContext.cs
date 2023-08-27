using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
      

    }
}
