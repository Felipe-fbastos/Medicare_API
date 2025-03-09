using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class ParceiroUtilizador
    {
        private int IdParceiro { get; set; }
        public Parceiro Parceiro { get; set; }
        private int IdColaborador { get; set; }
        private Utilizador colaborador { get; set; }
        
    }
}