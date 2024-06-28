using DentistaCadeirasAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentistaCadeirasAPI.Services.Interfaces
{
    public interface ICadeiraService
    {
        Task<IEnumerable<Cadeira>> GetCadeirasAsync();
        Task<Cadeira> GetCadeiraByIdAsync(int id);
        Task AddCadeiraAsync(Cadeira cadeira);
        Task<Alocacao> AddAlocacaoCadeiraAsync(DateTime inicio, DateTime fim);
        Task DeleteCadeiraAsync(int id);
        Task UpdateCadeiraAsync(Cadeira cadeira);
        Task<bool> CadeiraExistsAsync(int id);
    }
}
