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
        public Remedio Remedio { get; set; }
        public int IdUtilizador { get; set; }
        public Utilizador Utilizador { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public int Intervalo { get; set; }
        public int QtdRemedio { get; set; }
        public List<HistoricoPosologia> HistoricoPosologias { get; set; } = new();
        public List<Alarme> Alarmes { get; set; } = new();
    }
}