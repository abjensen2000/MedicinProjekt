using DTO;
using Models;
using System.Linq;

namespace Mappers
{
    public static class ReceptMapper
    {
        public static ReceptDTO? Map(Recept recept)
        {
            if (recept == null)
            {
                return null;
            }

            var dto = new ReceptDTO(recept.Ydernummer, recept.Cpr)
            {
                Id = recept.Id,
                Lukket = recept.Lukket
            };
            if (recept.OrdinationerId != null)
            {
                dto.OrdinationerId = recept.OrdinationerId.ToList();
            }
            return dto;
        }

        public static Recept? Map(ReceptDTO receptDTO)
        {
            if (receptDTO == null)
            {
                return null;
            }

            var recept = new Recept(receptDTO.Ydernummer, receptDTO.Cpr)
            {
                Id = receptDTO.Id,
                Lukket = receptDTO.Lukket
            };
            if (receptDTO.OrdinationerId != null)
            {
                recept.OrdinationerId = receptDTO.OrdinationerId.ToList();
            }
            return recept;
        }

        public static List<ReceptDTO> Map(List<Recept> list)
        {
            List<ReceptDTO> newList = new();
            foreach (var item in list)
            {
                var dto = Map(item);
                if (dto != null) newList.Add(dto);
            }
            return newList;
        }

        public static List<Recept> Map(List<ReceptDTO> list)
        {
            List<Recept> newList = new();
            foreach (var item in list)
            {
                var model = Map(item);
                if (model != null) newList.Add(model);
            }
            return newList;
        }
    }
}
