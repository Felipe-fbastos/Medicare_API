using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Medicare_API.Data;

namespace Medicare_API.Models
{
    public class Utilizador
    {
        public int IdUtilizador { get; set; }   //PK
        public  int IdTipoUtilizador { get; set; }  //FK
        public TipoUtilizador? TipoUtilizador { get; set; } // FK - Navegação para TipoUtilizador 

        // CPF será único para cada combinação de IdUtilizador + IdTipoUtilizador
        public  string CPF { get; set; }
        public  string Nome { get; set; }
        public  string Sobrenome { get; set; }
        public  DateTime DtNascimento { get; set; }
        public  string Email { get; set; }
        public string Telefone { get; set; }
        // Futuro colocar perfil médico -> Altura, peso, tipoSanguineo, alergia...

        //Relacionamentos
        public List<Cuidador> Cuidadores { get; set; } = new();
        public List<Responsavel> Responsaveis { get; set; } = new();
        public List<ParceiroUtilizador> ParceiroUtilizadores { get; set; } = new();
        public List<Posologia> Posologias { get; set; } = new();
        public List<Promocao> Promocoes { get; set; } = new();

        public Utilizador(int idTipoUtilizador, string cpf, string nome, string sobrenome, DateTime dtNascimento, string email, string telefone)
        {
            IdTipoUtilizador = idTipoUtilizador;
            CPF = cpf;
            Nome = nome;
            Sobrenome = sobrenome;
            DtNascimento = dtNascimento;
            Email = email;
            Telefone = telefone;
        }

    }
}