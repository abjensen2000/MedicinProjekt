using DataAccess;
using DTO;
using Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ReceptService
    {
        private UnitOfWork _unitOfWork;

        public ReceptService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ReceptDTO> GetRecepterByPatient(string cpr)
        {
            return _unitOfWork.Recepter.GetReceptByPatientCpr(cpr);
        }

        public ReceptDTO? GetReceptByOrdination(int ordinationId)
        {
            return _unitOfWork.Recepter.GetReceptByOrdinationId(ordinationId);
        }

        public void CheckOmReceptErTomt(int receptId)
        {
            _unitOfWork.Recepter.CheckOmReceptErTomtOgLuk(receptId);
        }

        public void AddOrdination(int receptId, int ordinationId)
        {
            _unitOfWork.Recepter.AddOrdination(receptId, ordinationId);
        }

        public void OpretRecept(ReceptDTO receptDTO)
        {
            _unitOfWork.Recepter.OpretRecept(receptDTO);
        }
    }
}
