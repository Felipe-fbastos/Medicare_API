using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class Posologia
    {
        [Key]
        public int IdPosologia { get; set; }
        public int IdRemedio { get; set; }  // Chave estrangeira para Remedio
        public Remedio remedio { get; set; }  // Relacionamento com Remedio
        public int IdUtilizador { get; set; }  // Chave estrangeira para Utilizador
        public Utilizador utilizador { get; set; }  // Relacionamento com Utilizador
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public int Intervalo { get; set; }
        public int QtdRemedio { get; set; }
        public List<HistoricoPosologia> HistoricoPosologias { get; set; } = new();  // Relacionamento com HistoricoPosologia
        public List<Alarme> Alarmes { get; set; } = new();  // Relacionamento com Alarme
    }
}
