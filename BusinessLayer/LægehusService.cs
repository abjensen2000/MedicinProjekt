using DataAccess;
using DTO;

namespace BusinessLayer
{
    public class LægehusService
    {
        private UnitOfWork _unitOfWork;

        public LægehusService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public LægehusDTO CreateLægehus(string cvr, string navn)
        {
            LægehusDTO lægehusDTO = new LægehusDTO(cvr, navn);
            return _unitOfWork.Lægehuse.OpretLægehus(lægehusDTO);
        }

        public LægehusDTO? GetLægehusDTO(int id)
        {
            return _unitOfWork.Lægehuse.GetLægehusById(id);
        }

        public LægehusDTO? GetByYdernummer(string ydernummer)
        {
            return _unitOfWork.Lægehuse.GetLægehusByYdernummer(ydernummer);
        }

        public List<LægehusDTO> GetAllLægehuse()
        {
            return _unitOfWork.Lægehuse.GetLægehuse();
        }
    }
}