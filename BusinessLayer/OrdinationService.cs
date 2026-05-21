using DataAccess;
using DTO;
using Mappers;
using Models;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace BusinessLayer
{
    public class OrdinationService
    {
        private UnitOfWork _unitOfWork;

        public OrdinationService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<OrdinationDTO> GetOrdinationerTilRecept(int receptId)
        {
            return _unitOfWork.Ordinationer.GetByReceptId(receptId);
        }

        public void UdleverOrdination(int ordinationId)
        {
            _unitOfWork.Ordinationer.UdleverOrdination(ordinationId);
        }

        public int OpretOrdination(OrdinationDTO ordinationDTO)
        {
            return _unitOfWork.Ordinationer.OpretOrdination(ordinationDTO);
        }
    }
}
