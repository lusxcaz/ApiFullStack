using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heroicrud.Models
{
    public class HeroiSuperPoderes
    {
        [Key]
        public int Id { get; set; }

        public int HeroiId { get; private set; }

        public int SuperPoderId { get; private set; }


        public HeroiSuperPoderes(int heroiId, int superPoderId)
        {
            HeroiId = heroiId;
            SuperPoderId = superPoderId;
        }

        public void AtualizarDados(int heroiId, int superPoderId)
        {
            HeroiId = heroiId;
            SuperPoderId = superPoderId;
        }
    }
}
