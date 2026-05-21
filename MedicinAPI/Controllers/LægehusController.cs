using BusinessLayer;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MedicinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LægehusController : ControllerBase
    {
        private ApotekService _apotekService;
        private ReceptService _receptService;
        private PatientService _patientService;
        private OrdinationService _ordinationService;
        private LægehusService _lægehusService;

        public LægehusController(ApotekService apotekService, ReceptService receptService, PatientService patientService, OrdinationService ordinationService, LægehusService lægehusService)
        {
            _apotekService = apotekService;
            _receptService = receptService;
            _patientService = patientService;
            _ordinationService = ordinationService;
            _lægehusService = lægehusService;
        }

        [HttpGet("lægehuse")]
        public IEnumerable<LægehusDTO> GetLægehuse()
        {
            return _lægehusService.GetAllLægehuse();
        }
        [HttpGet("recepter/patient/{cprnummer}")]
        public IEnumerable<ReceptDTO> GetRecepterTilPatient(string cprnummer)
        {
            return _receptService.GetRecepterByPatient(cprnummer);
        }

        [HttpPost("ordinationer/{receptId}")]
        public void OpretOrdination(int receptId, OrdinationDTO ordinationDTO)
        {
            int nytOrdinationId = _ordinationService.OpretOrdination(ordinationDTO); //MEGET interressant
            _receptService.AddOrdination(receptId, nytOrdinationId);
        }

        [HttpGet("ordinationer/recept/{receptId}")]
        public IEnumerable<OrdinationDTO> GetOrdinationerTilRecept(int receptId)
        {
            return _ordinationService.GetOrdinationerTilRecept(receptId);
        }

        [HttpPost("recepter")]
        public void OpretRecept(ReceptDTO receptDTO)
        {
            _receptService.OpretRecept(receptDTO);
        }
    }
}
