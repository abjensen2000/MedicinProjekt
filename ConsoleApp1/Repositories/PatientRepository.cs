using DTO;
using Mappers;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class PatientRepository
    {
        private readonly MedicinContext _context;

        public PatientRepository(MedicinContext context)
        {
            _context = context;
        }

        public PatientDTO CreatePatient(PatientDTO patientDTO)
        {
            var entity = PatientMapper.Map(patientDTO);
            _context.Patienter.Add(entity);
            _context.SaveChanges();
            return patientDTO;
        }

        public PatientDTO? GetPatientById(int id)
        {
            var entity = _context.Patienter.Find(id);
            if (entity != null)
            {
                return PatientMapper.Map(entity);
            }
            else {
                return null;
            }
        }

        public List<PatientDTO> GetPatienter()
        {
            return PatientMapper.Map(_context.Patienter.ToList());
        }
    }
}