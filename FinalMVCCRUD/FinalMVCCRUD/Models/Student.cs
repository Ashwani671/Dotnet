using System.ComponentModel.DataAnnotations;

namespace FinalMVCCRUD.Models
{
    public enum Course
    {
        DAC,DBDA,DITIS
    }
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Field is not Empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field is not Empty")]
        public string Address { get; set; }
        [EnumDataType(typeof(Course))]
        public Course course1 { get; set; }
            
       
    }

   
}
