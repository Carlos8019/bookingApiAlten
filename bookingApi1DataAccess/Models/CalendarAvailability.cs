using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*
Class qui represente le table de la base de donnes
*/
namespace bookingApi1DataAccess.Models
{
    [Table("BTCalendarAvailability")]
    public class CalendarAvailability
    {
        [Key]
        [Column("btcIdCalendar")]
        public int idCalendar { get; set; }
        
        [Column("btcDateCalendar")]
        public int date { get; set; }
        [Column("btcStatusCalendar")]
        public int status { get; set; }

    }
}