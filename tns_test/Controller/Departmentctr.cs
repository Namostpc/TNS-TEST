using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tns_test.Data;
using tns_test.Models;
using System.Collections.Generic;
using System.Linq;


[ApiController]
[Route("api/department/")]
public class DepartmentController : ControllerBase
{

    // สำหรับเก็บ Instance ของ DataContext
    private readonly DataContext _context;
    public DepartmentController(DataContext context)
    {
        _context = context;
    }

    // GET: api/department/
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartment([FromQuery] int? id)
    {

        if (!id.HasValue)
        {
            var departments = await _context.Departments.FromSqlRaw("SELECT * FROM \"Departments\"").ToListAsync();
            if (departments == null || !departments.Any())
            {
                return NoContent();
            }
            return Ok(departments);

        }else {
            var departments = await _context.Departments.FromSqlRaw("SELECT * FROM \"Departments\" WHERE \"departmentId\" = {0}", id.Value).FirstOrDefaultAsync();

        if (departments == null)
        {
            return NotFound();
        }
        return Ok(departments);
        }

    }


    [HttpPut("update")]
    public async Task<IActionResult> Updatedepartment([FromBody] int? id)
    {

        if(!id.HasValue){
            return BadRequest("id is required");
        }
        var getDepartment = await _context.Departments.FromSqlRaw("SELECT * FROM \"Departments\" WHERE \"departmentId\" = {0}", id.Value).FirstOrDefaultAsync();

        if (getDepartment == null)
        {
            return NotFound();

        }



        return NoContent();
    }

}