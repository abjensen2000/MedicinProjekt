using DTO;
using Mappers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ReceptRepository
    {
        private readonly MedicinContext _context;

        public ReceptRepository(MedicinContext context)
        {
            _context = context;
        }

        public List<ReceptDTO> GetReceptByPatientCpr(string cpr)
        {
            var entities = _context.Recepter.Where(i => i.Cpr == cpr && !i.Lukket).ToList();
            return ReceptMapper.Map(entities);
        }

        public ReceptDTO? GetReceptByOrdinationId(int ordinationId)
        {
            var entity = _context.Recepter.FirstOrDefault(i => i.OrdinationerId.Contains(ordinationId) && !i.Lukket);
            if (entity != null)
            {
                return ReceptMapper.Map(entity);
            }
            else
            {
                return null;
            }
        }

        public void CheckOmReceptErTomtOgLuk(int receptId)
        {
            var recept = _context.Recepter.FirstOrDefault(r => r.Id == receptId);
            if (recept != null)
            {
                var ordinationer = _context.Ordinationer.Where(o => recept.OrdinationerId.Contains(o.Id)).ToList();

                if (ordinationer.Any() && ordinationer.All(o => o.AntalUdleveringer == o.AntalForetagneUdleveringer))
                {
                    recept.Lukket = true;
                    _context.SaveChanges();
                }
            }
        }

        public void AddOrdination(int receptId, int ordinationId)
        {
            var recept = _context.Recepter.FirstOrDefault(r => r.Id == receptId);
            if (recept != null)
            {
                recept.OrdinationerId.Add(ordinationId);
                _context.SaveChanges();
            }
        }

        public void OpretRecept(ReceptDTO receptDTO)
        {
            if (receptDTO != null)
            {
                _context.Recepter.Add(ReceptMapper.Map(receptDTO));
                _context.SaveChanges();
            }
        }
    }
}