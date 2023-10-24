using BlazorWebAPIStroedProcedure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BlazorWebAPIStroedProcedure.DataRepository
{
    public class StudentRepo
    {
        public readonly string _connectionString;
        public StudentRepo(string connectionString ) { 
            _connectionString = connectionString;
        }
        public bool InsertStudent(
            string studentId, char gender, string nationality, string placeOfBirth, string stageId,
            string gradeId, char sectionId, string topic, char semester, string relation,
            int raisedHands, int visitedResources, int announcementsView, int discussion,
            string parentAnsweringSurvey, string parentSchoolSatisfaction, string studentAbsenceDays,
            int studentMarks, char classes)
            {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("InsertStudent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Student_ID", studentId);
                        command.Parameters.AddWithValue("@gender", gender);
                        command.Parameters.AddWithValue("@NationalITy", nationality);
                        command.Parameters.AddWithValue("@PlaceOfBirth", placeOfBirth);
                        command.Parameters.AddWithValue("@StageID", stageId);
                        command.Parameters.AddWithValue("@GradeID", gradeId);
                        command.Parameters.AddWithValue("@SectionID", sectionId);
                        command.Parameters.AddWithValue("@Topic", topic);
                        command.Parameters.AddWithValue("@Semester", semester);
                        command.Parameters.AddWithValue("@Relation", relation);
                        command.Parameters.AddWithValue("@RaisedHands", raisedHands);
                        command.Parameters.AddWithValue("@VisitedResources", visitedResources);
                        command.Parameters.AddWithValue("@AnnouncementsView", announcementsView);
                        command.Parameters.AddWithValue("@Discussion", discussion);
                        command.Parameters.AddWithValue("@ParentAnsweringSurvey", parentAnsweringSurvey);
                        command.Parameters.AddWithValue("@ParentschoolSatisfaction", parentSchoolSatisfaction);
                        command.Parameters.AddWithValue("@StudentAbsenceDays", studentAbsenceDays);
                        command.Parameters.AddWithValue("@Student_Marks", studentMarks);
                        command.Parameters.AddWithValue("@Class", classes);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            }

       public bool UpdateStudent(
            string studentId, char gender, string nationality, string placeOfBirth, string stageId,
            string gradeId, char sectionId, string topic, char semester, string relation,
            int raisedHands, int visitedResources, int announcementsView, int discussion,
            string parentAnsweringSurvey, string parentSchoolSatisfaction, string studentAbsenceDays,
            int studentMarks, char classes)
            {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("UpdateStudent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Student_ID", studentId);
                        command.Parameters.AddWithValue("@gender", gender);
                        command.Parameters.AddWithValue("@NationalITy", nationality);
                        command.Parameters.AddWithValue("@PlaceOfBirth", placeOfBirth);
                        command.Parameters.AddWithValue("@StageID", stageId);
                        command.Parameters.AddWithValue("@GradeID", gradeId);
                        command.Parameters.AddWithValue("@SectionID", sectionId);
                        command.Parameters.AddWithValue("@Topic", topic);
                        command.Parameters.AddWithValue("@Semester", semester);
                        command.Parameters.AddWithValue("@Relation", relation);
                        command.Parameters.AddWithValue("@RaisedHands", raisedHands);
                        command.Parameters.AddWithValue("@VisitedResources", visitedResources);
                        command.Parameters.AddWithValue("@AnnouncementsView", announcementsView);
                        command.Parameters.AddWithValue("@Discussion", discussion);
                        command.Parameters.AddWithValue("@ParentAnsweringSurvey", parentAnsweringSurvey);
                        command.Parameters.AddWithValue("@ParentschoolSatisfaction", parentSchoolSatisfaction);
                        command.Parameters.AddWithValue("@StudentAbsenceDays", studentAbsenceDays);
                        command.Parameters.AddWithValue("@Student_Marks", studentMarks);
                        command.Parameters.AddWithValue("@Class", classes);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
               
            }

        public List<Student> GetAllStudents()

        {
            try
            {
                List<Student> students = new List<Student>();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetAllStudents", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())

                            {
                                students.Add(MapStudentFromDataReader(reader));
                            }
                        }
                    }
                }
                return students;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Student GetStudentByID(string studentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetStudentByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Student_ID", studentId);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())

                            {
                                return MapStudentFromDataReader(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private Student MapStudentFromDataReader(SqlDataReader reader)

        {
            try
            {
                return new Student
                {
                    StudentId = reader["Student_ID"].ToString(),
                    Gender = reader["gender"].ToString()[0],
                    NationalIty = reader["NationalIty"].ToString(),
                    PlaceofBirth = reader["PlaceofBirth"].ToString(),
                    StageId = reader["StageID"].ToString(),
                    GradeId = reader["GradeID"].ToString(),
                    SectionId = reader["SectionID"].ToString()[0],
                    Topic = reader["Topic"].ToString(),
                    Semester = reader["Semester"].ToString()[0],
                    Relation = reader["Relation"].ToString(),
                    Raisedhands = Convert.ToInt32(reader["raisedhands"]),
                    VisItedResources = Convert.ToInt32(reader["VisITedResources"]),
                    AnnouncementsView = Convert.ToInt32(reader["AnnouncementsView"]),
                    Discussion = Convert.ToInt32(reader["Discussion"]),
                    ParentAnsweringSurvey = reader["ParentAnsweringSurvey"].ToString(),
                    ParentschoolSatisfaction = reader["ParentschoolSatisfaction"].ToString(),
                    StudentAbsenceDays = reader["StudentAbsenceDays"].ToString(),
                    StudentMarks = Convert.ToInt32(reader["Student_Marks"]),
                    Class = reader["Class"].ToString()[0]
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool DeleteStudentByID(string studentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand("DeleteStudentByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Student_ID", studentId);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
