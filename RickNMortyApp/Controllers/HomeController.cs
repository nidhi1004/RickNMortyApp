using Microsoft.AspNetCore.Mvc;
using RickNMortyApp.Models;
using System.Diagnostics;
using RickNMortyApp.Context;
using Microsoft.AspNetCore.Html;
using Microsoft.EntityFrameworkCore;
using Results = RickNMortyApp.Models.Results;
using System.Net.Http;

namespace RickNMortyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DBContext Context { get; }
        private List<Origin> origin = new List<Origin>();
        public HomeController(ILogger<HomeController> logger, DBContext dBContext)
        {
            _logger = logger;
            this.Context = dBContext;
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, string name, string species,string type, string gender, string status,string imageurl,string oName, string lName, string episode )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Origin origin = new Origin() { Name = oName };
                    Location location = new Location() { Name = lName };
                    Episode episode1 = new Episode() { Id = +1, Url = episode };
                    var chars = new Results()
                    {
                        Id = id, Name = name, Species = species, Status = status, Image = imageurl, Type = type, Origin = origin, Location = location, Episode = new List<string>() { episode }, Created = (DateTime.UtcNow).ToString() 
                    };
                    Context.CharacterTable.Update(chars);
                    Context.Origin.Update(origin);
                    Context.Location.Update(location);
                    Context.Episode.UpdateRange(episode1);
                    Context.SaveChanges();

                }
                catch(DbUpdateConcurrencyException)
                {
                    return PartialView("Error");
                }
            }
            else
            {
                return PartialView("Error");
            }
            return View("Characters");
        }

        public async Task<IActionResult> Characters()
        {
            List<Results> input = await GetJsonObject();
            List<Models.Results> result = new List<Models.Results>();
            Models.Results r = new Models.Results();
            foreach(var re in input)
            {
                result.AddRange(new List<Results> { re });
            }
            return View(result);
        }

        public async Task<IActionResult> DetailedCharacter(Models.Results results)
        {
            List<Results> input = await GetJsonObject();
            List<Models.Results> result = new List<Models.Results>();
            Models.Results output = new Models.Results();

            output = input.First(s => s.Id == results.Id);

            foreach (var epi in output.Episode)
            {
                var subs = epi.Substring(40);
                if (results.Episode is null)
                {
                    results.Episode = new List<string>() { subs };
                }
                else { results.Episode.Add(subs); }
            }
            results.Name = output.Name; 
            results.Status = output.Status;
            results.Species = output.Species;
            results.Type = output.Type;
            results.Gender = output.Gender;
            results.Origin = output.Origin;
            results.Location = output.Location;
            results.Image = output.Image;
            results.Url = output.Url;
            results.Created = output.Created;
            results.Episode = new List<string>() { string.Join(",", results.Episode.ToArray()) };
            return View(results);
        }

        public ActionResult GetIconStatus(string model)
        {
            if (model == "Alive")
            {
                return View(new HtmlString("<i class=fa-solid fa-skull-crossbones></i>"));
                //return new ContentResult
                //{
                //    Content = " < FontAwesomeIcon icon ={ Heartbeat} />",
                //    ContentType = "text/html"
                //};
            }
            else if(model == "Dead")
            {
                return new ContentResult
                {
                    Content = "<i class=fa-solid fa-skull-crossbones></i>",
                    ContentType = "text/html"
                };
            }
            else
            {
                return new ContentResult
                {
                    Content = " < FontAwesomeIcon icon ={ Question} />",
                    ContentType = "text/html"
                };
            }
        }

        public async Task<IActionResult> CharactersUsingPlanet(string planet)
        {
            List<Results> input = await GetJsonObject();
            List<Models.Results>? chars = new List<Models.Results>();
            chars = input.Where(r => r.Origin.Name.ToLower().Contains(planet.ToLower())).ToList();
            return View("Characters",chars);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult UpdateDB(Results chars, Origin org, Location loc, Episode epi)
        {
            //if(chars.Results.Status == "Alive")
            //{
            //    this.Context.CharacterTable.Add(chars);
            //    this.Context.Origin.Add(org);
            //    this.Context.Location.Add(loc);
            //    this.Context.Episode.Add(epi);
            //}
            //this.Context.SaveChanges();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }  

        public PartialViewResult CreatePartial()
        {
            return PartialView("CreateForm");
        }
        public async Task<List<Results>> GetJsonObject()
        {
            Results input = new Results();
            List<Results> output = new List<Results>();
            var services = new ServiceCollection();
            services.AddHttpClient();
            var serviceProvider = services.BuildServiceProvider();
            var client = serviceProvider.GetService<HttpClient>();
            var response = await client.GetFromJsonAsync<Characters>("https://rickandmortyapi.com/api/character/");

            //using (var httpClient = new Http)
            //{
            //    using (var response = httpClient.Get("https://rickandmortyapi.com/api/character/"))
            //    {
            //        string apiResponse = response.ToString();
            //        input = JsonConvert.DeserializeObject<Characters>(apiResponse);
            //    }
            //}
            output.AddRange(response.Results);
            return output;
        }
    }
}