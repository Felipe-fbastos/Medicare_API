using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class TipoOrdemGrandeza
    {
        private int Id { get; set; }
        private string Descricao { get; set; }
        private List<Remedio> Remedios { get; set; }

    }
}