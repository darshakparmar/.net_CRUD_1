using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_2.DataLayer;
using Task_2.Models;

namespace Task_2.Controllers
{
    public class OFC_DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OFC_DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            OFC_DepartmentModel _model = new OFC_DepartmentModel();
            _model.DeptList = new List<SelectListItem>();

            var data = _context.Departments.FromSqlRaw("[dbo].[OFC_Department_DropDown]").ToList();

            _model.DeptList.Add(new SelectListItem
            {
                Text = "Select Departmennt",
                Value = ""
            }
            );

            foreach (var item in data)
            {
                Console.WriteLine(item.DeptName + " " + item.DeptID);

                Console.WriteLine("----------------------------");

                _model.DeptList.Add(new SelectListItem
                {
                    Text = item.DeptName,
                    Value = item.DeptID.ToString()
                }

                );


            }

            

            return View(_model);

        }

       /* public IActionResult Index2()
        {
            var data = _context.Departments.FromSqlRaw("[dbo].[OFC_Department_DropDown]").ToList();

            foreach (var department in data)
            {
                // Access the properties of the department entity
                var departmentId = department.DeptID;
                var departmentName = department.DeptName;
                Console.WriteLine(departmentId + departmentName);
                // ... and so on
            }

            OFC_DepartmentModel _deptModel = new OFC_DepartmentModel();
            _deptModel.DeptList = new List<SelectListItem>();

            foreach (var department in data)
            {
                _deptModel.DeptList.Add(new SelectListItem
                {
                    Text = department.DeptName,
                    Value = department.DeptID.ToString()
                });
            }

           

            return View("Index", _deptModel);
        }*/
          
    }
}
