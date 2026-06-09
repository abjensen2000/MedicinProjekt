using ApotekUI.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace ApotekUI.Controllers
{
    public class HomeController : Controller
    {
        private HttpClient _client = new HttpClient();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int apotekID)
        {
            try
            {
                List<ApotekDTO>? apotekDTOer = await _client.GetFromJsonAsync<List<ApotekDTO>>("http://localhost:5062/api/apotek/apoteker");
                if (apotekDTOer != null)
                {
                    var valgtApotek = apotekDTOer.FirstOrDefault(a => a.Id == apotekID);
                    if (valgtApotek != null)
                    {
                        return View("Forside", valgtApotek);
                    }
                }

                ViewBag.Error = "Forkert ID indtastet eller apotek blev ikke fundet.";
            }
            catch (HttpRequestException)
            {
                ViewBag.Error = "Kunne ikke oprette forbindelse til serveren. Tjek om API'et kører.";
            }
            catch (Exception)
            {
                ViewBag.Error = "Der opstod en uventet fejl.";
            }

            return View("Index");
        }
        [HttpGet]
        public IActionResult Forside()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Forside(string cprnummer, int apotekID) //Bruger ikke apotekID
        {
            List<ReceptMedOrdinationer> recepterMedOrdinationer = new List<ReceptMedOrdinationer>();

            if (string.IsNullOrWhiteSpace(cprnummer) || cprnummer.Length != 10 || !cprnummer.All(char.IsDigit))
            {
                ViewBag.Error = "Du skal indtaste et CPR-nummer.";
                return View("Forside");
            }

            try
            {
                List<ReceptDTO>? patientsRecepter = await _client.GetFromJsonAsync<List<ReceptDTO>>($"http://localhost:5062/api/apotek/recepter/patient/{cprnummer}");

                if (patientsRecepter != null && patientsRecepter.Any())
                {
                    foreach (ReceptDTO recept in patientsRecepter)
                    {
                        var ordinationer = await _client.GetFromJsonAsync<List<OrdinationDTO>>($"http://localhost:5062/api/apotek/ordinationer/recept/{recept.Id}");
                        ReceptMedOrdinationer receptMedOrdinationer = new ReceptMedOrdinationer(recept);
                        if (ordinationer != null)
                        {
                            foreach (OrdinationDTO ordination in ordinationer)
                            {
                                receptMedOrdinationer.Ordinationer.Add(ordination);
                            }
                        }
                        recepterMedOrdinationer.Add(receptMedOrdinationer);
                    }
                }
                else
                {
                    ViewBag.Message = "Ingen recepter fundet på dette CPR-nummer.";
                }
            }
            catch (HttpRequestException)
            {
                ViewBag.Error = "Kunne ikke hente data fra serveren. Tjek om API'et kører.";
                return View("Forside");
            }
            catch (Exception)
            {
                ViewBag.Error = "Der opstod en uventet fejl under fremsøgningen.";
                return View("Forside");
            }

            return View("ReceptListe", recepterMedOrdinationer);
        }



        [HttpGet]
        public IActionResult ReceptListe()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GenindlæsReceptListe(string cprnummer)
        {
            return await Forside(cprnummer, 0);
        }

        [HttpPost]
        public async Task<IActionResult> UdleverOrdination(int ordinationId, string cpr)
        {
            try
            {
                var response = await _client.PostAsync($"http://localhost:5062/api/apotek/ordinationer/{ordinationId}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GenindlæsReceptListe", new { cprnummer = cpr });
                }
            }
            catch (Exception)
            {

            }

            return Content($"Der opstod en fejl ved udlevering af ordinationen.");
        }
    }
}
