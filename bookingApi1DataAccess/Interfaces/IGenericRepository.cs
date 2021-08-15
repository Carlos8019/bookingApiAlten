using System.Threading.Tasks;
using System.Collections.Generic;
namespace bookingApi1DataAccess.Interfaces
{
    /*
    cette interface gestione le dependecy injection pour les fonctions d'insertion, de selection
    et d'actualitation d'information avec la base de données.
    */

    public interface IGenericRepository<T> where T:class
    {
        //obtenir l'information pour ID
        public Task<T> Get(int id);
        //obtenir toute l'information d'une entity
        public Task<IEnumerable<T>> GetAll();

        //enregistrement 
        public Task Add(T entity);

        //effacer l'information d'une table
        public void Delete(T entity);

        //mettre à jour une table
        public void Update(T entity);
        //enregistrer les changements
        public Task<int> save();
    }
}