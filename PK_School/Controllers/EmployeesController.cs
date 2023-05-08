using Microsoft.AspNetCore.Mvc;
using PK_School.Models;
using System.Linq;

namespace PK_School.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeesController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public IActionResult Index()
        {
            var result = _employeeDbContext.Employees.ToList();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employee()
                {
                    Name = employee.Name,
                    City=employee.City,
                    State=employee.State,
                    Salary=employee.Salary
                };
                _employeeDbContext.Employees.Add(emp);
                _employeeDbContext.SaveChanges();
                TempData["error"] = "Record Save";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Empty field Can't Submit";
                return RedirectToAction("Index");
            }
           
        }
        
        public IActionResult Edit(int id)
        {

                var emp = _employeeDbContext.Employees.SingleOrDefault(e => e.Id == id);            
                var result = new Employee()
                {
                    Name = emp.Name,
                    City = emp.City,
                    State = emp.State,
                    Salary = emp.Salary
                };
            return View(result);
                     

        }
        [HttpPost]
       public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employee()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    City = employee.City,
                    State = employee.State,
                    Salary = employee.Salary

                };
                _employeeDbContext.Employees.Update(emp);
                _employeeDbContext.SaveChanges();
                TempData["error"] = "Record Update";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Empty field Can't Submit";
                return RedirectToAction("Index");
            }
         
        }
        public IActionResult Delete(int id)
        {
            var emp = _employeeDbContext.Employees.SingleOrDefault(e=>e.Id ==id);
            _employeeDbContext.Employees.Remove(emp);
            _employeeDbContext.SaveChanges();
            TempData["error"] = "Record Delete";
            return RedirectToAction("Index");
        }
    }
}
