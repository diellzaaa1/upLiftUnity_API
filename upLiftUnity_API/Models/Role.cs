using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace upLiftUnity_API.Models
{
    public class Role

    {
        [Key]
        public int Id { get; set; }

     
        public string RoleDesc { get; set; }


    }
}