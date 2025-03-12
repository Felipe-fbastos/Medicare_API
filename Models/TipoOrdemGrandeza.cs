using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class TipoOrdemGrandeza
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Remedio> Remedios { get; set; }

    }
}