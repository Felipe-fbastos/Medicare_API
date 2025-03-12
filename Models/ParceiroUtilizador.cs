using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class ParceiroUtilizador
    {
        public int IdParceiro { get; set; }
        public Parceiro Parceiro { get; set; }
        public int IdColaborador { get; set; }
        public Utilizador colaborador { get; set; }
        
    }
}