using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Remedio
    {
        private int Id { get; set; }
        private int IdGrandeza { get; set; }
        private TipoOrdemGrandeza grandeza { get; set; }
        private int IdLaboratorio { get; set; }
        private Laboratorio laboratorio { get; set; }
        private string Nome { get; set; }
        private string Anotacao { get; set; }
        private int Dosagem { get; set; }
        private DateTime DtRegistro { get; set; }
        private double QtdAlerta { get; set; }
        private List<Posologia> Posologias { get; set; } = new();
        private List<HistoricoPosologia> HistoricoPosologias { get; set; } = new();
        private List<Alarme> Alarmes { get; set; } = new();
        public List<Promocao> Promocaosromocaos { get; set; }

        

    }
}