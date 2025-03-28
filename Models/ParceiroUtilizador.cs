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
        public int IdParceiro { get; set; } // PK - FK
        public Parceiro? Parceiro { get; set; }//FK - Relacionamento com Parceiro 

        public int IdColaborador { get; set; } // PK - FK
        public Utilizador? Colaborador { get; set; }//FK - Relacionamento com Utilizador 

        public ParceiroUtilizador(int idParceiro, int idColaborador)
        {
            IdParceiro = idParceiro;
            IdColaborador = idColaborador;
        }

    }
}