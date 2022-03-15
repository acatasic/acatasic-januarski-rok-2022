using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace zoo.Models{
    [Table("Kategorija")]
    public class Kategorija
    {
        [Key]
        [Column("ID")]    
        public int ID{get;set;}

        [Column("Tip")]
        public string Tip{get;set;}

        [JsonIgnore]
        public List<ProdukcijskaKuca> ProdukcijskaKuca{get;set;}
     
        public Kategorija()
        {
       
           this.ProdukcijskaKuca=new List<ProdukcijskaKuca>();
        }
    }
}
//////////U SUSTINI PK I KATEGORIJA VEZA SPOJA I FILM IMA LISTU