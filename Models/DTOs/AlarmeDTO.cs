using System;
using System.ComponentModel.DataAnnotations;

namespace Medicare_API.Models
{
    
    public class AlarmeDTO
    {
        public required int IdAlarme { get; set; }
        public required int IdPosologia { get; set; }
        public required int IdRemedio { get; set; } 
        public required DateTime DtHoraAlarme { get; set; }
        public required string StAlarme { get; set; } 
    }
}