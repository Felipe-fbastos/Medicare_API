using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class TipoUtilizador
    {
        private int Id { get; set; }
        private string Descricao { get; set; }
        public List<Utilizador> Utilizadores { get; set; }
    }
}