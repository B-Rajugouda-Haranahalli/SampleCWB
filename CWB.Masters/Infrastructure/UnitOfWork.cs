using System;
using System.Threading.Tasks;

namespace CWB.Masters.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MastersDbContext _context;


        public UnitOfWork(MastersDbContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
