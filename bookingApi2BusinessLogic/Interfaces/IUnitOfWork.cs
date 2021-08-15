using System;
using bookingApi2BusinessLogic.Repositories;
using bookingApi1DataAccess;
namespace bookingApi2BusinessLogic.Interfaces
{
    /*
    Cette interface est utilice en tant que facade pattern afin de consolidation 
    d'autres interfaces et methodes le plus detaille.
    IDisposable permets gestioner le garbage collector
    */
    public interface IUnitOfWork:IDisposable
    {
        public IClientRepository Clients {get;}
        
    }
}