using System.Threading.Tasks;
using bookingApi1DataAccess.Interfaces;
using bookingApi1DataAccess.Models;
using bookingApi2BusinessLogic.Dto;
namespace bookingApi2BusinessLogic.Interfaces
{
    public interface IRoomsRepository:IGenericRepository<Rooms>
    {
        public Task<int> GetIdRoom(string roomCode);
    }
}