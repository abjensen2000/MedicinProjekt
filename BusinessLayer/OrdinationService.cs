using DataAccess;
using DTO;
using Mappers;
using Models;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace BusinessLayer
{
    public class OrdinationService
    {
        private MedicinContext _context;

        public OrdinationService(MedicinContext context)
        {
            _context = context;
        }
        public List<OrdinationDTO> GetOrdinationDTOerTilRecept(int receptId)
        {
            var recept = _context.Recepter.FirstOrDefault(r => r.Id == receptId && !r.Lukket);

            if (recept == null || recept.OrdinationerId == null || !recept.OrdinationerId.Any())
            {
                return new List<OrdinationDTO>();
            }
            var ordinationer = _context.Ordinationer.Where(o => recept.OrdinationerId.Contains(o.Id)).ToList();
            return OrdinationMapper.Map(ordinationer);
        }

        public void UdleverOrdination(int ordinationId)
        {
            Ordination? ordination = _context.Ordinationer.FirstOrDefault((i) => i.Id == ordinationId);
            if (ordination != null)
            {
                if (ordination.AntalUdleveringer - ordination.AntalForetagneUdleveringer > 0)
                {
                    ordination.AntalForetagneUdleveringer++;
                    _context.SaveChanges();
                }
                else {
                    throw new ArgumentException("Ikke flere udleveringer på ordination");
                }
            }
        }
    }
}
