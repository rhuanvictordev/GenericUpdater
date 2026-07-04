using AtualizadorGenerico.Models;

namespace AtualizadorGenerico.Repository
{
    public interface IProgramaRepository
    {
        public Programa CarregarPrograma(string AppKeyName);
    }
}
