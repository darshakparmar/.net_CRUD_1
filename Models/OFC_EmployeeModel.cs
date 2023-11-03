using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_2.Models
{
    public class OFC_EmployeeModel
    {
        [Key]
        public int? EmpID { get; set; }

        [Required]
        public string? EmpName { get; set; }

        
        public string? DeptName { get; set; }

        
        [Required , Browsable(false)]
        [EmailAddress]
        public string? Email { get; set; }

        /*[RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid number.")]*/
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be  10 characters")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Select Your Department")]
        public int? DeptID { get; set; }

        [Required(ErrorMessage = "Please choose your Gender.")]
        public string? Gender { get; set; }

        /*[DefaultValue(typeof(DateTime), DateTime.ToString("yyyy-MM-dd"))]*/
        [Required]
        public DateTime? DateOfBirth { get; set; }

        [NotMapped]
        public List<SelectListItem> DeptLisE { get; set; }
        
    }

    public enum Gender
    {
        Male,
        Female,
        Others
    }
}
