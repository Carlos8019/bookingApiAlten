
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
namespace bookingApi2BusinessLogic.Repositories
{
    public class ReservationsRepository : GenericRepository<Reservations>, IReservationsRespository
    {
        private readonly IRoomsRepository _rooms;
        
        public ReservationsRepository(BApiContext context, IRoomsRepository rooms) : base(context)
        {
            this._rooms = rooms;
        }
        //Enregistrer une reservation
        public async Task<bool> CreateReservation(Reservations obj)
        {
            bool result = false;
            await _context.Reservations.AddAsync(obj);
            var insert = await _context.SaveChangesAsync();
            //valider l'enregistrement
            if (insert > 0)
                result = true;
            return result;
        }
        public async Task<bool> ValidateDatesReservation(ReservationDto dto)
        {
            //extraire les dates pour valider la disponibilité
            //Le format de date yyyymmdd permets validar facilement la disponibilite car on peux l'utiliser comme un int
            int.TryParse(dto.startDate, out int startDate);
            int.TryParse(dto.endDate, out int endDate);
            //valider l'intervalle de dates avec les autres reservations
            //date initialle
            var validateStartDate = await _context.Reservations
                                  .Where(sd => startDate >= sd.startDate && startDate <= sd.endDate)
                                  .AsNoTracking()
                                  .ToListAsync();
            //si deja existe information ce n'est pas possible de creer la reservation
            if (validateStartDate.Any())
                return false;
            
            //date de fin
            var validateEndDate = await _context.Reservations
                                  .Where(sd => endDate >= sd.startDate && endDate <= sd.endDate)
                                  .AsNoTracking()
                                  .ToListAsync();

            //si deja existe information ce n'est pas possible de creer la reservation
            if (validateEndDate.Any())
                return false;

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

        public async Task<bool> UpdateReservation(Reservations objNew)
        {
            //chercher la reservation pour id
            //Il va retourner 1 valeur car le index c'est identity
            var findReservation = await _context.Reservations
                           .Where(r => r.idReservacion == objNew.idReservacion)
                           .FirstAsync();
            
            //actualisation des donnees
            findReservation.ClientsidClient=objNew.ClientsidClient;
            findReservation.idRoom=objNew.idRoom;
            findReservation.startDate=objNew.startDate;
            findReservation.endDate=objNew.endDate;               
            var result=await _context.SaveChangesAsync();
            if(result>0)
                return true;
            return false;

        }
        public async Task<bool> DeleteReservation(Reservations objDelete)
        {
            //chercher la reservation pour id
            //Il va retourner 1 valeur car le index c'est identity
            var findReservation=await _context.Reservations
                           .Where(d=>d.idReservacion==objDelete.idReservacion)
                           .FirstAsync();
            _context.Reservations.Remove(findReservation);
            var delete=await _context.SaveChangesAsync();
            if(delete>0)
                return true;
            return false;
            
        }
        //liste des reservation du client
        public async Task<IEnumerable<Reservations>> GetReservationsByClient(string client)
        {
            var result=await _context.Reservations
                       .Include(cl=>cl.Clients).Where(c=>c.Clients.userName.Equals(client))
                       .AsNoTracking()
                       .ToListAsync();
            return result;
        }
        public Task<IEnumerable<Reservations>> GetAvailability()
        {
            throw new NotImplementedException();   
        }

    }
}