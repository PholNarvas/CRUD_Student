using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class StudentController : Controller
    {
    private readonly IStudentService _StudentService;
    private readonly CrudDbContext _crudDbContext;
    public StudentController(IStudentService studentservice, CrudDbContext crudDbContext)
    {
        _StudentService = studentservice;
        _crudDbContext = crudDbContext;
    }
       
        [HttpGet]
        public async Task<IActionResult> Create ()
        {
        var departments = await _crudDbContext.Departments.ToListAsync();
        ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");

        var classes = await _crudDbContext.Classes.ToListAsync();
        ViewBag.Classes = new SelectList(classes, "ClassID", "SectionName");

        return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create (Student student)
        {
            if(ModelState.IsValid)
            {
            var success = await _StudentService.CreateStudentAsync(student);

                if (success)
                {
                    TempData["SuccessMessage"] = "Student Created Successfully";
                    return RedirectToAction("List");
                }
            }
            var departments = await _crudDbContext.Departments.ToListAsync();
            ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");

            var classes = await _crudDbContext.Classes.ToListAsync();
            ViewBag.Classes = new SelectList(classes, "ClassID", "SectionName");
        return View(student);
            }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var departments = await _crudDbContext.Departments.ToListAsync();
        ViewBag.Departments = new SelectList(departments, "DepartmentID", "DepartmentName");

        var classes = await _crudDbContext.Classes.ToListAsync();
        ViewBag.Classes = new SelectList(classes, "ClassID", "SectionName");

        var student = await _StudentService.GetStudentByIdAsync(id);
        if (student == null)
            return NotFound();

        return View(student);
    }

 
    [HttpPost]
    public async Task<IActionResult> UpdateStudent(Student student)
    {
        if (ModelState.IsValid)
        {
            var success = await _StudentService.UpdateStudentAsync(student);
            if (!success)

            return BadRequest("Update Failed");
            return RedirectToAction("List");


        }
        return Ok(student);
    }

    public async Task<IActionResult> DeleteStudent (int id)
    {
        if(ModelState.IsValid)
        {
            var isSuccess = await _StudentService.DeleteStudentAsync(id);

            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Student has been Deleted successfully";
                return RedirectToAction("List");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Delete Student";
            }
        }
        return RedirectToAction("List");

    }

        [HttpGet]
        public async Task<IActionResult> List (string search, string value)
        {
                IEnumerable<Student> students;
            if (string.IsNullOrWhiteSpace(search) || string.IsNullOrWhiteSpace(value))
            {
                students = await _StudentService.GetAllStudentAsync();
            }
            else
            {
                students = await _StudentService.SearchStudentAsync(search, value);
            }

                return View(students);

         }

    }

