using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Task_2.DataLayer;
using Task_2.Models;
using static System.Collections.Specialized.BitVector32;

namespace Task_2.Controllers
{
    public class OFC_EmployeeController : Controller
    {
        #region _Context

        private readonly ApplicationDbContext _context;

        public OFC_EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Select All
        public IActionResult Index()
        {

            var getAllEmployees = _context.Employees.FromSqlRaw("[dbo].[OFC_Emp_SelectAll]").ToList();

            return View( "OFC_Employee_List" ,getAllEmployees);
        }
        #endregion


        #region Get Single Row

        public IActionResult GetDetail(int? id)
        {
            var getSingleEmployee = _context.Employees.FromSqlRaw($"OFC_Emp_SelectByPk {id}").AsEnumerable().FirstOrDefault();
            return View(getSingleEmployee);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int EmpID)
        {
            Console.WriteLine(EmpID);

            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@EmpID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = EmpID
                }
            };

            var EmpID_Param = EmpID;

            var deleteEmployee = await _context.Database.ExecuteSqlRawAsync($"EXEC [dbo].[OFC_Emp_DeleteByPK] @EmpID" , param);

            if(deleteEmployee == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

            return View();
        }
        #endregion

        #region AddEdit
        public IActionResult AddEdit(int? EmpID)
        {
            #region DROP DOWN


            OFC_EmployeeModel _model = new OFC_EmployeeModel();
            _model.DeptLisE = new List<SelectListItem>();

            var data = _context.Departments.FromSqlRaw("[dbo].[OFC_Department_DropDown]").ToList();

            _model.DeptLisE.Add(new SelectListItem
            {
                Text = "---Select Department---",
                Value = "",
                Selected = false
            }
            );

            foreach (var item in data)
            {
                Console.WriteLine(item.DeptName + " " + item.DeptID );

                Console.WriteLine("----------------------------");

                _model.DeptLisE.Add(new SelectListItem
                {
                    Text = item.DeptName,
                    Value = item.DeptID.ToString()
                }

                );


            }

            Console.WriteLine(_model.DeptLisE[4].Value + _model.DeptLisE[4].Text + _model.DeptLisE[4].Selected);

            

            Console.WriteLine(_model.DeptLisE[4].Value + _model.DeptLisE[4].Text + _model.DeptLisE[4].Selected);
            #endregion

            if (EmpID != null)
            {

                var getSingleRecord = _context.Employees.FromSqlRaw($"OFC_Emp_SelectByPk {EmpID}").AsEnumerable().FirstOrDefault();
                /*OFC_EmployeeModel employeeModel = new OFC_EmployeeModel();*/
                _model.DeptID = getSingleRecord.DeptID;
                _model.PhoneNumber = getSingleRecord.PhoneNumber;
                _model.EmpName = getSingleRecord.EmpName;
                _model.Email = getSingleRecord.Email;
                _model.EmpID = getSingleRecord.EmpID;
                _model.Gender = getSingleRecord.Gender;
                _model.DateOfBirth = getSingleRecord.DateOfBirth;

                /*Console.WriteLine(_model.DeptLisE.Length);*/

                Console.WriteLine(getSingleRecord.EmpID);

                return View( "OFC_Employee_Add"  , /*getSingleRecord*/ _model);   

            }
                
            Console.WriteLine("INSERT Method");



            /*var setEmployees = _context.Employees.FromSqlRaw("OFC_Emp_SelectAll").ToList();*/
            Console.WriteLine("IN Add Action Method");
            /*Console.ReadLine();*/
            return View("OFC_Employee_Add",_model);
        }
        #endregion

        #region SAVE (INSERT / EDIT)
        public IActionResult Save(OFC_EmployeeModel _EmployeeModel)
        {
            Console.WriteLine("IN SAVE METHOD");

            #region Parameters

            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@DeptID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = _EmployeeModel.DeptID
                },
                new SqlParameter()
                {
                    ParameterName = "@EmpName",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = _EmployeeModel.EmpName
                },
                new SqlParameter()
                {
                    ParameterName = "@Email",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = _EmployeeModel.Email
                },
                new SqlParameter()
                {
                    ParameterName = "@PhoneNumber",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = _EmployeeModel.PhoneNumber
                },
                new SqlParameter()
                {
                    ParameterName = "@Gender",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = _EmployeeModel.Gender
                },
                new SqlParameter()
                {
                    ParameterName = "@DateOfBirth",
                    SqlDbType = System.Data.SqlDbType.Date,
                    Value = _EmployeeModel.DateOfBirth
                }
            };
            #endregion

            if (_EmployeeModel.EmpID == null)
            {
                Console.WriteLine(_EmployeeModel.DeptID);
                /*Console.ReadLine();*/
                var saveEmployee = _context.Database.ExecuteSqlRaw($"EXEC [dbo].[OFC_Emp_Insert] @DeptID,@EmpName,@Email,@PhoneNumber,@Gender,@DateOfBirth", param);

                if (saveEmployee == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("ERROR IS APPEARED");
                }
            }
            else
            {
                #region Parameters UPDT

                var paramUpdt = new SqlParameter[]
                {
                    new SqlParameter()
                    {
                        ParameterName = "@EmpID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = _EmployeeModel.EmpID
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@DeptID",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Value = _EmployeeModel.DeptID
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@EmpName",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Value = _EmployeeModel.EmpName
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Email",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Value = _EmployeeModel.Email
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@PhoneNumber",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Value = _EmployeeModel.PhoneNumber
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@Gender",
                        SqlDbType = System.Data.SqlDbType.VarChar,
                        Value = _EmployeeModel.Gender
                    },
                    new SqlParameter()
                    {
                        ParameterName = "@DateOfBirth",
                        SqlDbType = System.Data.SqlDbType.Date,
                        Value = _EmployeeModel.DateOfBirth
                    }
                };
                #endregion

                var saveEmployee = _context.Database.ExecuteSqlRaw($"EXEC [dbo].[OFC_Emp_UpdateByPK] @EmpID,@DeptID,@EmpName,@Email,@PhoneNumber,@Gender,@DateOfBirth", paramUpdt);

                if (saveEmployee == 1)
                {
                    Console.WriteLine("updated!!");
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("ERROR IS APPEARED");
                }

                
            }

            

            return RedirectToAction("Index");
        }
        #endregion

    }
}



