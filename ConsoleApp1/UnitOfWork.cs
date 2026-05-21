using DataAccess.Repositories;

namespace DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private readonly MedicinContext _context;
        private ApotekRepository _apotekRepository;
        private OrdinationRepository _ordinationRepository;
        private PatientRepository _patientRepository;
        private ReceptRepository _receptRepository;
        private LægehusRepository _lægehusRepository;
        private bool _disposed = false;

        public UnitOfWork(MedicinContext context)
        {
            _context = context;
            _apotekRepository = new ApotekRepository(_context);
            _ordinationRepository = new OrdinationRepository(_context);
            _patientRepository = new PatientRepository(_context);
            _receptRepository = new ReceptRepository(_context);
            _lægehusRepository = new LægehusRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public MedicinContext Context => _context;

        public ApotekRepository Apoteker { get => _apotekRepository;}
        public OrdinationRepository Ordinationer { get => _ordinationRepository; }
        public PatientRepository Patienter { get => _patientRepository; }
        public ReceptRepository Recepter { get => _receptRepository; }
        public LægehusRepository Lægehuse { get => _lægehusRepository; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}