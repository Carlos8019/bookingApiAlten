using System.Threading.Tasks;
using bookingApi1DataAccess.Interfaces;
using bookingApi1DataAccess.Models;
using bookingApi2BusinessLogic.Dto;
using System.Collections.Generic;
namespace bookingApi2BusinessLogic.Interfaces
{
    /*
    Cette interface gestione les fonctions des availability qui seront utilices 
    pour l'unit of work dans le web api.
    IGenericRepository permets reutilicer les fonctions de CRUD avec la base de donees
    */
    public interface ICalendarAvailabilityRespository:IGenericRepository<CalendarAvailability>
    {
        public Task<bool> UpdateAvailability(Reservations entity,int option);
        public Task<IEnumerable<CalendarAvailability>> GetAvailability(int days);
    }
}