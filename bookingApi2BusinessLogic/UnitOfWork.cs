using bookingApi2BusinessLogic.Interfaces;
using bookingApi1DataAccess;
using System;
namespace bookingApi2BusinessLogic
{
    public class UnitOfWork : IUnitOfWork
    {
        //_context permets gestioner l'access a la base de donees 
        private readonly BApiContext _context;
        public IClientRepository Clients {get;}
        //Dependency injection allocation
        public UnitOfWork(BApiContext context, IClientRepository clients)
        {
            this.Clients=clients;
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