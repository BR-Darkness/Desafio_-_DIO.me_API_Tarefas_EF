using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.DTOs
{
    public class TarefaDTO
    {
        public string Titulo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public DateTime Data { get; set; } = default!;
        public EnumStatusTarefa Status { get; set; } = default!;
    }
}