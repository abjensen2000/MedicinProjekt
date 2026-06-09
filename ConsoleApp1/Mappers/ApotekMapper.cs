using DTO;
using Models;

namespace Mappers
{
    public static class ApotekMapper
    {
        internal static ApotekDTO? Map(Apotek? apotek)
        {
            if (apotek == null)
            {
                return null;
            }
            string apotekNavn = apotek.Navn;

            var dto = new ApotekDTO(apotekNavn);
            dto.Id = apotek.Id;
            return dto;
        }

        internal static Apotek? Map(ApotekDTO? apotekDTO)
        {
            if (apotekDTO == null)
            {
                return null;
            }
            string apotekNavn = apotekDTO.Navn;

            var apotek = new Apotek(apotekNavn);
            apotek.Id = apotekDTO.Id;
            return apotek;
        }

        internal static List<ApotekDTO> Map(List<Apotek> apotekList)
        { 
            List<ApotekDTO> newList = new();
            foreach (Apotek apotek in apotekList)
            {
                var dto = Map(apotek);
                if (dto != null)
                {
                    newList.Add(dto);
                }
            }
            return newList;
        }
    }
}
