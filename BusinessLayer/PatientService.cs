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
        private MedicinContext? _context;

        public PatientService(MedicinContext context)
        {
            _context = context;
        }
        public PatientDTO CreatePatientDTO(string cpr, string navn)
        {
            PatientDTO patientDTO = new PatientDTO(cpr, navn);
            _context.Patienter.Add(PatientMapper.Map(patientDTO));
            _context.SaveChanges();
            return patientDTO;
        }

        public PatientDTO? GetApotekDTO(int id)
        {
            return PatientMapper.Map(_context.Patienter.Find(id));
        }

        public List<PatientDTO> GetAllPatientDTOer()
        {
            return PatientMapper.Map(_context.Patienter.ToList());
        }
    }
}
