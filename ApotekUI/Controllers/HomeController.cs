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
            List<ApotekDTO>? apotekDTOer = await _client.GetFromJsonAsync<List<ApotekDTO>>("http://localhost:5062/api/medicin/apoteker");
            foreach (ApotekDTO apotekDTO in apotekDTOer)
            {
                if (apotekDTO.Id == apotekID)
                {
                    return View("Forside", apotekDTO);
                }
            }
            return View("Index");
        }
        [HttpGet]
        public IActionResult Forside()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Forside(string cprnummer, int apotekID)
        {

            List<ReceptDTO>? patientsRecepter = await _client.GetFromJsonAsync<List<ReceptDTO>>($"http://localhost:5062/api/medicin/recepter/patient/{cprnummer}");
            List<ReceptMedOrdinationer> recepterMedOrdinationer = new List<ReceptMedOrdinationer>();

            if (patientsRecepter != null)
            {
                foreach (ReceptDTO recept in patientsRecepter)
                {
                    var ordinationer = await _client.GetFromJsonAsync<List<OrdinationDTO>>($"http://localhost:5062/api/medicin/ordinationer/recept/{recept.Id}");
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
            var response = await _client.PostAsync($"http://localhost:5062/api/medicin/ordinationer/{ordinationId}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GenindlæsReceptListe", new { cprnummer = cpr });
            }

            return Content($"Der opstod en fejl ved udlevering af ordinationen.");
        }
    }
}
