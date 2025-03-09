using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Posologia
    {
        private int Id { get; set; }
        private int IdRemedio { get; set;} 
        private Remedio remedio { get; set; }
        private int IdUtilizador { get; set; }
        private Utilizador Utilizador { get; set; }
        private DateTime DtInicio { get; set; }
        private DateTime DtFim { get; set; }
        private int Intervalo { get; set; }
        private int QtdRemedio { get; set; }
        private List<HistoricoPosologia> HistoricoPosologias { get; set; } = new();
        private List<Alarme> Alarmes { get; set; } = new();
    }
}