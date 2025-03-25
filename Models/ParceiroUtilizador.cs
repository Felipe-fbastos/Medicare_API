using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class ParceiroUtilizador
    {
        [Key]
        [Column(Order = 1)]
        public int IdParceiro { get; set; }
        public Parceiro Parceiro { get; set; }
        [Key]
        [Column(Order = 2)]
        public int IdUtilizador { get; set; }
        public Utilizador colaborador { get; set; }
        
    }
}