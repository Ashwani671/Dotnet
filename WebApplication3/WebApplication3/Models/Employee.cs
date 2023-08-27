using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public enum Department
    {
        SALES, MARKETING, HR
    }
    public class Employee

    {
        [Key]
       
        public int Id { get; set; }
        [Required(ErrorMessage ="Field is required")]  
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }
        [EnumDataType(typeof(Department))]
        public Department dept { get; set; }
      
    }
}
