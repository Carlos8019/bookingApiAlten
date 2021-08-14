using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using dataAccess.interfaces;
namespace bookingApi1DataAccess.Classes
{
    /*
    Cette Class met en oeuvre l'interface IGenericRepository afin d'être utilice 
    en tant qu'une abstract class pour reutilicer les fonctions du CRUD avec
    la base de données. Toutes les methods sont asynchrones.
    */
    public abstract class GenericRepository<T>:IGenericRepository<T> where T:class
    {
        //BApiContext est utilice pour l'Entity Framework pour se communiquer avec la base de données
        protected readonly BApiContext _context;
        //represente chaque table ou entity
        internal DbSet<T> dBSet;
        
        //le constructor gestione l'initialisation du context et le DBSET
        protected GenericRepository(EsDbContext context)
        {
            _context=context;
            this.dBSet=context.Set<T>();
        }
        //implementation du method Get pour ID
        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        //implementation du method d'obtenir tout
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        //implementation du method d'enregistrer
        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        //implementation du method d'effacer
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        //implemtation du method de mettre à jour
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        //implementation d'enregistrer
        public async Task<int> save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}