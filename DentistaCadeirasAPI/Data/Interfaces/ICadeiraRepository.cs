using DentistaCadeirasAPI.Models;

namespace DentistaCadeirasAPI.Data.Interfaces
{
    public interface ICadeiraRepository
    {
        Task<IEnumerable<Cadeira>> GetCadeirasAsync();
        Task<IEnumerable<Cadeira>> GetCadeirasDisponiveisAsync(DateTime inicio, DateTime fim);
        Task<Cadeira> GetCadeiraByIdAsync(int id);
        Task AddCadeiraAsync(Cadeira cadeira);
        Task<Alocacao> AddAlocacaoCadeiraAsync(Alocacao alocacao);
        Task DeleteCadeiraAsync(int id);
        Task UpdateCadeiraAsync(Cadeira cadeira);
        Task<bool> CadeiraExistsAsync(int id);
    }
}
