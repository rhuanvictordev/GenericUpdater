namespace AtualizadorGenerico.Models
{
    public class Programa
    {
        public string Nome { get; set; }
        public string Versao { get; set; }

        public Programa(){}

        public Programa(string nome, string versao)
        {
            this.Nome = nome;
            this.Versao = versao;
        }
    }
}
