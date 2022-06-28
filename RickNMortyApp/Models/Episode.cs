using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RickNMortyApp.Models
{
    [Table("episode")]
    [JsonObject]
    public class Episode
    {
        public int Id { get; set; }
        public string Url { get; set; }

        

    }
}
