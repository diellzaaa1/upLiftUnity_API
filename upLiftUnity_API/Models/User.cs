using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace upLiftUnity_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Surname { get; set; }
      
        public string Email { get; set; }
      
        public string Password { get; set; }
       
        public string PhoneNumber {  get; set; }
      
        public string Address { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId {  get; set; }
        
        public Role? UserRole { get; }






    }
}
