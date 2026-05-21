using DTO;
using Mappers;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class LægehusRepository
    {
        private readonly MedicinContext _context;

        public LægehusRepository(MedicinContext context)
        {
            _context = context;
        }

        public LægehusDTO OpretLægehus(LægehusDTO lægehusDTO)
        {
            var entity = LægehusMapper.Map(lægehusDTO);
            _context.Lægehuse.Add(entity);
            _context.SaveChanges();
            return lægehusDTO;
        }

        public LægehusDTO? GetLægehusByYdernummer(string ydernummer)
        {
            var entity = _context.Lægehuse.FirstOrDefault(l => l.Ydernummer == ydernummer);
            if (entity != null)
            {
                return LægehusMapper.Map(entity);
            }
            else {
                return null;
            }
        }

        public LægehusDTO? GetLægehusById(int id)
        {
            var entity = _context.Lægehuse.Find(id);
            if (entity != null)
            {
                return LægehusMapper.Map(entity);
            }
            else {
                return null;
            }
        }

        public List<LægehusDTO> GetLægehuse()
        {
            return _context.Lægehuse.ToList().Select(LægehusMapper.Map).ToList();
        }
    }
}