using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using tns_test.Data;
using tns_test.Models;
using System.Collections.Generic;
using System.Linq;


[ApiController]
[Route("api/users/")]

public class UserController : ControllerBase
{

    private readonly DataContext _context;
    public UserController(DataContext context)
    {
        _context = context;
    }

    //Get User
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<User>>> GetUser([FromQuery] int? id)
    {

        // ถ้าไม่มีการส่ง Params id มาให้ Getall
        if (!id.HasValue)
        {
            var getuser = await _context.Users.FromSqlRaw("SELECT * FROM \"Users\"").ToListAsync();
            return Ok(getuser);
        }
        else
        {
            var getuser = await _context.Users.FromSqlRaw("SELECT * FROM \"Users\" WHERE userid = {0}", id.Value).FirstOrDefaultAsync();
            if (getuser == null)
            {
                return NotFound();
            }

            return Ok(getuser);
        }
    }

    //Create User
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.email == request.email);
        if (existingUser != null)
        {
            return BadRequest("This User already created.");
        }

        var existingDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.departmentname == request.department);
        Department departmentToAssign;
        if (existingDepartment == null)
        {
            var newDepartment = new Department
            {
                departmentname = request.department
            };
            _context.Departments.Add(newDepartment);
            await _context.SaveChangesAsync();
            departmentToAssign = newDepartment;
        }
        else
        {
            departmentToAssign = existingDepartment;
        }

        // 2. สร้าง User ใหม่
        var newUser = new User
        {
            firstname = request.firstname,
            lastname = request.lastname,
            email = request.email,
            departmentid = departmentToAssign.departmentid,
            Department = departmentToAssign 
        };

        // 3. เพิ่ม User ลงใน Context
        await _context.Users.AddAsync(newUser);

        // 4. บันทึก User ลงใน Database
        int insertResult = await _context.SaveChangesAsync();

        if (insertResult > 0)
        {
            return CreatedAtAction(nameof(GetUser), new { id = newUser.userid }, newUser);
        }
        else
        {
            return StatusCode(500, "Failed to create user.");
        }
    }
}