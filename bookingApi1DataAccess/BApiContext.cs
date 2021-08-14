using Microsoft.EntityFrameworkCore;
namespace bookingApi1DataAccess
{
    public class BApiContext:DBContext
    {
        public BApiContext(DbContextOptions<BApiContext> options):base(options)
        {
            
        }
        
    }
}