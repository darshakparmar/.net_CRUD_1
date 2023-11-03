using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_2.Models
{
    [Table("OFC_Deparrtment")]
    public class OFC_DepartmentModel
    {
        [Key]
        public int? DeptID { get; set; }

        public string? DeptName { get; set; }

        [NotMapped]
        public List<SelectListItem> DeptList { get; set; }

        /*public  int? SelectED { get; set; }*/
    }
}
