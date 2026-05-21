using DataAccess;
using DTO;
using Mappers;

namespace BusinessLayer
{
    public class ApotekService
    {
        private UnitOfWork _unitOfWork;

        public ApotekService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ApotekDTO CreateApotek(string navn)
        {
            ApotekDTO apotekDTO = new ApotekDTO(navn);
            return _unitOfWork.Apoteker.OpretApotek(apotekDTO);
        }

        public ApotekDTO? GetApotekDTO(int id)
        {
            return _unitOfWork.Apoteker.GetApotekById(id);
        }

        public List<ApotekDTO> GetAllApoteker()
        {
            return _unitOfWork.Apoteker.GetApoteker();
        }
    }
}
