using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*
Class qui represente le table de la base de donnes
*/
namespace bookingApi1DataAccess.Models
{
    [Table("BTReservations")]
    public class Reservations
    {
        [Key]
        [Column("btrIdReservation")]
        public int idReservacion { get; set; }
        [Column("btrIdClient")]
        public int ClientsidClient { get; set; }
        public virtual Clients? Clients { get; set; }
        [Column("btrStartDate")]
        public int startDate { get; set; }
        [Column("btrEndDate")]
        public int endDate { get; set; }
        [Column("btrIdRoom")]
        public int idRoom { get; set; }
        /*
        //constructor vide pour le Entity Framework
        public Reservation()
        {
            
        }
        //transformer les donnees Json à une object Clients pour l'interaction avec Entity Framework
        //J'utilise dynamic car le project bookingApi1DataAccess ne puet pas accéder au project bookingApi2BusinessLogic
        //parce que il aura un redundant dependency
        public Reservation(dynamic dto)
        {
            this.userName=dto.userName;
            this.password=dto.password;
        }
        */        
    }
}