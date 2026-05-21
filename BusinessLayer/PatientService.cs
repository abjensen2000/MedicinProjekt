using DataAccess;
using DTO;
using Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class PatientService
    {
        private UnitOfWork _unitOfWork;

        public PatientService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public PatientDTO CreatePatient(string cpr, string navn)
        {
            PatientDTO patientDTO = new PatientDTO(cpr, navn);
            return _unitOfWork.Patienter.CreatePatient(patientDTO);
        }

        public PatientDTO? GetApotek(int id)
        {
            return _unitOfWork.Patienter.GetPatientById(id);
        }

        public List<PatientDTO> GetAllPatienter()
        {
            return _unitOfWork.Patienter.GetPatienter();
        }
    }
}
