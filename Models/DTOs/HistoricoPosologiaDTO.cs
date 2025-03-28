using System;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class HistoricoPosologiaDTO
    {
        public required int IdPosologia { get; set; }
        public required int IdRemedio { get; set; }
        public required int SdPosologia { get; set; }  
    }
}
