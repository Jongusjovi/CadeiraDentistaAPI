namespace DentistaCadeirasAPI.Models
{
    public class Cadeira
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Descricao { get; set; }
        public string Fabricante { get; set; }
        public DateTime UltimaManutencao { get; set; }
        public DateTime ProximaManutencao { get; set; }
        public List<Alocacao> Alocacoes { get; set; } = new List<Alocacao>();

        public TimeSpan TempoTotalDeLocacaoNoDia(DateTime data)
        {
            return Alocacoes
                .Where(a => a.Inicio.Date == data.Date)
                .Aggregate(TimeSpan.Zero, (total, locacao) => total + (locacao.Fim - locacao.Inicio));
        }
    }
}
