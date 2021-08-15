using Microsoft.Extensions.DependencyInjection;
using bookingApi2BusinessLogic.Interfaces;
using bookingApi2BusinessLogic.Repositories;
using bookingApi1DataAccess;
namespace bookingApi2BusinessLogic
{
    /*
    Cette class permet gestioner l'instances d'object avec le startup du projet de services
    et utilicer l'interfaces comme dependency injection.
    Elle sera invoque dans le startup class.
    */
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            //AddTrasient permet la creation d'objets pour toute la petition
            services.AddTransient<IClientRepository,ClientRepository>();
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            return services;
        }
    }
}