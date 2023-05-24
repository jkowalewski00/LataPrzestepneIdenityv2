using LataPrzestepneIdenity.Data;
using LataPrzestepneIdenity.Interfaces;
using LataPrzestepneIdenity.Models;

namespace LataPrzestepneIdenity.Services
{
    public class LeapYearService : ILeapYearInterface
    {
        private readonly ApplicationDbContext _context;

        public LeapYearService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public void addRecord(LeapYears leapYears)
        {
            _context.LeapYears.Add(leapYears);
            _context.SaveChanges();
        }

        public IQueryable<LeapYears> getYears()
        {
             return _context.LeapYears.AsQueryable();
        }

        public LeapYears checkOwner(int  ownerId)
        {
            return _context.LeapYears.SingleOrDefault(d => d.Id == ownerId);
        }

        public void deleteRecord(LeapYears leapYears)
        {
            _context.LeapYears.Remove(leapYears);
            _context.SaveChanges();
        }
    }
}
