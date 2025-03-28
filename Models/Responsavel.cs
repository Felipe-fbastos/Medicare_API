using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Responsavel
    {
        public int IdResponsavel { get; set; } 
        public Utilizador? ResponsavelUtilizador { get; set; }

        public int IdUtilizador { get; set; } 
        public Utilizador? Utilizador { get; set; }

        public int IdGrauParentesco { get; set; }
        public GrauParentesco? GrauParentesco { get; set; }

        public DateTime DcResponsavel { get; set; }
        public DateTime DuResponsavel { get; set; }
        public string StResponsavel { get; set; }

        public Responsavel(int idResponsavel, int idUtilizador, int idGrauParentesco, DateTime dcResponsavel, DateTime duResponsavel, string stResponsavel)
        {
            IdResponsavel = idResponsavel;
            IdUtilizador = idUtilizador;
            IdGrauParentesco = idGrauParentesco;
            DcResponsavel = dcResponsavel;
            DuResponsavel = duResponsavel;
            StResponsavel = stResponsavel;
        }

    }
}
