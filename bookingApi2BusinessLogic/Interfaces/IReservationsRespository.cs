using System.Collections.Generic;
using System.Threading.Tasks;
using bookingApi1DataAccess.Interfaces;
using bookingApi1DataAccess.Models;
using bookingApi2BusinessLogic.Dto;
using Microsoft.EntityFrameworkCore;
namespace bookingApi2BusinessLogic.Interfaces
{
    public interface IReservationsRespository:IGenericRepository<Reservations>
    {
        //fonctions pour gestioner les reservation, le detaille c'est dasn l'implementation
        public Task<bool> CreateReservation(Reservations obj);
        public Task<bool> ValidateDatesReservation(ReservationDto dto,int maxdays);
        public Task<Reservations>  CreateEntity(ReservationDto dto, bool flagUpdate);
        public Task<bool> UpdateReservation(Reservations objNew);
        public Task<bool> DeleteReservation(Reservations objDelete);
        public Task<IEnumerable<Reservations>> GetReservationsByClient(string client);
    }
}