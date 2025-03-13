using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Responsavel
    {
        public int IdUtilizador { get; set; }
        public Utilizador Utilizador { get; set; }
        public int IdResponsavel { get; set; }
        public Utilizador responsavel { get; set; }
        public int IdGrauParentesco { get; set; }
        public GrauParentesco GrauParentesco { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DtUltimaAlteracao { get; set; }
        public string Situacao { get; set; }
       
    }
}