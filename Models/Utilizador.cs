using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Utilizador
    {
        public int Id { get; set; }        
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int IdTipoUtilizador { get; set; }
        public TipoUtilizador TipoUtilizador { get; set; }
        public List<Cuidador> Cuidadores { get; set; } = new(); //Instanciando a classe para evitar problemas de acesso [NullReferenceException]
        public List<Responsavel> Responsaveis { get; set; } = new();
        public List<ParceiroUtilizador> ParceiroUtilizadores { get; set; } = new();
        public List<Posologia> Posologias { get; set; } = new();
        public List<Promocao> Promocoes { get; set; }




    }
}