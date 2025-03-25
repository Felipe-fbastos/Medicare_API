using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class Remedio
    {
        [Key]
        public int IdRemedio { get; set; }
        public int IdTipoOrdemGrandeza { get; set; }  // Renomeado para IdTipoOrdemGrandeza
        public TipoOrdemGrandeza Grandeza { get; set; }  // Relacionamento com TipoOrdemGrandeza

        public int IdLaboratorio { get; set; }  // Renomeado para IdLaboratorio
        public Laboratorio laboratorio { get; set; }  // Relacionamento com Laboratorio
        public string NomeRemedio { get; set; }
        public string Anotacao { get; set; }
        public int Dosagem { get; set; }
        public DateTime DtRegistro { get; set; }
        public double QtdAlerta { get; set; }

        public List<Posologia> Posologias { get; set; } = new();  // Relacionamento com Posologia
        public List<HistoricoPosologia> HistoricoPosologias { get; set; } = new();  // Relacionamento com HistoricoPosologia
        public List<Alarme> Alarmes { get; set; } = new();  // Relacionamento com Alarme
        public List<Promocao> Promocoes { get; set; } = new();  // Relacionamento com Promocao
    }
}
