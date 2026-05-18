using DTO;
using Models;
using System.Linq;

namespace Mappers
{
    public static class LægehusMapper
    {
        public static LægehusDTO? Map(Lægehus lægehus)
        {
            if (lægehus == null)
            {
                return null;
            }

            var dto = new LægehusDTO(lægehus.Ydernummer, lægehus.Navn);
            dto.Id = lægehus.Id;
            if (lægehus.PatientCPR != null)
            {
                dto.PatientCPR = lægehus.PatientCPR.ToList();
            }
            return dto;
        }

        public static Lægehus? Map(LægehusDTO lægehusDTO)
        {
            if (lægehusDTO == null)
            {
                return null;
            }

            var lægehus = new Lægehus(lægehusDTO.Ydernummer, lægehusDTO.Navn);
            lægehus.Id = lægehusDTO.Id;
            if (lægehusDTO.PatientCPR != null)
            {
                lægehus.PatientCPR = lægehusDTO.PatientCPR.ToList();
            }
            return lægehus;
        }

        public static List<LægehusDTO> Map(List<Lægehus> list)
        {
            List<LægehusDTO> newList = new();
            foreach (var item in list)
            {
                var dto = Map(item);
                if (dto != null) newList.Add(dto);
            }
            return newList;
        }

        public static List<Lægehus> Map(List<LægehusDTO> list)
        {
            List<Lægehus> newList = new();
            foreach (var item in list)
            {
                var model = Map(item);
                if (model != null) newList.Add(model);
            }
            return newList;
        }
    }
}
