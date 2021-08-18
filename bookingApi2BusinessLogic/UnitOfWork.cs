using bookingApi2BusinessLogic.Interfaces;
using bookingApi1DataAccess;
using System;
namespace bookingApi2BusinessLogic
{
    public class UnitOfWork : IUnitOfWork
    {
        //_context permets gestioner l'access a la base de donees 
        private readonly BApiContext _context;
        //repositories
        public IClientRepository Clients {get;}
        public IReservationsRespository Reservations {get;}
        public IRoomsRepository Rooms{get;}
        //Dependency injection allocation
        public UnitOfWork(BApiContext context
        ,IClientRepository clients
        ,IReservationsRespository reservations
        ,IRoomsRepository rooms)
        {
            this.Clients=clients;
            this.Reservations=reservations;
            this.Rooms=rooms;
            this._context=context;
        }
        //gestion du garbage collector
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //override le fonction Dispose pour le gestioner de facon personnalise
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
                _context.Dispose();
        }

    }
}