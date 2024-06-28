using DentistaCadeirasAPI.Data.Interfaces;
using DentistaCadeirasAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DentistaCadeirasAPI.Data.Repositories
{
    public class CadeiraRepository : ICadeiraRepository
    {
        private readonly DentistaCadeirasContext _context;

        public CadeiraRepository(DentistaCadeirasContext context)
        {
            _context = context;
        }

        public async Task AddCadeiraAsync(Cadeira cadeira)
        {
            _context.Cadeiras.Add(cadeira);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CadeiraExistsAsync(int id)
        {
            return await _context.Cadeiras.AnyAsync(c => c.Id == id);
        }

        public async Task DeleteCadeiraAsync(int id)
        {
            var cadeira = await _context.Cadeiras.FindAsync(id);

            if (cadeira != null)
            {
                _context.Cadeiras.Remove(cadeira);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cadeira> GetCadeiraByIdAsync(int id)
        {
            return await _context.Cadeiras.FindAsync(id);
        }

        public async Task<IEnumerable<Cadeira>> GetCadeirasAsync()
        {
            return await _context.Cadeiras.Include(c => c.Alocacoes).ToListAsync();
        }

        public async Task<IEnumerable<Cadeira>> GetCadeirasDisponiveisAsync(DateTime inicio, DateTime fim)
        {
            return await _context.Cadeiras.Include(c => c.Alocacoes)
                .Where(c => !c.Alocacoes.Any(a => a.Inicio < fim && a.Fim > inicio))
                .ToListAsync();
        }

        public async Task UpdateCadeiraAsync(Cadeira cadeira)
        {
            _context.Entry(cadeira).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Alocacao> AddAlocacaoCadeiraAsync(Alocacao alocacao)
        {
            _context.Alocacoes.Add(alocacao);
            await _context.SaveChangesAsync();
            return alocacao;
        }
    }
}
