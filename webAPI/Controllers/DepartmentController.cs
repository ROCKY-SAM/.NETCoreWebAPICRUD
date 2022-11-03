using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using webAPI.Model.Entities;
using webAPI.Model.Repository;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController:ControllerBase
    {
        private readonly IDepartmentRepository _department;
        public DepartmentController(IDepartmentRepository department) {
            _department = department ?? throw new ArgumentNullException(nameof(department));
        }

        [HttpGet]
        [Route("GetDepartment")]
        public async Task<IActionResult> Get() {
            return Ok(await _department.GetDepartments());
        }

        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post(Department dep) {
            var result = await _department.InsertDepartment(dep);
            if (result.DepartmentId == 0) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
            return Ok("Added Successfully");
        }

        [HttpGet]
        [Route("GetDepartmentByID/{id}")]
        public async Task<IActionResult> GetDeptById(int Id) {
            return Ok(await _department.GetDepartmentById(Id));
        }

        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put(Department dep) {
            await _department.UpdateDepartment(dep);
            return Ok("Update Successfully");
        }

        [HttpDelete]
        [Route("DeleteDepartment")]
        public async Task<IActionResult>  Delete(int Id)
        {
            _department.DeleteDepartment(Id);
            return Ok("Deleted Successfully");
        }
    }
}
