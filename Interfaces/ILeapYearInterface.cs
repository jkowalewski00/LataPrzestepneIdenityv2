using LataPrzestepneIdenity.Models;

namespace LataPrzestepneIdenity.Interfaces
{
    public interface ILeapYearInterface
    {
        public void addRecord(LeapYears leapYears);

        public IQueryable<LeapYears> getYears();

        public LeapYears checkOwner(int id);

        public void deleteRecord(LeapYears leapYears);
  
    }
}
