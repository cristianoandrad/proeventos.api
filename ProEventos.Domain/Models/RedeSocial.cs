using ProEventos.Domain.Models.Base;

namespace ProEventos.Domain.Models
{
    public class RedeSocial : Entidade
    {
        public string Nome { get; set; }
        public string Url { get; set; }
        public int? EventoId { get; set; }
        public Evento Evento { get; set; }
        public int? PalestranteId { get; set; }
        public Palestrante Palestrante { get; set; }
    }
}
