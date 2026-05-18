using BusinessLayer;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinController : ControllerBase
    {
        private ApotekService _apotekService;
        private ReceptService _receptService;
        private PatientService _patientService;
        private OrdinationService _ordinationService;

        public MedicinController(ApotekService apotekService, ReceptService receptService, PatientService patientService, OrdinationService ordinationService)
        {
            _apotekService = apotekService;
            _receptService = receptService;
            _patientService = patientService;
            _ordinationService = ordinationService;
        }

        [HttpGet("apoteker")]
        public IEnumerable<ApotekDTO> GetApoteker()
        {
            return _apotekService.GetAllApoteker();
        }

        [HttpGet("patienter")]
        public IEnumerable<ApotekDTO> GetPatienter()
        {
            return _apotekService.GetAllApoteker();
        }

        [HttpGet("recepter/patient/{cprnummer}")]
        public IEnumerable<ReceptDTO> GetRecepterTilPatient(string cprnummer)
        {
            return _receptService.GetRecepterByPatient(cprnummer);
        }

        [HttpGet("ordinationer/recept/{receptId}")]
        public IEnumerable<OrdinationDTO> GetOrdinationerTilRecept(int receptId)
        {
            return _ordinationService.GetOrdinationDTOerTilRecept(receptId);
        }

        [HttpPost("ordinationer/{ordinationId}")]
        public void UdleverOrdination(int ordinationId)
        {
            var recept = _receptService.GetReceptByOrdination(ordinationId);
            _ordinationService.UdleverOrdination(ordinationId);
            _receptService.CheckOmReceptErTomt(recept.Id);
        }
    }
}
