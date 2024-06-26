using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CollegeProject1.Pages.page1
{
    public class Index1Model : PageModel
    {
        public List<Student> listStudents = [];
        [BindProperty]
        public string? RollNo { get; set; }
        [BindProperty]
        public string? ClassName { get; set; }
        [BindProperty]
        public string? ExamName { get; set; }

        public class Student
        {
            public String? StudentName;
            public String? Class;
            public String? Exam;
            public String? Roll;
            public String? Address;
            public String? Result;
            public String? TotalMark;
            public String? Percentage;
        }
        public void OnPost()
        {
            string connectionString = "Server=LOLO;Database=CollegeProject;User Id=sa;Password=lolo@123;";
            string query1 = "exec Result @RollNo=@rollno,@ClassName=@classname,@ExamName=@exam";


            using SqlConnection connection = new(connectionString);
            try
            {
                connection.Open();
                using SqlCommand command = new(query1, connection);

                command.Parameters.AddWithValue("rollno", RollNo);
                command.Parameters.AddWithValue("classname", ClassName);
                command.Parameters.AddWithValue("exam", ExamName);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Student obj = new()
                    {
                        StudentName = reader["StudentName"].ToString(),
                        Class = reader["ClassName"].ToString(),
                        Exam = reader["ExamName"].ToString(),
                        Roll = reader["RollNo"].ToString(),
                        Address = reader["Address"].ToString(),
                        Result = reader["Result"].ToString(),
                        TotalMark = reader["TotalMark"].ToString(),
                        Percentage = reader["Percentage"].ToString()
                    };

                    listStudents.Add(obj);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         
        }
        public void OnGet()
        {

        }
    }
}
