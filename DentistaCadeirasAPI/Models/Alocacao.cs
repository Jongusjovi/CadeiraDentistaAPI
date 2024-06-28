namespace DentistaCadeirasAPI.Models
{
    public class Alocacao
    {
        public int Id { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public int CadeiraId { get; set; }
    }
}
