using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models.DTOs
{
    public class UtilizadorAutenticarDTO
    {
        public required string Nome {get; set; }
        public required string SenhaString  { get; set; }
    }
}