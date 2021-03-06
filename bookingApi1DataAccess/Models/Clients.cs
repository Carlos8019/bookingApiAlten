using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
/*
Class qui represente le table de la base de donnes
*/
namespace bookingApi1DataAccess.Models
{
    [Table("BTClients")]
    public class Clients
    {
        [Key]
        [Column("btcIdClient")]
        public int idClient { get; set; }
        
        [Column("btcUserName")]
        public string userName { get; set; }
        [Column("btcPassword")]
        public string password { get; set; }
        //constructor vide pour Entity Framework
        public Clients()
        {
            
        }
        //transformer les donnees Json à une object Clients pour l'interaction avec le Entity Framework
        //J'utilise dynamic car le project bookingApi1DataAccess ne puet pas accéder au project bookingApi2BusinessLogic
        //parce que il aura un redundant dependency
        public Clients(dynamic dto)
        {
            this.userName=dto.userName;
            this.password=dto.password;
        }
    }
}