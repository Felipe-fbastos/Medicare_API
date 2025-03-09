using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Cuidador
    {
        private int IdCuidador { get; set; }
        private Utilizador cuidador { get; set; }
        private int IdUtilizador { get; set; }
        private Utilizador Utilizador { get; set; }
        private DateTime DtInicio { get; set; }
        private DateTime DtFim { get; set; }
        private DateTime DcCuidador { get; set; }
        private DateTime DuCuidador { get; set; }
        private string Situacao { get; set; }
    }
}