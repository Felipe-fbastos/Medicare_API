using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class ParceiroUtilizadorDTO
    {
        public required int IdParceiro { get; set; }
        public required int IdColaborador { get; set; } 
    }
}