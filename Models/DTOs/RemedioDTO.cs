using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class RemedioDTO
    {
        public required int IdTipoOrdemGrandeza { get; set; }
        public required int IdLaboratorio { get; set; }
        public required string NomeRemedio { get; set; }
        public required string Anotacao { get; set; }
        public required int Dosagem { get; set; }
        public required DateTime DtRegistro { get; set; }
        public required double QtdAlerta { get; set; }
    }
}
