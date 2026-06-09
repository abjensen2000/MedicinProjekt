using DTO;
using Models;
using System.Linq;

namespace Mappers
{
    public static class LægehusMapper
    {
        internal static LægehusDTO? Map(Lægehus lægehus)
        {
            if (lægehus == null)
            {
                return null;
            }

            var dto = new LægehusDTO(lægehus.Ydernummer, lægehus.Navn);
            dto.Id = lægehus.Id;

            return dto;
        }

        internal static Lægehus? Map(LægehusDTO lægehusDTO)
        {
            if (lægehusDTO == null)
            {
                return null;
            }

            var lægehus = new Lægehus(lægehusDTO.Ydernummer, lægehusDTO.Navn);
            lægehus.Id = lægehusDTO.Id;

            return lægehus;
        }

        internal static List<LægehusDTO> Map(List<Lægehus> list)
        {
            List<LægehusDTO> newList = new();
            foreach (var item in list)
            {
                var dto = Map(item);
                if (dto != null) newList.Add(dto);
            }
            return newList;
        }

        internal static List<Lægehus> Map(List<LægehusDTO> list)
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
