using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using webAPI.Model.Entities;
using webAPI.Model.Repository;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository employee, IWebHostEnvironment env)
        {
            _employee = employee ?? 
                throw new ArgumentNullException(nameof(employee));
            _env = env;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get() {
            return Ok(await _employee.GetEmployees());
        }

        [HttpGet]
        [Route("GetEmployeeByID/{Id}")]
        public async Task<IActionResult> GetEmpByID(int Id) {
            return Ok(await _employee.GetEmployeeByID(Id));
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post(Employee emp) {
            var result = await _employee.InsertEmployee(emp);
            if (result.EmployeeID == 0) {
                return StatusCode(StatusCodes.Status500InternalServerError,"Something went wrong");
            }
            return Ok("Added Successfully");
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put(Employee emp) {
            await _employee.UpdateEmployee(emp);
            return Ok("Update Successfully");
        }

        [HttpPost]
        [Route("SaveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    stream.CopyTo(stream);
                }
                return new JsonResult(filename);

            }
            catch (Exception ex) {
                return new JsonResult("anonymous.png", ex);
            }
        }

    }
}
