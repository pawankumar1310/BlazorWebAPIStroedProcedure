using BlazorWebAPIStroedProcedure.Models;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using ClosedXML.Excel;

namespace BlazorWebAPIStroedProcedure.Controllers
{
    public class DatabaseController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public readonly ILogger _logger;
        public DatabaseController(IConfiguration iConfiguratioin, ILogger<DatabaseController> ilogger)
        {
            _configuration = iConfiguratioin;
            _logger = ilogger;
        }
        [Route("ImportCsv")]
        [HttpPost]
        public IActionResult ImportCsv(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("No file uploaded.");
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                IEnumerable<Student> csvData = csv.GetRecords<Student>();
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
                {
                    connection.Open();
                    foreach (var csvStudent in csvData)
                    {
                        if (!IsDuplicateStudent(connection, csvStudent.StudentId))
                        {
                            InsertCsvStudent(connection, csvStudent);
                        }
                    }
                    connection.Close();
                }
            }
            _logger.LogInformation("Student Data imported Successfully: StudentID ");
            return Ok("CSV data imported successfully.");
        }
        private bool IsDuplicateStudent(SqlConnection connection, string studentId)
        {
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Student WHERE Student_ID = @Student_ID", connection))
            {
                command.Parameters.AddWithValue("@Student_ID", studentId);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private void InsertCsvStudent(SqlConnection connection, Student csvStudent)
        {
            using (var command = new SqlCommand("INSERT INTO Student VALUES (@Student_ID, @Gender, @NationalITy, @PlaceofBirth, @StageID, @GradeID, @SectionID, @Topic, @Semester, @Relation, @raisedhands, @VisITedResources, @AnnouncementsView, @Discussion, @ParentAnsweringSurvey, @ParentschoolSatisfaction, @StudentAbsenceDays, @Student_Marks, @Class)", connection))
            {
                command.Parameters.AddWithValue("@Student_ID", csvStudent.StudentId);
                command.Parameters.AddWithValue("@Gender", csvStudent.Gender);
                command.Parameters.AddWithValue("@NationalITy", csvStudent.NationalIty);
                command.Parameters.AddWithValue("@PlaceofBirth", csvStudent.PlaceofBirth);
                command.Parameters.AddWithValue("@StageID", csvStudent.StageId);
                command.Parameters.AddWithValue("@GradeID", csvStudent.GradeId);
                command.Parameters.AddWithValue("@SectionID", csvStudent.SectionId);
                command.Parameters.AddWithValue("@Topic", csvStudent.Topic);
                command.Parameters.AddWithValue("@Semester", csvStudent.Semester);
                command.Parameters.AddWithValue("@Relation", csvStudent.Relation);
                command.Parameters.AddWithValue("@raisedhands", csvStudent.Raisedhands);
                command.Parameters.AddWithValue("@VisITedResources", csvStudent.VisItedResources);
                command.Parameters.AddWithValue("@AnnouncementsView", csvStudent.AnnouncementsView);
                command.Parameters.AddWithValue("@Discussion", csvStudent.Discussion);
                command.Parameters.AddWithValue("@ParentAnsweringSurvey", csvStudent.ParentAnsweringSurvey);
                command.Parameters.AddWithValue("@ParentschoolSatisfaction", csvStudent.ParentschoolSatisfaction);
                command.Parameters.AddWithValue("@StudentAbsenceDays", csvStudent.StudentAbsenceDays);
                command.Parameters.AddWithValue("@Student_Marks", csvStudent.StudentMarks);
                command.Parameters.AddWithValue("@Class", csvStudent.Class);
                command.ExecuteNonQuery();
            }
        }


        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings")))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SELECT Student_ID, gender, NationalITy, PlaceOfBirth, StageID, GradeID, SectionID, Topic, Semester, Relation, raisedhands, VisITedResources, AnnouncementsView, Discussion, ParentAnsweringSurvey, ParentschoolSatisfaction, StudentAbsenceDays, Student_Marks, Class FROM Student", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);

                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Student");
                            worksheet.Cell(1, 1).InsertTable(dataTable);
                            worksheet.Columns().AdjustToContents();

                            using (var stream = new System.IO.MemoryStream())
                            {
                                workbook.SaveAs(stream);
                                var content = stream.ToArray();
                                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentTest.xlsx");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Finding  the  student with specified id : {ErrorMessage}", ex.Message);
                return BadRequest("Error exporting data to Excel: " + ex.Message);
            }
        }



        [HttpDelete("Truncate")]
        public IActionResult TruncateDatabase()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectionStrings");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    using (var command = new SqlCommand("TRUNCATE TABLE Student", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Database truncated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Finding  the  student with specified id : {ErrorMessage}", ex.Message);
                return BadRequest($"Failed to truncate the database: {ex.Message}");
            }
        }
    }
}
