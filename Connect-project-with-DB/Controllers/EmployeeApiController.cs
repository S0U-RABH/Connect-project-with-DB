using Connect_project_with_DB.Data;
using Connect_project_with_DB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Connect_project_with_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        // step 5
        private readonly ApplicationDbContext _db;
        public EmployeeApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        // fetch data from database
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmps()
        {
            var emp = _db.Employees.ToList();
            return Ok(emp);
        }

        // fetch data from database using Id
        [HttpGet("id:int")]
        public ActionResult<Employee> GetEmp(int id)
        {
            //add method and decision if you like 
            var emp1 = _db.Employees.FirstOrDefault(u => u.Id == id);
            return Ok(emp1);
        }

        // post data into data base to create new record
        [HttpPost]
        public ActionResult<Employee> CreateEmp([FromBody]Employee emp)
        {
            Employee emp1 = new Employee()
            {
                Name = emp.Name,
                JobProfile = emp.JobProfile,
                City = emp.City,
                EmployeeId = emp.Id,
                
            };
            _db.Employees.Add(emp1);
            _db.SaveChanges();
            return Ok(NoContent());
        }

        // Update data into database
        [HttpPut("id:int")]
        public IActionResult UpdateEmp( int id, [FromBody]Employee emp)
        {
            var Emp1 = _db.Employees.FirstOrDefault(emp => emp.Id == id);
            Emp1.Name = emp.Name;
            Emp1.JobProfile = emp.JobProfile;
            Emp1.City = emp.City;
            Emp1.EmployeeId = emp.Id;

            _db.Employees.Update(Emp1);
            _db.SaveChanges();
            return Ok(NoContent());
        }

        // Delete data from database 
        [HttpDelete("id:int")]
        public IActionResult DeleteEmp(int id)
        {
            var emp = _db.Employees.FirstOrDefault(u =>u.Id == id);
            _db.Employees.Remove(emp);
            _db.SaveChanges();
            return Ok(NoContent());
        }

        // Patch data from data base 
        // using third party depandencies newtonsoftjson and jsonpatch from microsoft.
        // https://jsonpatch.com/
        // .AddNewtonsoftJson() in program.cs file in controllers.
        [HttpPatch("id:int")]
        public IActionResult PatchEmp(int id, JsonPatchDocument<Employee> Emp) 
        {
            var Emp1 = _db.Employees.FirstOrDefault(u => u.Id == id);

            Employee emp2 = new()
            {
                Name = Emp1.Name,
                JobProfile = Emp1.JobProfile,
                City = Emp1.City,
                EmployeeId = Emp1.Id
            };

            Emp.ApplyTo(emp2);

            Employee model = new()
            {
                Name = emp2.Name,
                JobProfile = emp2.JobProfile,
                City = emp2.City,
                EmployeeId = emp2.Id
            };

            _db.Employees.Update(model);
            _db.SaveChanges();
            return Ok(NoContent());
        }

    }
}
