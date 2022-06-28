using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace RickNMortyApp.Models
{
    [Table("character")]
    [JsonObject]
    public class Characters
    {
        [JsonProperty("info")]
        public Info info { get; set; }

        [JsonProperty("results")]
        public List<Results> Results { get; set; }

    }

    public class Info
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
    }

    [Keyless]
    public class Results
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [DisplayName("Name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("species")]
        public string Species { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("origin")]
        public Origin Origin { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("episode")]
        public List<string> Episode { get; set; }

        public int Origin_id { get; set; }
        public int Location_id { get; set; }
        public int Episode_id { get; set; }

        public ICollection<Origin> Origins{ get; set; }
        public Results()
        {
            Origins = new Collection<Origin>();
        }
    }

}
