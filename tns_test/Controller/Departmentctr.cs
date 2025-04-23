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
            return Ok(departments);

        }
        else
        {
            var departments = await _context.Departments.FromSqlRaw("SELECT * FROM \"Departments\" WHERE \"departmentId\" = {0}", id.Value).FirstOrDefaultAsync();

            if (departments == null)
            {
                return NotFound();
            }
            return Ok(departments);
        }

    }


    [HttpPut("update")]
    public async Task<IActionResult> Updatedepartment([FromBody] UpdateDepartmentRequest request)
    {


        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingDepartment = await _context.Departments.FromSqlRaw("SELECT * FROM \"Departments\" WHERE departmentname = {0}", request.department).FirstOrDefaultAsync();
        if (existingDepartment == null)
        {
            return NotFound("Department Not Found");
        }


        existingDepartment.departmentname = request.newdepartmentname ?? existingDepartment.departmentname;



        try
        {
            await _context.SaveChangesAsync();
            return Ok(existingDepartment);
        }
        catch (DbUpdateConcurrencyException)
        {
            var DepartmentstillExists = await _context.Departments.AnyAsync(d => d.departmentname == request.department);
        if (!DepartmentstillExists)
        {
            return NotFound("Department Not Found");
        }
        else
        {
            throw;
        }
        }


    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingDepartment = await _context.Departments.FromSqlRaw("SELECT * FROM \"Departments\" WHERE departmentname = {0}", request.department).FirstOrDefaultAsync();
        if (existingDepartment != null)
        {
            return BadRequest("This Department has been created");
        }
        Department departmentToAssign;

        var newDepartment = new Department
        {
            departmentname = request.department
        };
        _context.Departments.Add(newDepartment);
        await _context.SaveChangesAsync();
        departmentToAssign = newDepartment;

        return Ok(newDepartment);

    }
}