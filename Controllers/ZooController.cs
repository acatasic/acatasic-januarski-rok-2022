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

            var produkcijskeKuceNadjene=await Context.produkcijskeKuce.FindAsync(idProdKuce);
            var kat=await Context.kategorije.Include(p=>p.ProdukcijskaKuca).Where(p=>p.ProdukcijskaKuca.Contains(produkcijskeKuceNadjene)).ToListAsync();         
            
           
            return kat;
        }      
   
        [Route("UcitavanjeTriTopFilmaKategorije/{idKategorije}/{idProdKuce}")]
        [HttpGet]
        public async Task<Film[]> UcitavanjeTriTopFilmaKategorije(int idKategorije,int idProdKuce)
        {
                   
             var nadjeniFilmovi= await Context.filmovi.Where(p=>p.Kategorija.ID==idKategorije && p.ProdukcijskaKuca.ID==idProdKuce).ToListAsync();/////OVO RADI TAKO STO VRACA FILMOVE RESPONSE JE OVAKAV [{"id":1,"ime":"Armagedon","ocena":5,"brojOcena":3},{"id":1005,"ime":"jedno","ocena":9,"brojOcena":10},{"id":1006,"ime":"jedno","ocena":9,"brojOcena":10},{"id":1007,"ime":"jedno","ocena":9,"brojOcena":10},{"id":1008,"ime":"jedno","ocena":9,"brojOcena":10},{"id":1009,"ime":"jedno","ocena":9,"brojOcena":10}]
            Film[] TriNadjenaFilma=new Film[3];///niz 3 filma
            
            float maxOcena=-1;
            float minOcena=11;
            int duzinaNiza=nadjeniFilmovi.Count;

            foreach( Film element in nadjeniFilmovi)
            {
                if(element.Ocena>maxOcena) {
                    maxOcena=element.Ocena; 
                    TriNadjenaFilma[0]=element;
                }
                if(element.Ocena<minOcena) {
                    minOcena=element.Ocena;
                    TriNadjenaFilma[2]=element;
                }
            }
            for (int n=0;n<duzinaNiza-1;n++){
                for (int m=1;m<duzinaNiza;m++)
                {
                    if (nadjeniFilmovi[n].Ocena>nadjeniFilmovi[m].Ocena) 
                    {
                    Film t=nadjeniFilmovi[n];
                    nadjeniFilmovi[n]=nadjeniFilmovi[m];
                    nadjeniFilmovi[m]=t;
                    }
                }
            }
            TriNadjenaFilma[1]= nadjeniFilmovi[duzinaNiza/2 +1 ];//film koji je srednji po ocenama
        
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
            var pronadjenFilm = await Context.filmovi.FindAsync(idFilma);
            
            pronadjenFilm.Ocena = (ocena+pronadjenFilm.BrojOcena*pronadjenFilm.Ocena)/(pronadjenFilm.BrojOcena+1);
            pronadjenFilm.BrojOcena++;
            Context.Update<Film>(pronadjenFilm);
            await Context.SaveChangesAsync();
        }
    }
}
















/*///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
*////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/




















 





















































    
