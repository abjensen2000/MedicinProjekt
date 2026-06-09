using DTO;
using Models;

namespace Mappers
{
    public static class PatientMapper
    {
        internal static PatientDTO? Map(Patient patient)
        {
            if (patient == null)
            {
                return null;
            }

            return new PatientDTO(patient.Cpr, patient.Navn) { Id = patient.Id };
        }

        internal static Patient? Map(PatientDTO patientDTO)
        {
            if (patientDTO == null)
            {
                return null;
            }

            return new Patient(patientDTO.Cpr, patientDTO.Navn) { Id = patientDTO.Id };
        }

        internal static List<PatientDTO> Map(List<Patient> list)
        {
            List<PatientDTO> newList = new();
            foreach (var item in list)
            {
                var dto = Map(item);
                if (dto != null) newList.Add(dto);
            }
            return newList;
        }

        internal static List<Patient> Map(List<PatientDTO> list)
        {
            List<Patient> newList = new();
            foreach (var item in list)
            {
                var model = Map(item);
                if (model != null) newList.Add(model);
            }
            return newList;
        }
    }
}
