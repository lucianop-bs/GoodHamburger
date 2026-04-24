using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GoodHamburgerContext _context;

        public UnitOfWork(GoodHamburgerContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}