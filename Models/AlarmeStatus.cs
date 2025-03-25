using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Medicare_API.Models
{
    public class AlarmeStatus
    {
        [Key]
        public int IdAlarmeStatus { get; set; }
        public string Descricao { get; set; }
        public List<Alarme> Alarmes { get; set; } = new();  // Lista de Remedios
    }
}
