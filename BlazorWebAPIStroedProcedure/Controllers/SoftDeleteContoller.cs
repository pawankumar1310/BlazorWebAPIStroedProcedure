using BlazorWebAPIStroedProcedure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BlazorWebAPIStroedProcedure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftDeleteContoller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SoftDeleteContoller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("Recovery")]
        [HttpPost]
        public ActionResult Recovery()
        {
            SqlConnection connString;
            SqlCommand cmd;
            SqlDataAdapter adap;
            DataTable dtb;
            connString = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings"));
            try
            {
                dtb = new DataTable();
                cmd = new SqlCommand("select * from StudentExtra ", connString);
                connString.Open();
                adap = new SqlDataAdapter(cmd);
                adap.Fill(dtb);
                List<Student> students = new List<Student>();
                foreach (DataRow dataRow in dtb.Rows)
                {
                    cmd = new SqlCommand("insert into Student values ('" + dataRow["Student_ID"] + "','" + dataRow["gender"] + "','" + dataRow["NationalITy"] + "','" + dataRow["PlaceOfBirth"] + "','" + dataRow["StageID"] + "', '" + dataRow["GradeID"] + "','" + dataRow["SectionID"] + "' ,'" + dataRow["Topic"] + "' ,'" + dataRow["Semester"] + "' , '" + dataRow["Relation"] + "' , '" + dataRow["raisedhands"] + "','" + dataRow["VisITedResources"] + "','" + dataRow["AnnouncementsView"] + "','" + dataRow["Discussion"] + "', '" + dataRow["ParentAnsweringSurvey"] + "', '" + dataRow["ParentschoolSatisfaction"] + "', '" + dataRow["StudentAbsenceDays"] + "', '" + dataRow["Student_Marks"] + "', '" + dataRow["Class"] + "'  )", connString);
                    cmd.ExecuteNonQuery();
                    students.Add(new Student
                    {
                        StudentId = dataRow["Student_ID"].ToString(),
                        Gender = Convert.ToChar(dataRow["gender"]),
                        NationalIty = dataRow["NationalITy"].ToString(),
                        PlaceofBirth = dataRow["PlaceofBirth"].ToString(),
                        StageId = dataRow["StageID"].ToString(),
                        GradeId = dataRow["GradeID"].ToString(),
                        SectionId = Convert.ToChar(dataRow["SectionID"]),
                        Topic = dataRow["Topic"].ToString(),
                        Semester = Convert.ToChar(dataRow["Semester"]),
                        Relation = dataRow["Relation"].ToString(),
                        Raisedhands = int.Parse(dataRow["raisedhands"].ToString()),
                        VisItedResources = int.Parse(dataRow["VisITedResources"].ToString()),
                        AnnouncementsView = int.Parse(dataRow["AnnouncementsView"].ToString()),
                        Discussion = int.Parse(dataRow["Discussion"].ToString()),
                        ParentAnsweringSurvey = dataRow["ParentAnsweringSurvey"].ToString(),
                        ParentschoolSatisfaction = dataRow["ParentschoolSatisfaction"].ToString(),
                        StudentAbsenceDays = dataRow["StudentAbsenceDays"].ToString(),
                        StudentMarks = int.Parse(dataRow["Student_Marks"].ToString()),
                        Class = Convert.ToChar(dataRow["Class"])
                    });

                }
                cmd = new SqlCommand("TRUNCATE TABLE StudentExtra", connString);
                cmd.ExecuteNonQuery();
                return Ok(students);
            }
            catch (Exception ef)
            {
                return BadRequest(ef.Message);
            }
            finally { connString.Close(); }
        }

        [Route("Delete/{id}")]
        [HttpDelete] //DELETE
        public ActionResult Delete(string id)
        {
            SqlConnection connString;
            SqlCommand cmd;
            SqlDataAdapter adap;
            DataTable dtb;
            connString = new SqlConnection(_configuration.GetConnectionString("ConnectionStrings"));
            try
            {
                dtb = new DataTable();
                cmd = new SqlCommand("select * from Student where Student_ID = @StudentID", connString);
                cmd.Parameters.AddWithValue("@StudentID ", id);
                connString.Open();
                adap = new SqlDataAdapter(cmd);
                adap.Fill(dtb);
                DataRow dr = dtb.Rows[0];
                cmd = new SqlCommand("insert into StudentExtra values ('" + dr["Student_ID"] + "','" + dr["gender"] + "','" + dr["NationalITy"] + "','" + dr["PlaceofBirth"] + "','" + dr["StageID"] + "', '" + dr["GradeID"] + "','" + dr["SectionID"] + "' ,'" + dr["Topic"] + "' ,'" + dr["Semester"] + "' , '" + dr["Relation"] + "' , '" + dr["raisedhands"] + "','" + dr["VisITedResources"] + "','" + dr["AnnouncementsView"] + "','" + dr["Discussion"] + "', '" + dr["ParentAnsweringSurvey"] + "', '" + dr["ParentschoolSatisfaction"] + "', '" + dr["StudentAbsenceDays"] + "', '" + dr["Student_Marks"] + "', '" + dr["Class"] + "' , '" + 1 + "' , GETDATE() )", connString);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from Student where Student_ID=@StudentID" + "", connString);
                cmd.Parameters.AddWithValue("@StudentID", id);
                //connString.Open();
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    return Ok(new { Message = "Record Deleted!" });
                }
                return BadRequest(new { Message = "Record Not found!" });
            }
            catch (Exception ef)
            {
                return BadRequest(ef.Message);
            }
            finally { connString.Close(); }
        }

    }
}
