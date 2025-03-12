using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class GrauParentesco
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Responsavel> Responsavel { get; set; }

        
    }

}