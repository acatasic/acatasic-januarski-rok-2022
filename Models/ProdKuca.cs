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

         
        //public virtual Kategorija Kategorija{get;set;}
        [JsonIgnore]/////mora da se doda json ignore, tabela spoja se pravi automatski
        public List<Kategorija>  Kategorija{get;set;}

      /*  [JsonIgnore]
        public Kategorija KategorijaID{get;set;}      */
    public ProdukcijskaKuca()
    {
        this.Kategorija = new List<Kategorija>();
    }
    }
}
