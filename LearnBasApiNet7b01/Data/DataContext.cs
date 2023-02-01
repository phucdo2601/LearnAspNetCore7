using Microsoft.EntityFrameworkCore;

namespace LearnBasApiNet7b01.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }

        //co the set chuoi ket noi trong class DbContext
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-7CKON28R\\SQLEXPRESS;Initial Catalog=LearnEnFrameCoreV7B01;User ID=sa;Password=12345678;Encrypt=false");
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
