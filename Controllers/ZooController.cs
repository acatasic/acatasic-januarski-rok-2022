using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zoo.Models;

namespace zoo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class zooController : ControllerBase
    {
        public zooContext Context { get; set; }

        public zooController(zooContext context)
        {
            Context = context;
        }

        [Route("PreuzmiPKuce")]
        [HttpGet]
        public async Task<List<ProdukcijskaKuca>> PreuzmiPKuce()
        {
            return await Context.produkcijskeKuce.ToListAsync();
        }
    

        [Route("PreuzmiKategorije/{idProdKuce}")]
        [HttpGet]
        public async Task<List<Kategorija>> PreuzmiKategorije(int idProdKuce)
        {

            var produkcijskuKucuNadjene=await Context.produkcijskeKuce.FindAsync(idProdKuce);
            var kat=await Context.kategorije.Include(p=>p.ProdukcijskaKuca).Where(p=>p.ProdukcijskaKuca.Contains(produkcijskuKucuNadjene)).ToListAsync();         
             //////////// kako da povezes tabele bez tabele za vezu u c sharpu i kako da pristupis podacima preko liste
           
            return kat;
        }      
    



        




        /*[Route("UcitavanjeTriTopFilma/{idProdKuce}")]
        [HttpGet]
        public async Task<Film[]> UcitavanjeTriTopFilma(int idProdKuce)
        {
 
        var listaIDKategorija= await Context.kategorije.Where(p => p.produkcijskaKuca[1].ID  ==idProdKuce).ToListAsync();


        var nadjeniFilmovi= await Context.filmovi.Where(p=>listaIDKategorija.Contains(p.Kategorija)).ToListAsync();/////OVO RADI TAKO STO VRACA FILMOVE RESPONSE JE OVAKAV [{"id":1,"ime":"Armagedon","ocena":5,"brojOcena":3},{"id":1005,"ime":"j","ocena":9,"brojOcena":10},{"id":1006,"ime":"j","ocena":9,"brojOcena":10},{"id":1007,"ime":"j","ocena":9,"brojOcena":10},{"id":1008,"ime":"j","ocena":9,"brojOcena":10},{"id":1009,"ime":"j","ocena":9,"brojOcena":10}]
        Film[] TriNadjenaFilma=new Film[3];///niz 3 filma
            
            float maxOcena=-1;
            float minOcena=11;
            foreach( Film element in nadjeniFilmovi)
            {
                if(element.Ocena>maxOcena){ maxOcena=element.Ocena; TriNadjenaFilma[0]=element;}
                if(element.Ocena<minOcena) {
                    minOcena=element.Ocena;
                    TriNadjenaFilma[2]=element;
                    TriNadjenaFilma[1]=element;
                }
            }
        return TriNadjenaFilma;
        }
*/



        [Route("UcitavanjeTriTopFilmaKategorije/{idKategorije}/{idProdKuce}")]
        [HttpGet]
        public async Task<Film[]> UcitavanjeTriTopFilmaKategorije(int idKategorije,int idProdKuce)
        {
          
            var nadjeniFilmovi= await Context.filmovi.Where(p=>p.Kategorija.ID==idKategorije && p.ProdukcijskaKuca.ID==idProdKuce).ToListAsync();/////OVO RADI TAKO STO VRACA FILMOVE RESPONSE JE OVAKAV [{"id":1,"ime":"Armagedon","ocena":5,"brojOcena":3},{"id":1005,"ime":"jedno","ocena":9,"brojOcena":10},{"id":1006,"ime":"jedno","ocena":9,"brojOcena":10},{"id":1007,"ime":"jedno","ocena":9,"brojOcena":10},{"id":1008,"ime":"jedno","ocena":9,"brojOcena":10},{"id":1009,"ime":"jedno","ocena":9,"brojOcena":10}]
            Film[] TriNadjenaFilma=new Film[3];///niz 3 filma
            
            float maxOcena=-1;
            float minOcena=11;
            float trenutnaOcena=0;//sluzi za prosecnu ocenu
            int i=1;

            foreach( Film element in nadjeniFilmovi)
            {
                if(element.Ocena>=maxOcena){ maxOcena=element.Ocena; TriNadjenaFilma[0]=element;}
                if(element.Ocena<minOcena) {
                    minOcena=element.Ocena;
                    TriNadjenaFilma[1]=element;
                }
               
                if (i<=nadjeniFilmovi.Count/2) TriNadjenaFilma[2]=element;
                if (element.Ocena>=trenutnaOcena) {i++; trenutnaOcena = element.Ocena;}
            }
        return TriNadjenaFilma;
        }

    
        [Route("PreuzmiFilmove/{idKategorije}/{idProdKuce}")]
        [HttpGet]
        public async Task<List<Film>> PreuzmiFilmove(int idKategorije,int idProdKuce)
        {
            return await Context.filmovi.Where(p => p.Kategorija.ID==idKategorije && p.ProdukcijskaKuca.ID==idProdKuce).ToListAsync();
        }

    
        [Route("IzmenaPodatakaOFilmu/{ocena}/{idFilma}")]
        [HttpPut]
        public async Task IzmenaPodatakaOFilmu(float ocena,int idFilma)
        {
            var stariFilm = await Context.filmovi.FindAsync(idFilma);
            
            stariFilm.Ocena = (ocena+stariFilm.BrojOcena*stariFilm.Ocena)/(stariFilm.BrojOcena+1);
            stariFilm.BrojOcena++;
            Context.Update<Film>(stariFilm);
            await Context.SaveChangesAsync();
        }
    }
}




































 /*Nije bitno sta je u bazi na azure serveru, bitno je kako isprojektujes sistem , a posle kako se napravi migracija, to nije bitno sad za mene*/


/*var id = 1;
var query =
   from post in database.Posts
   join meta in database.Post_Metas on post.ID equals meta.Post_ID
   where post.ID == id
   select new { Post = post, Meta = meta };
If you're really stuck on using lambdas though, your syntax is quite a bit off. Here's the same query, using the LINQ extension methods:
  ++ 
   





var id = 1;
var query = database.Posts    // your starting point - table in the "from" statement
   .Join(database.Post_Metas, // the source table of the inner join
      post => post.ID,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
      meta => meta.Post_ID,   // Select the foreign key (the second part of the "on" clause)
      (post, meta) => new { Post = post, Meta = meta }) // selection
   .Where(postAndMeta => postAndMeta.Post.ID == id);  

*/
















    