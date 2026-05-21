using DTO;
using Mappers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class OrdinationRepository
    {
        private readonly MedicinContext _context;

        public OrdinationRepository(MedicinContext context)
        {
            _context = context;
        }

        public List<OrdinationDTO> GetByReceptId(int receptId)
        {
            var recept = _context.Recepter.FirstOrDefault(r => r.Id == receptId && !r.Lukket);

            if (recept == null || recept.OrdinationerId == null || !recept.OrdinationerId.Any())
            {
                return new List<OrdinationDTO>();
            }

            var entities = _context.Ordinationer.Where(i => recept.OrdinationerId.Contains(i.Id)).ToList();
            return OrdinationMapper.Map(entities);
        }

        public void UdleverOrdination(int ordinationId)
        {
            var entity = _context.Ordinationer.FirstOrDefault(i => i.Id == ordinationId);
            if (entity != null)
            {
                if (entity.AntalUdleveringer - entity.AntalForetagneUdleveringer > 0)
                {
                    entity.AntalForetagneUdleveringer++;
                    _context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Ikke flere udleveringer på ordination");
                }
            }
        }

        public int OpretOrdination(OrdinationDTO ordinationDTO)
        {
            if (ordinationDTO != null)
            {
                var skalBrugesTilNytID = OrdinationMapper.Map(ordinationDTO);
                _context.Ordinationer.Add(skalBrugesTilNytID);
                _context.SaveChanges();
                return skalBrugesTilNytID.Id;
            }
            else
            {
                throw new ArgumentException("OrdinationDTO ikke givet");
            }

        }
    }
}