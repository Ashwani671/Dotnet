using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WEBAPP.Models

{

    public enum Course
    {
        DAC ,DBDA,DITIS 
    }
    [Index(nameof(Email),IsUnique= true)]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Field is Empty!!")]
        public string Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Field is Empty!!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field is Empty!!")]
        public string Phone { get; set; }
        [EnumDataType(typeof(Course))]
        public Course course{ get; set; }

    }
}
