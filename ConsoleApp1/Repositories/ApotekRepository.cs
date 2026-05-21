using DTO;
using Mappers;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ApotekRepository
    {
        private readonly MedicinContext _context;

        public ApotekRepository(MedicinContext context)
        {
            _context = context;
        }

        public ApotekDTO OpretApotek(ApotekDTO apotekDTO)
        {
            var entity = ApotekMapper.Map(apotekDTO);
            _context.Apoteker.Add(entity);
            _context.SaveChanges();
            return apotekDTO;
        }

        public ApotekDTO? GetApotekById(int id)
        {
            var entity = _context.Apoteker.Find(id);
            return entity != null ? ApotekMapper.Map(entity) : null;
        }

        public List<ApotekDTO> GetApoteker()
        {
            return ApotekMapper.Map(_context.Apoteker.ToList());
        }
    }
}