using Angular2.DAL;
using Angular2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Angular2.Controllers
{
    public class EmployeeController : Controller
    {
        public Connection con { get; private set; }
        public IConfiguration _configuration { get; private set; }
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
            con = new Connection(_configuration["ConnectionStrings:Defaultconnection"].ToString());
        }
        public IActionResult Index()
        {

            return View();
        }
        public JsonResult Create()
        {
            var add = con.GetStudents();

            return Json(add);
        }
        public JsonResult Add(string Name, string Email, string Phone, int EmployeeID)
        {
            Employee employee = new Employee
            {
                EmployeeID = EmployeeID,
                Name = Name,
                Email = Email,
                Phone = Phone,
            };

            return Json(con.Add(employee));
        }

        public JsonResult Delete(int employeeID)
        {
            Employee? employee = con.GetStudents().Where(obj => obj.EmployeeID == employeeID).FirstOrDefault();
            if (employee != null)
            {
                con.Delete(employeeID);
                return Json(new { success = true });

            }
            else { return Json(new { success = false }); }

        }

        public JsonResult Update(string Name, string Email, string Phone, int EmployeeID)
        {
            Employee employee = new Employee
            {
                EmployeeID = EmployeeID,
                Name = Name,
                Email = Email,
                Phone = Phone,
            };
            return Json(con.Update(employee));
        }
        public JsonResult GetbyID(int ID)
        {
            var conn = con.GetStudents().FirstOrDefault(o => o.EmployeeID.Equals(ID));
            return Json(conn);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }

}
    

