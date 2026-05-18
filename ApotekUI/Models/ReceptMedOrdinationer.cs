using DTO;

namespace ApotekUI.Models
{
    public class ReceptMedOrdinationer
    {
        private ReceptDTO _recept;
        private List<OrdinationDTO> _ordinationer;

        public ReceptMedOrdinationer(ReceptDTO recept)
        {
            _recept = recept;
            _ordinationer = new List<OrdinationDTO>();
        }

        public ReceptDTO Recept { get => _recept; set => _recept = value; }
        public List<OrdinationDTO> Ordinationer { get => _ordinationer; set => _ordinationer = value; }
    }
}
