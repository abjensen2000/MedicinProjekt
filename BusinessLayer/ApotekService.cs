using DataAccess;
using DTO;
using Mappers;

namespace BusinessLayer
{
    public class ApotekService
    {
        private MedicinContext _context;

        public ApotekService(MedicinContext context)
        {
            _context = context;
        }
        public ApotekDTO CreateApotek(string navn)
        {
            ApotekDTO apotekDTO = new ApotekDTO(navn);
            _context.Apoteker.Add(ApotekMapper.Map(apotekDTO));
            _context.SaveChanges();
            return apotekDTO;
        }

        public ApotekDTO? GetApotekDTO(int id)
        {
            return ApotekMapper.Map(_context.Apoteker.Find(id));
        }

        public List<ApotekDTO> GetAllApoteker()
        {
            return ApotekMapper.Map(_context.Apoteker.ToList());
        }
    }
}
