using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class PosologiaDTO
    {
        public required int IdPosologia { get; set; }
        public required int IdRemedio { get; set; } 
        public required int IdUtilizador { get; set; } 
        public required DateTime DtInicio { get; set; }
        public DateTime? DtFim { get; set; }
        public required int Intervalo { get; set; }
        public required int QtdRemedio { get; set; } 
    }
}