using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Remedio
    {
        public int Id { get; set; }
        public int IdGrandeza { get; set; }
        private TipoOrdemGrandeza grandeza { get; set; }
        public int IdLaboratorio { get; set; }
        private Laboratorio laboratorio { get; set; }
        public string Nome { get; set; }
        public string Anotacao { get; set; }
        public int Dosagem { get; set; }
        public DateTime DtRegistro { get; set; }
        public double QtdAlerta { get; set; }
        private List<Posologia> Posologias { get; set; } = new();
        private List<HistoricoPosologia> HistoricoPosologias { get; set; } = new();
        private List<Alarme> Alarmes { get; set; } = new();
        public List<Promocao> Promocaosromocaos { get; set; }

        

    }
}