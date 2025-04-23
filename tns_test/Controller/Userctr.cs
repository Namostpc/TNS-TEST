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
            var getuser = await _context.Database.SqlQueryRaw<UserWithDepartment>("SELECT u.userid, u.firstname, u.lastname, u.email, t.departmentname, t.departmentid FROM \"Users\" as u INNER JOIN \"Departments\" AS t ON t.departmentid = u.departmentid").ToListAsync();
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

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateuserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingUser = await _context.Users.FromSqlRaw("SELECT * FROM \"Users\" WHERE email = {0}", request.email).FirstOrDefaultAsync();

        if (existingUser == null)
        {
            return NotFound("User Not Found");
        }
        ;

        // ถ้ามีข้อมูลใหม่ ให้ใช้ข้อมูลใหม่
        existingUser.firstname = request.firstname ?? existingUser.firstname;
        existingUser.lastname = request.lastname ?? existingUser.lastname;

        if (!string.IsNullOrEmpty(request.department))
        {
            var existingDepartment = await _context.Departments.FromSqlRaw("SELECT departmentid FROM \"Department\" WHERE departmentname = {0}", request.department).FirstOrDefaultAsync();
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
                existingUser.departmentid = existingDepartment.departmentid;
            }
        }
        try
        {
            await _context.SaveChangesAsync();
            return Ok(existingUser);
        }
        catch (DbUpdateConcurrencyException)
        {
            var userStillExists = await _context.Users.AnyAsync(u => u.email == request.email);
            if (!userStillExists)
            {
                return NotFound("User Not Found");
            }
            else
            {
                throw;
            }
        }

    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DelteUser([FromQuery] int? id)
    {
        if (id.HasValue)
        {
            var userToDelete = await _context.Users.FromSqlRaw("SELECT * FROM \"Users\" WHERE userid = {0}", id.Value).FirstOrDefaultAsync();

            if (userToDelete == null)
            {
                return NotFound("User Not Found");
            }

            _context.Users.Remove(userToDelete);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "User deleted successfully" });
            }
            catch (DbUpdateConcurrencyException)
            {
                // ตรวจสอบว่า User ยังมีอยู่ใน Database โดยตรง
                var userStillExists = await _context.Users.AnyAsync(e => e.userid == id);
                if (!userStillExists)
                {
                    return NotFound("User Not Found");
                }
                else
                {
                    throw;
                }
            }
        }
        else
        {
            return BadRequest("require id");
        }

    }

}