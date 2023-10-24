using BlazorWebAPIStroedProcedure.DataRepository;
using BlazorWebAPIStroedProcedure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace BlazorWebAPIStroedProcedure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepo _studentRepo;
        private readonly ILogger _logger;


        public StudentController(StudentRepo studentRepo, ILogger<StudentController> logger)
        {
            _studentRepo = studentRepo;
            _logger = logger;
        }

        [Route("AddStudentData")]
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            if (student != null && ModelState.IsValid)
            {

                bool success = _studentRepo.InsertStudent(
                    student.StudentId, student.Gender, student.NationalIty, student.PlaceofBirth,
                    student.StageId, student.GradeId, student.SectionId, student.Topic,
                    student.Semester, student.Relation, student.Raisedhands.Value,
                    student.VisItedResources.Value, student.AnnouncementsView.Value, student.Discussion.Value,
                    student.ParentAnsweringSurvey, student.ParentschoolSatisfaction, student.StudentAbsenceDays,
                    student.StudentMarks.Value, student.Class
                );
                if (success)
                {
                    _logger.LogInformation("Student data is inserted successfully");
                    return Ok("Student data inserted successfully.");
                }
                else
                {
                    _logger.LogWarning("Student data not found");
                    return BadRequest("Failed to insert student.");
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPut("updateStudentData/{id}")]
        //[HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Student student)
        {
            bool success = _studentRepo.UpdateStudent(
                id, student.Gender, student.NationalIty, student.PlaceofBirth,
                student.StageId, student.GradeId, student.SectionId, student.Topic,
                student.Semester, student.Relation, student.Raisedhands.Value,
                student.VisItedResources.Value, student.AnnouncementsView.Value, student.Discussion.Value,
                student.ParentAnsweringSurvey, student.ParentschoolSatisfaction, student.StudentAbsenceDays,
                student.StudentMarks.Value, student.Class
            );
            if (success)
            {
                return Ok("Student updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update student.");
            }
        }

        [Route("getAllStudentData")]
        [HttpGet]
        public IActionResult Get()
        {
            List<Student> students = _studentRepo.GetAllStudents();
            return Ok(students);
        }



        [Route("getStudentDataById")]
        [HttpGet]
        //[HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Student student = _studentRepo.GetStudentByID(id);
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound("Student not found.");
            }
        }

        [Route("deleteStudentData")]
        [HttpDelete]
        //[HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            bool success = _studentRepo.DeleteStudentByID(id);
            if (success)
            {
                return Ok("Student deleted successfully.");
            }
            else
            {
                return BadRequest("Failed to delete student.");
            }
        }
    }
}
