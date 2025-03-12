using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Cuidador
    {
        public int IdCuidador { get; set; }
        public Utilizador cuidador { get; set; }
        public int IdUtilizador { get; set; }
        public Utilizador Utilizador { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public DateTime DcCuidador { get; set; }
        public DateTime DuCuidador { get; set; }
        public string Situacao { get; set; }
    }
}