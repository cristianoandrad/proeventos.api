using ProEventos.Domain.Models;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository.Interfaces
{
    public interface ILoteRepository : IBaseRespository
    {
        /// <summary>
        /// Método que retorna uma lista de lotes por evento
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Lote</param>
        /// <returns>Lista de lotes</returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);

        /// <summary>
        /// Métoro que retorna um lote
        /// </summary>
        /// <param name="eventoId">Código chave da tabela Evento</param>
        /// <param name="id">Código chave da tabela Lote</param>
        /// <returns>Apenas um lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int id);
    }
}
