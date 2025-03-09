using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Utilizador
    {
        private int Id { get; set; }        
        private string Nome { get; set; }
        private string Sobrenome { get; set; }
        private string CPF { get; set; }
        private string Email { get; set; }
        private string Telefone { get; set; }
        private int IdTipoUtilizador { get; set; }
        private TipoUtilizador TipoUtilizador { get; set; }
        private List<Cuidador> Cuidadores { get; set; } = new(); //Instanciando a classe para evitar problemas de acesso [NullReferenceException]
        private List<Responsavel> Responsaveis { get; set; } = new();
        private List<ParceiroUtilizador> ParceiroUtilizadores { get; set; } = new();
        private List<Posologia> Posologias { get; set; } = new();
        public List<Promocao> Promocoes { get; set; }




    }
}