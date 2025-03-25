using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Medicare_API.Models
{
    public class TipoUtilizador
    {
        [Key]
        public int IdTipoUtilizador { get; set; }
        public string Descricao { get; set; }
        [JsonIgnore]
        public List<Utilizador> Utilizadores { get; set; } = new(); // Relacionamento 1:N com Utilizadores
    }
}