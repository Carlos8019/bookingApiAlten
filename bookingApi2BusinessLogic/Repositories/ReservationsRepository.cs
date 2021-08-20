
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
    Cette class permet gestioner les reservations 
    */
    public class ReservationsRepository : GenericRepository<Reservations>, IReservationsRespository
    {
        //gestioner les dates
        private readonly IManageDates _dates;
        //gestioner le loggin
        private readonly ILogger _logger;
        private readonly IRoomsRepository _rooms;
        //pour la gestion du calendrier 
        private ICalendarAvailabilityRespository _calendar;
        //returner le code de la fonction du validation
        public int codeValidation { set; get; }
        //Constructor pour le dependency injection
        public ReservationsRepository(BApiContext context, IRoomsRepository rooms
        , ILoggerFactory loggerFactory, ICalendarAvailabilityRespository calendar, IManageDates dates) : base(context)
        {
            this._rooms = rooms;
            this._dates = dates;
            this._calendar = calendar;
            this._logger = loggerFactory.CreateLogger("ReservationsRepository");

        }
        //Enregistrer une reservation
        public async Task<bool> CreateReservation(Reservations obj)
        {
            bool result = false;
            //gestioner les changements dans plusieurs tableaux avec transactions
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("Transaction cree correctement");
                await _context.Reservations.AddAsync(obj);
                var insert = await _context.SaveChangesAsync();
                //desactiver les dates 
                var updateCalendar = await _calendar.UpdateAvailability(obj, 0);
                //valider l'enregistrement
                if (insert > 0 && updateCalendar)
                {
                    result = true;
                    await transaction.CommitAsync();
                    _logger.LogInformation("Commit de creation de reservation");
                }
                else
                {
                    await transaction.RollbackAsync();
                    _logger.LogWarning("Rollback de creation de reservation insert: " + insert + " update: " + updateCalendar);
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error de creation de reservation:" + ex.Message);
                result = false;
            }
            return result;
        }
        //valider que les intervalles sont correctes , soulement de maxdays jours , n c'est un parametre en appsetting.json
        //et soulement maxdaysAdvance jours apres
        public async Task<bool> ValidateDatesReservation(ReservationDto dto, int maxdays, int maxdaysAdvance)
        {
            _logger.LogWarning("MaxDays Parameter: " + maxdays);
            //extraire les dates pour valider la disponibilité
            //Le format de date yyyymmdd permets validar facilement la disponibilite car on peux l'utiliser comme un int
            int.TryParse(dto.startDate, out int startDate);
            int.TryParse(dto.endDate, out int endDate);
            //valider que la réservation est faite moins de maxdaysAdvance jours à l'avance.
            var getEndDate = await _dates.GetMaxDate(maxdaysAdvance);
            if (endDate > getEndDate)
            {
                _logger.LogWarning("la reservation n'est pas moins de maxdaysAdvance jours à l'avance. endDate > getEndDate" + endDate +" "+ getEndDate);
                codeValidation = -3;//0 lorsque la reservation n'est pas moins de maxdaysAdvance jours à l'avance
                return false;
            }

            //valider que l'intervalle n'est que de maxdays jours
            int validateMaxDays = endDate - startDate;
            _logger.LogWarning("endDate-startDate: " + validateMaxDays);
            if (validateMaxDays > maxdays)
            {
                _logger.LogWarning("L'intervalle des jours est plus grande que " + maxdays);
                codeValidation = 0;//0 lorsque l'intervalle est plus grand
                return false;
            }

            _logger.LogWarning("L'intervalle des jours est correcte");
            //valider l'intervalle de dates avec les autres reservations a l'exception de la 
            //reservation actual s'ell est une modification
            //date initialle
            var validateStartDate = await _context.Reservations
                                  .Where(sd => startDate >= sd.startDate && startDate <= sd.endDate)
                                  .AsNoTracking()
                                  .ToListAsync();
            //si c'est une modification supprimer le reservation qui sera modifie
            if (!dto.idReservation.Equals("0"))
            {
                int.TryParse(dto.idReservation, out var reservation);
                validateStartDate = validateStartDate
                                .Where(r => r.idReservacion != reservation)
                                .ToList();

            }

            //si deja existe information ce n'est pas possible de creer la reservation
            if (validateStartDate.Any())
            {
                codeValidation = -1;//lorsque La date initialle fait partie d'autre reservation
                _logger.LogWarning("La date initialle fait partie d'autre reservation");
                return false;
            }


            //date de fin
            var validateEndDate = await _context.Reservations
                                  .Where(sd => endDate >= sd.startDate && endDate <= sd.endDate)
                                  .AsNoTracking()
                                  .ToListAsync();
            //si c'est une modification supprimer le reservation qui sera modifie
            if (!dto.idReservation.Equals("0"))
            {
                int.TryParse(dto.idReservation, out var reservation);
                validateEndDate = validateEndDate
                                .Where(r => r.idReservacion != reservation)
                                .ToList();

            }
            //si deja existe information ce n'est pas possible de creer la reservation
            if (validateEndDate.Any())
            {
                codeValidation = -2;//lorsque La date de fin fait partie d'autre reservation
                _logger.LogWarning("La date de fin fait partie d'autre reservation");
                return false;
            }


            return true;
        }
        //Cette fonctione permet retourner un objet pour enregistrer ou mis a jour (option)la reservation
        //flagUpdate=false -> enregistrer ne pas inclure l'index car ca n'existe pas
        //flagUpdate=true -> mis a jour , inclure l'index de la reservation
        public async Task<Reservations> CreateEntity(ReservationDto dto, bool flagUpdate)
        {
            //chercher le client pour prenom 
            var findClient = await _context.Clients
                         .Where(c => c.userName == dto.userName)
                         .AsNoTracking()
                         .ToListAsync();
            if (findClient.Any())
            {
                var findIdRoom = await _rooms.GetIdRoom(dto.room);
                //Valider si le room exist
                if (findIdRoom != 0)
                {
                    //creation de l'entity
                    int.TryParse(dto.startDate, out var startDate);
                    int.TryParse(dto.endDate, out var endDate);
                    Reservations obj = new Reservations();
                    //toujours obtenir le premiere id car il est identity
                    obj.ClientsidClient = findClient.First().idClient;
                    obj.idRoom = findIdRoom;
                    obj.startDate = startDate;
                    obj.endDate = endDate;
                    if (flagUpdate)
                    {
                        int.TryParse(dto.idReservation, out var idReservation);
                        obj.idReservacion = idReservation;
                    }
                    return obj;
                }
            }
            return null;
        }
        //Actualisation d'une reservation qui deja existe
        public async Task<bool> UpdateReservation(Reservations objNew)
        {
            //chercher la reservation pour id
            //Il va retourner 1 valeur car le index c'est identity
            var findReservation = await _context.Reservations
                           .Where(r => r.idReservacion == objNew.idReservacion)
                           .FirstAsync();
            var updateOldReservation = await _calendar.UpdateAvailability(findReservation, 1);
            if (updateOldReservation)
            {
                //actualisation des donnees avec transactions car il y a plusieurs des tableaux
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {

                    _logger.LogInformation("Transaction cree correctement");
                    //modifiers les donnees
                    findReservation.ClientsidClient = objNew.ClientsidClient;
                    findReservation.idRoom = objNew.idRoom;
                    findReservation.startDate = objNew.startDate;
                    findReservation.endDate = objNew.endDate;
                    //enregistrer
                    var result = await _context.SaveChangesAsync();
                    var update = await _calendar.UpdateAvailability(findReservation, 0);
                    //pour activer les dates précédentes

                    //valider modification
                    if (result > 0 && update)
                    {
                        await transaction.CommitAsync();
                        _logger.LogInformation("Commit bien fait update reservation");
                        return true;
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        _logger.LogWarning("Rollback de creation de reservation insert: " + result + " update: " + update);
                        return false;
                    }
                }
                catch (System.Exception)
                {
                    return false;
                }
            }
            return false;
        }
        //supprimer une reservation
        public async Task<bool> DeleteReservation(Reservations objDelete)
        {
            //chercher la reservation pour id
            //Il va retourner 1 valeur car le index c'est identity
            var findReservation = await _context.Reservations
                           .Where(d => d.idReservacion == objDelete.idReservacion)
                           .FirstAsync();
            //activer les dates qui seront supprimes
            var update = await _calendar.UpdateAvailability(findReservation, 1);
            if (update)
            {
                _context.Reservations.Remove(findReservation);
                var delete = await _context.SaveChangesAsync();
                if (delete > 0)
                {
                    _logger.LogWarning("Suppression correcte:");
                    return true;
                }
                _logger.LogWarning("Error de suppression de reservation:");
                return false;
            }
            _logger.LogWarning("Error de activation de reservation:");
            return false;
        }
        //liste des reservation du client
        public async Task<IEnumerable<Reservations>> GetReservationsByClient(string client)
        {
            var result = await _context.Reservations
                       .Include(cl => cl.Clients).Where(c => c.Clients.userName.Equals(client))
                       .AsNoTracking()
                       .ToListAsync();
            return result;
        }
    }
}