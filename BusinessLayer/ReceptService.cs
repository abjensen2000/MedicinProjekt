using DataAccess;
using DTO;
using Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ReceptService
    {
        private MedicinContext _context;

        public ReceptService(MedicinContext context)
        {
            _context = context;
        }

        public List<ReceptDTO> GetRecepterByPatient(string cpr)
        {
            return ReceptMapper.Map(_context.Recepter.Where(i => i.Cpr == cpr && !i.Lukket).ToList());
        }

        public ReceptDTO? GetReceptByOrdination(int ordinationId)
        {
            return ReceptMapper.Map(_context.Recepter.FirstOrDefault(i => i.OrdinationerId.Contains(ordinationId) && !i.Lukket));
        }

        public void CheckOmReceptErTomt(int receptId)
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
    }
}
