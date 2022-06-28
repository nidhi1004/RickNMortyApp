using Newtonsoft.Json;

namespace RickNMortyApp.Models
{
    [JsonObject]
    public class Input
    {
        public Input()
        {

        }
        //public Input(List<Characters> characters, List<Origin> origin, List<Location> location, List<Episode> episode)
        //{
        //    Characters = characters;
        //    Origin = origin;
        //    Location = location;
        //    Episode = episode;
        //}

        [JsonProperty("info")]
        public Info1 info { get; set; }

        [JsonProperty("results")]
        public List<Results> Characters { get; set; }
        //public List<Origin> Origin { get; set; }
        //public List<Location> Location { get; set; }
        //public List<Episode> Episode { get; set; }

    }

    public class Info1
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
    }
}
