using System.ComponentModel.DataAnnotations;

namespace Heroicrud.Models
{
    public class SuperPoder
    {
        [Key]
        public int Id { get; set; }
        public String Poder { get; private set; }
        public String Descricao { get; private set; }



        public SuperPoder(string poder, string descricao)

        {
            Poder = poder;
            Descricao = descricao;
           
        }

     
    }
}
