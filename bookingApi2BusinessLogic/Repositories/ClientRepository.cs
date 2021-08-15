using bookingApi1DataAccess.Models;
using bookingApi1DataAccess;
using bookingApi2BusinessLogic.Interfaces;
using bookingApi1DataAccess.Classes;
using System.Threading.Tasks;
using bookingApi2BusinessLogic.Dto;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace bookingApi2BusinessLogic.Repositories
{
    /*
    Cette class utilice le abstract class GenericRepository avec l'entity Clients
    et implements les fonctions de l'interface IClientsRepository pour l'utilisation de
    le dependency injection.
    */
    public class ClientRepository : GenericRepository<Clients>, IClientRepository
    {
        //Constructor pour l'access au context
        public ClientRepository(BApiContext context) : base(context)
        {
        }
        //implementation des fonctions de l'interface
        public async Task<bool> Login(ClientDto dto)
        {
            bool result = false;
            //chercher l'utilisateur d'accord a l'user name et le password qui est crypte.
            var getLogin = await _context.Clients
                         .Where(c => c.userName == dto.userName && c.password == dto.password)
                         .AsNoTracking()
                         .ToListAsync();
            //si l'information existe donc changer le resultat a vrai
            if (getLogin.Any())
            {
                result = true;
            }
            return result;
        }

        //enregistrer un nouveau client, implementation de l'interface
        public async Task<bool> CreateUser(ClientDto dto)
        { 
            var result=false;
            //obtenir l'objet Client pour l'enregistrer
            var entity=GetEntity(dto);
            //ajouter l'entity
            await _context.Clients.AddAsync(entity);
            //enregistrer les changements
            var insert=await _context.SaveChangesAsync();
            //valider l'enregistrement
            if(insert>0)
                result=true;
            return result;
        }
        //transformet le DTO a une objet Client
        public Clients GetEntity(ClientDto dto)
        {
            Clients result = new Clients(dto);
            return result;
        }
    }
}