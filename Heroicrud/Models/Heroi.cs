using System.ComponentModel.DataAnnotations;

namespace Heroicrud.Models
{
    public class Heroi
    {
        [Key]
        public int Id { get; set; }
        public String Nome { get; private set; }
        public String NomeHeroi { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public Double Altura { get; private set; }
        public Double Peso { get; private set; }
        public bool Ativo { get; private set; }

        public Heroi( string nome, string nomeHeroi, DateTime dataNascimento, double altura, double peso)

        {
            Nome = nome;
            NomeHeroi = nomeHeroi;
            DataNascimento = dataNascimento;
            Altura = altura;
            Peso = peso;
            Ativo = true;
        }

        public void AtualizarDados(string nome, string nomeHeroi, DateTime dataNascimento, double altura, double peso) 
        {
            Nome = nome;
            NomeHeroi = nomeHeroi;
            DataNascimento = dataNascimento;
            Altura = altura;
            Peso = peso;
        }

    }
}
