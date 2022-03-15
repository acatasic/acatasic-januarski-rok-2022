using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace zoo.Models{
    [Table("ProdKuca")]
    public class ProdukcijskaKuca
    {
        [Key]
        [Column("ID")]    
        public int ID{get;set;}

        [StringLength(100)] 
        [Required(ErrorMessage="Neophodno je uneti naziv !")]
        [Column("Naziv")]
        public string Naziv{get;set;}

         ///////UMESTO JSON IGNORE TREBA NOT MAPPED,TAKO PRAVI DODATNU TABELU SPOJA. Tabel se pravi automatski
        //public virtual Kategorija Kategorija{get;set;}
        [JsonIgnore]/////mora da se doda json ignore
        public List<Kategorija>  Kategorija{get;set;}

      /*  [JsonIgnore]
        public Kategorija KategorijaID{get;set;}      */
    public ProdukcijskaKuca()
    {
        this.Kategorija = new List<Kategorija>();
    }
    }
}