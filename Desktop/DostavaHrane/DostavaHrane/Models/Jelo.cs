using System.ComponentModel.DataAnnotations.Schema;

namespace DostavaHrane.Models
{
    public class Jelo
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }


        [ForeignKey("RestoranID")] 
        public int RestoranID { get; set; }

        public Restoran Restoran { get; set; }

        public Jelo()
        {

        }
    }
}
