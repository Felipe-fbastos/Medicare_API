using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Responsavel
    {
        public int IdUtilizador { get; set; }
        private Utilizador Utilizador { get; set; }
        public int IdResponsavel { get; set; }
        private Utilizador responsavel { get; set; }
        public int IdGrauParentesco { get; set; }
        private GrauParentesco GrauParentesco { get; set; }
        private DateTime DtCadastro { get; set; }
        private DateTime DtUltimaAlteracao { get; set; }
        public string Situacao { get; set; }
        private GrauParentesco GrauParentescos { get; set; }
    }
}