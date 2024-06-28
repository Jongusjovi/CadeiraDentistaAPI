using DentistaCadeirasAPI.Controllers;
using DentistaCadeirasAPI.Data.Interfaces;
using DentistaCadeirasAPI.Models;
using DentistaCadeirasAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DentistaCadeirasAPI.Services
{
    public class CadeiraService : ICadeiraService
    {
        private readonly ICadeiraRepository _cadeiraRepository;

        public CadeiraService(ICadeiraRepository cadeiraRepository)
        {
            _cadeiraRepository = cadeiraRepository;
        }

        public async Task AddCadeiraAsync(Cadeira cadeira)
        {
            if (cadeira == null)
            {
                throw new ArgumentNullException(nameof(cadeira), "A cadeira não pode ser nula");
            }

            if (cadeira.Numero <= 0)
            {
                throw new ArgumentException("O número da cadeira deve ser um número inteiro positivo", nameof(cadeira.Numero));
            }

            await _cadeiraRepository.AddCadeiraAsync(cadeira);
        }

        public async Task<bool> CadeiraExistsAsync(int id)
        {
            return await _cadeiraRepository.CadeiraExistsAsync(id);
        }

        public async Task DeleteCadeiraAsync(int id)
        {
            await _cadeiraRepository.DeleteCadeiraAsync(id);
        }

        public async Task<Cadeira> GetCadeiraByIdAsync(int id)
        {
            return await _cadeiraRepository.GetCadeiraByIdAsync(id);
        }

        public async Task<IEnumerable<Cadeira>> GetCadeirasAsync()
        {
            return await _cadeiraRepository.GetCadeirasAsync();
        }

        public async Task UpdateCadeiraAsync(Cadeira cadeira)
        {
            if (cadeira == null)
            {
                throw new ArgumentNullException(nameof(cadeira), "A cadeira não pode ser nula");
            }

            if (cadeira.Numero <= 0)
            {
                throw new ArgumentException("O número da cadeira deve ser um número inteiro positivo", nameof(cadeira.Numero));
            }

            await _cadeiraRepository.UpdateCadeiraAsync(cadeira);
        }

        public async Task<Alocacao> AddAlocacaoCadeiraAsync(DateTime inicio, DateTime fim)
        {
            // Este método deve consultar e verificar as cadeiras disponíveis para alocação.
            // Caso haja mais de uma cadeira disponível, é selecionada a cadeira com o menor tempo de alocação no dia da data informada,
            // balanceando as alocações de forma justa.

            var cadeirasDisponiveis = (await _cadeiraRepository.GetCadeirasDisponiveisAsync(inicio, fim)).ToList();

            if (cadeirasDisponiveis.Count == 0)
            {
                return null;
            }

            var cadeiraSelecionada = cadeirasDisponiveis.Count == 1 ? cadeirasDisponiveis[0] : 
                                        cadeirasDisponiveis.OrderBy(c => c.TempoTotalDeLocacaoNoDia(inicio)).FirstOrDefault();

            var alocacao = new Alocacao { Inicio = inicio, Fim = fim, CadeiraId = cadeiraSelecionada.Id };

            return await _cadeiraRepository.AddAlocacaoCadeiraAsync(alocacao);
        }
    }
}
