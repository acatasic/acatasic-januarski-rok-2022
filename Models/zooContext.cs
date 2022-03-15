using Microsoft.EntityFrameworkCore;

namespace zoo.Models
{
    public class zooContext:DbContext
    {
        public DbSet<ProdukcijskaKuca> produkcijskeKuce{get;set;}
        public DbSet<Kategorija> kategorije{get;set;}
        public DbSet<Film> filmovi {get;set;}

     /*   public DbSet<KategorijaProdKuca> kategorijaProdukcijskaKuca {get;set;}*/
        
        public zooContext(DbContextOptions options):base(options)
        {

        }

    }
}