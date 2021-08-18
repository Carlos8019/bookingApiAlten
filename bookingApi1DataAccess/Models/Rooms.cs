using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
/*
Class qui represente le table de la base de donnes
*/
namespace bookingApi1DataAccess.Models
{
    [Table("BTRooms")]
    public class Rooms
    {
        [Key]
        [Column("btrIdRoom")]
        public int idRoom { get; set; }
        [Column("btrCodeRoom")]
        public string codeRoom { get; set; }
        [Column("btrDescriptionRoom")]
        public string descriptionRoom { get; set; }
        [Column("btrPriceRoom")]
        public double priceRoom { get; set; }
    }
}