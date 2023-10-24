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
        public IActionResult PostStudentData([FromBody] Student student)
        {
            try
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
            }
            catch (Exception ex)
            {

                _logger.LogError("Error Finding  the  student with specified id : {ErrorMessage}", ex.Message);
                
            }
            return BadRequest(ModelState);
        }

        [HttpPut("updateStudentData/{id}")]
        //[HttpPut("{id}")]
        public IActionResult PutStudentData(string id, [FromBody] Student student)
        {
            try
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
                    _logger.LogInformation("Student updated successfully,");
                    return Ok("Student updated successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to update student.");
                    return BadRequest("Failed to update student.");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Error Finding  the  student with specified id : {ErrorMessage}", ex.Message);
            }
            return BadRequest("Failed to update student.");
        }

        [Route("getAllStudentData")]
        [HttpGet]
        public IActionResult GetStudentsData()
        {
            try
            {
                List<Student> students = _studentRepo.GetAllStudents();
                _logger.LogInformation("Successfully fetched !!");
                return Ok(students);
            }
            catch (Exception ex)
            {

                _logger.LogError("Error Finding  the  student : {ErrorMessage}", ex.Message);
            }
            return BadRequest(ModelState); 
        }



        [Route("getStudentDataById")]
        [HttpGet]
        //[HttpGet("{id}")]
        public IActionResult GetStudentDataById(string id)
        {
            try
            {
                Student student = _studentRepo.GetStudentByID(id);
                if (student != null)
                {
                    _logger.LogInformation("Student data successfull fetched!!");
                    return Ok(student);
                }
                else
                {
                    _logger.LogWarning("Student data not found");
                    return NotFound("Student not found.");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Error Finding  the  student with specified id : {ErrorMessage}", ex.Message);
            }
            return BadRequest(ModelState);
        }

        [Route("deleteStudentData")]
        [HttpDelete]
        //[HttpDelete("{id}")]
        public IActionResult DeleteStudentData(string id)
        {
            try
            {
                bool success = _studentRepo.DeleteStudentByID(id);
                if (success)
                {
                    _logger.LogInformation("Student deleted successfully.");
                    return Ok("Student deleted successfully.");
                }
                else
                {
                    _logger.LogWarning("Failed to delete student.");
                    return BadRequest("Failed to delete student.");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Error Finding  the  student with specified id : {ErrorMessage}", ex.Message);
            }
            return BadRequest(ModelState);  
        }
    }
}
