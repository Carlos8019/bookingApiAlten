using System.Threading.Tasks;
namespace bookingApi2BusinessLogic.Interfaces
{
    /*Cette interface gestione les dates pour DI */
    public interface IManageDates
    {
        public int startDate {get;set;}
        public Task<int> GetMaxDate(int days);
    }
}