using System.Threading.Tasks;
using bookingApi1DataAccess.Interfaces;
using bookingApi1DataAccess.Models;
using bookingApi2BusinessLogic.Dto;
namespace bookingApi2BusinessLogic.Interfaces
{
    /*
    Cette interface gestione les fonctions des clients qui seront utilices 
    por l'unit of work dans le web api.
    IGenericRepository permets reutilicer les fonctions de CRUD avec la base de donees
    */
    public interface IClientRepository:IGenericRepository<Clients>
    {
        //Valider l'access en utilisant le dto qui est reÇu du client React
        public Task<bool> Login(ClientDto dto);
        //enregistrer un nouveau client
        public Task<bool> CreateUser(ClientDto dto);
        //transformer le donnee Json a un object du type client afin d'utiliser Entity Framework
        public Clients GetEntity(ClientDto dto);
    }
}