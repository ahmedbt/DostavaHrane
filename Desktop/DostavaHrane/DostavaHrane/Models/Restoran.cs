namespace DostavaHrane.Models
{
    public class Restoran
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Telefon { get; set; }

        public ICollection<Jelo> Jela { get; set; }

        public Restoran()
        {

        }
    }
}
