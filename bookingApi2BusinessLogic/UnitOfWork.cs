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
        public ICalendarAvailabilityRespository Calendars {get;}
        //Dependency injection allocation
        public UnitOfWork(BApiContext context
        ,IClientRepository clients
        ,IReservationsRespository reservations
        ,IRoomsRepository rooms
        ,ICalendarAvailabilityRespository calendars)
        {
            this.Clients=clients;
            this.Reservations=reservations;
            this.Rooms=rooms;
            this.Calendars=calendars;
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