using bookingApi2BusinessLogic.Interfaces;
using System;
using System.Globalization;
using System.Threading.Tasks;
namespace bookingApi2BusinessLogic.Utilities
{
    /*
    Cette classe permet gestioner l'information de dates
    */
    public class ManageDates : IManageDates
    {
        //obtenir le jour actuel
        public int startDate { get; set; }

        public ManageDates()
        {

        }
        //obtenir les prochaines n jours apres le jour actuel
        public Task<int> GetMaxDate(int days)
        {
            //debuter par obtenir le date du jour actuel
            var day = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            int.TryParse(day, out var dateTmp);
            startDate = dateTmp;

            //obtenir le jour actuel en le format requerant pour additionner les jours
            var tmpDate = DateTime.ParseExact(startDate.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
            var nextDays = tmpDate.AddDays(days).ToString("yyyy-MM-dd");
            //transformant le jour additionne
            DateTime dt = DateTime.ParseExact(nextDays, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            int.TryParse(dt.ToString("yyyyMMdd"), out var result);
            return Task.FromResult(result);
        }
    }
}