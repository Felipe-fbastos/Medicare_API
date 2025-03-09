using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Responsavel
    {
        private int IdUtilizador { get; set; }
        private Utilizador Utilizador { get; set; }
        private int IdResponsavel { get; set; }
        private Utilizador responsavel { get; set; }
        private int IdGrauParentesco { get; set; }
        private GrauParentesco GrauParentesco { get; set; }
        private DateTime DtCadastro { get; set; }
        private DateTime DtUltimaAlteracao { get; set; }
        private string Situacao { get; set; }
        public GrauParentesco GrauParentescos { get; set; }
    }
}