﻿using System.Collections.Generic;

namespace ProEventos.Application.Dtos
{
    public class PalestranteDto
    {
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemUrl { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}
