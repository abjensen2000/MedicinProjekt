using DTO;
using Models;

namespace Mappers
{
    public static class OrdinationMapper
    {
        internal static OrdinationDTO? Map(Ordination ordination)
        {
            if (ordination == null)
            {
                return null;
            }

            var dto = new OrdinationDTO(ordination.Lægemiddel, ordination.Dosis, ordination.AntalUdleveringer)
            {
                Id = ordination.Id,
                AntalForetagneUdleveringer = ordination.AntalForetagneUdleveringer
            };
            return dto;
        }

        internal static Ordination? Map(OrdinationDTO ordinationDTO)
        {
            if (ordinationDTO == null)
            {
                return null;
            }

            var ordination = new Ordination(ordinationDTO.Lægemiddel, ordinationDTO.Dosis, ordinationDTO.AntalUdleveringer)
            {
                Id = ordinationDTO.Id,
                AntalForetagneUdleveringer = ordinationDTO.AntalForetagneUdleveringer
            };
            return ordination;
        }

        internal static List<OrdinationDTO> Map(List<Ordination> list)
        {
            List<OrdinationDTO> newList = new();
            foreach (var item in list)
            {
                var dto = Map(item);
                if (dto != null) newList.Add(dto);
            }
            return newList;
        }

        internal static List<Ordination> Map(List<OrdinationDTO> list)
        {
            List<Ordination> newList = new();
            foreach (var item in list)
            {
                var model = Map(item);
                if (model != null) newList.Add(model);
            }
            return newList;
        }
    }
}
