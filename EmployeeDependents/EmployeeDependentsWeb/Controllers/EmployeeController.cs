using EmployeeDependentsData.Models;
using EmployeeDependentsWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDependentsWeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly TestDbContext _context;
        int NUM_PAYCHECKS = 26;

        public EmployeeController(TestDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of Employees, with their dependents
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {            
            var employees = _context.Employees
                .Include(x => x.EmployeeDependents)
                .OrderBy(x => x.LastName)
                .ToList();
            return employees;
        }

        /// <summary>
        /// Calculate the deduction per employee, and their dependents
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost("CalculateDeduction")]
        public ActionResult<string> Get([FromBody] Employee employee)
        {
            double costs = 0;

            var foundEmployee = _context.Employees
                .Include(x => x.EmployeeDependents)
                .Where(x => x.FirstName == employee.FirstName && x.LastName == employee.LastName).FirstOrDefault();

            if (foundEmployee != null)
            {
                // check employee first name starts with 'A' or 'a', they get 10% discount
                if (foundEmployee.FirstName.StartsWith('A') || foundEmployee.FirstName.StartsWith('a'))
                {                    
                    costs += (1000 - 1000 * 0.1);
                }
                else
                {
                    costs += 1000;
                }

                // next check, do they have dependents?
                if (foundEmployee.EmployeeDependents.Any())
                {
                    foreach (var dependent in foundEmployee.EmployeeDependents)
                    {
                        if (dependent.FirstName.StartsWith('A') || dependent.FirstName.StartsWith('a'))
                        {
                            costs += (500 - 500 * 0.1);
                        }
                        else
                        {
                            costs += 500;
                        }
                    }

                }
            }
            
            // $2000 per paycheck, 26 paychecks in a year
            // costs we have are YEARLY. Divide by num paychecks to get per paycheck rate

            costs = Math.Round(costs / NUM_PAYCHECKS, 2);

            return Ok($"The deductions for this employee are ${costs} per pay period.");
        }

        [HttpPost]
        public IEnumerable<Employee> PostEmployee([FromBody] EmployeeVM employee)
        {

            // convert from EmployeeVM to employee
            var employeeToSave = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };

            _context.Employees.Add(employeeToSave);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
                // Log error in error table
            }

            // Do we have dependents?
            if (employee.Dependents != null && employee.Dependents.Any())
            {
                var dependentsToSave = new List<EmployeeDependent>();

                foreach (var dependent in employee.Dependents)
                {
                    var dependentToSave = new EmployeeDependent()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = dependent.FirstName,
                        LastName = dependent.LastName,
                        EmployeeId = employeeToSave.Id
                    };

                    dependentsToSave.Add(dependentToSave);
                }

                _context.EmployeeDependents.AddRange(dependentsToSave);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException e)
                {
                    throw;
                    // Log error in error table
                }
                catch (Exception e)
                {
                    throw new Exception(e.ToString());
                    // log general exception in error table
                }
            }

            // Lastly returns the list of employees, will trigger React UI to re-update the list

            var employees = _context.Employees
                .Include(x => x.EmployeeDependents)
                .ToList();
            return employees;

        }

        // STUBS section, didn't have time for this implementation


        //[HttpDelete]
        //public ActionResult<string> Delete([FromBody] EmployeeVM employee)
        //{

        //}

        //[HttpPut]
        //public ActionResult<string> Update([FromBody] EmployeeVM employee)
        //{

        //}

        //[HttpDelete]
        //public ActionResult<string> DeleteDependent([FromBody] EmployeeDependentVM dependent)
        //{

        //}

        //[HttpPut]
        //public ActionResult<string> UpdateDependent([FromBody] EmployeeDependentVM dependent)
        //{

        //}
    }
}
