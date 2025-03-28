using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Cuidador
    {
        public int IdCuidador { get; set; }
        public Utilizador? CuidadorUtilizador { get; set; }

        public int IdUtilizador { get; set; }
        public Utilizador? Utilizador { get; set; }

        public DateTime DtInicio { get; set; }
        public DateTime? DtFim { get; set; }
        public DateTime DcCuidador { get; set; }
        public DateTime DuCuidador { get; set; }
        public string StCuidador { get; set; }

        public Cuidador(int idCuidador, int idUtilizador, DateTime dtInicio, DateTime? dtFim, DateTime dcCuidador, DateTime duCuidador, string stCuidador)
        {
            IdCuidador = idCuidador;
            IdUtilizador = idUtilizador;
            DtInicio = dtInicio;
            DtFim = dtFim;
            DcCuidador = dcCuidador;
            DuCuidador = duCuidador;
            StCuidador = stCuidador;
        }

    }
}
