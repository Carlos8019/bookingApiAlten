
using bookingApi1DataAccess.Models;
using bookingApi1DataAccess;
using bookingApi2BusinessLogic.Interfaces;
using bookingApi1DataAccess.Classes;
using System.Threading.Tasks;
using bookingApi2BusinessLogic.Dto;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace bookingApi2BusinessLogic.Repositories
{
    /*
    Cette fonction permet l'actualisation des dates de reservations dans chaque fois qui existe une modification des donness
    Elle utilise un field aved 1 ou 0 pour savoir que la date est deja reserve, se considere uniquement les 30 jours procahines
    d'accord au besoin du chanllenge, les nombre de jours sont un parameter dans le appsetting.json
    */
    public class CalendarAvailabilityRespository:GenericRepository<CalendarAvailability>,ICalendarAvailabilityRespository
    {
        //gestioner le loggin
         private readonly ILogger _logger;
        //gestioner les dates
        private readonly IManageDates _dates;
        public CalendarAvailabilityRespository(BApiContext context,ILoggerFactory loggerFactory,IManageDates dates) : base(context)
        {
            _logger=loggerFactory.CreateLogger("CalendarAvailability");
            _dates=dates;
        }

        //obtenir les dates avec la disponibilite des prochaines 30 jours
        //les jours sont obtenus du fichier appsetting.json
        public async Task<IEnumerable<CalendarAvailability>> GetAvailability(int days)
        {
            var endDate=await _dates.GetMaxDate(days);
            _logger.LogInformation("Start Date: "+_dates.startDate);
            _logger.LogInformation("End Date: "+endDate);
            var result=await _context.CalendarAvailabilities
                  .Where(d=>d.date>=_dates.startDate && d.date<=endDate)
                  .ToListAsync();

            return result;
        }
        //actualisation des dates avec une modification de une reservation
        //quand le parametre option=0 signifie desactiver le date
        //quand le parametre option=1 signifie activer le date
        public async Task<bool> UpdateAvailability(Reservations entity,int option)
        {
            bool result=false;
            //obtenir les dates d'intervalle pour faire l'update
            var dates=await _context.CalendarAvailabilities
                      .Where(d=>d.date>=entity.startDate && d.date<=entity.endDate) 
                      .ToListAsync();
            //modifier btcStatus pour la reservartion d'accord a l'option
            foreach (var item in dates)
            {
                item.status=(option==0)?0:1;
            }
            //faire l'actualisation
            _context.UpdateRange(dates);
            var update=await _context.SaveChangesAsync();
            if(update>0)
               result=true;
            
            return result;
        }
    }
}