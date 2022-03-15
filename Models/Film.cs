using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace zoo.Models{
    [Table("Film")]
    public class Film
    {
        [Key]
        [Column("ID")]    
        public int ID{get;set;}

        [Column("Ime")]
        public string Ime{get;set;}
        

        [Column("Ocena")]
        public float Ocena{get;set;}

        [Column("BrojOcena")]
        public int BrojOcena{get;set;}

        [JsonIgnore]    
        public virtual Kategorija Kategorija{get;set;}

        [JsonIgnore]  
        public virtual ProdukcijskaKuca ProdukcijskaKuca{get;set;}

        public Film()
        {
            
        }
    }
}