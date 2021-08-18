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
    public class RoomsRepository:GenericRepository<Rooms>,IRoomsRepository
    {
        public RoomsRepository(BApiContext context) : base(context)
        {
            
        }

        public async Task<int> GetIdRoom(string roomCode)
        {
            //chercher le id du room pour l'enregistrer en tant que foreing key
            var findRoom=await _context.Rooms
                   .Where(ro=>ro.codeRoom.Equals(roomCode))
                   .ToListAsync();
            if(findRoom.Any())
                return findRoom.First().idRoom;
            else
                return 0;
        }
    }
}