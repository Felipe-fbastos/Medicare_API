using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Posologia
    {
        public int Id { get; set; }
        public int IdRemedio { get; set;} 
        private Remedio remedio { get; set; }
        public int IdUtilizador { get; set; }
        private Utilizador Utilizador { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public int Intervalo { get; set; }
        public int QtdRemedio { get; set; }
        private List<HistoricoPosologia> HistoricoPosologias { get; set; } = new();
        private List<Alarme> Alarmes { get; set; } = new();
    }
}