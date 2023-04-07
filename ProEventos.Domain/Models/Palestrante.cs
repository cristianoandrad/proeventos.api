using ProEventos.Domain.Identity;
using ProEventos.Domain.Models.Base;
using System.Collections.Generic;

namespace ProEventos.Domain.Models
{
    public class Palestrante : Entidade
    {
        public Palestrante()
        {
            RedesSociais = new List<RedeSocial>();
        }
        public string MiniCurriculo { get; set; }        
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}
