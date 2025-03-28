using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Medicare_API.Models
{
    public class TipoUtilizador
    {
        public  int IdTipoUtilizador { get; set; }
        public required string Descricao { get; set; }

        //Relacionamentos
        [JsonIgnore]
        public List<Utilizador> Utilizadores { get; set; } = new(); 
    }
}