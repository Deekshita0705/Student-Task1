using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Student_Task1.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Student_Task1.Controllers
{
    public class StudentController : Controller
    {
        System.Data.SqlClient.SqlConnection _connection;
        public ActionResult CreateStudent()
        {
            //List<Branch> branches = new List<Branch>();
            //List<SelectListItem> selectList = new List<SelectListItem>();       
            //while ()
            //{
            //    selectList.Add(new SelectListItem { Value = "1", Text = "CSE" });
            //}

            //var branchesNew  = 
            var branchitems = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "CSE" },
                new SelectListItem { Value = "2", Text = "ECE" },
                new SelectListItem { Value = "3", Text = "EEE" },
                new SelectListItem { Value = "4", Text = "IT" },
            };
           
            var yearitems = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "1st year" },
                new SelectListItem { Value = "2", Text = "2nd year" },
                new SelectListItem { Value = "3", Text = "3rd year" },
                new SelectListItem { Value = "4", Text = "4th year" },
            };
            var branchlist = new Student
            {
                BranchList = new SelectList(branchitems, "Value", "Text"),
                YearList = new SelectList(yearitems, "Value","Text")
            };
            return View(branchlist);
        }
        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            _connection = new SqlConnection("Data Source=DKOTHA-L-5509\\SQLEXPRESS;Initial Catalog=Task1;User ID=sa;Password=Welcome2evoke@1234");
            _connection.Open();
            int SelectListItem = student.BranchId;
            using (SqlCommand command = new SqlCommand("InsertStudent", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@BranchId", student.BranchId);
                command.Parameters.AddWithValue("@YearId", student.YearId);
                command.ExecuteNonQuery();
            }
            return RedirectToAction("StudentDetails");

        }
        public IActionResult StudentDetails()
        {
            _connection = new SqlConnection("Data Source=DKOTHA-L-5509\\SQLEXPRESS;Initial Catalog=Task1;User ID=sa;Password=Welcome2evoke@1234");
            _connection.Open();
            SqlCommand cmd = new SqlCommand("StudentDetails", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            List<StudentDetails> details = new List<StudentDetails>();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                details.Add(new StudentDetails
                {
                    Id = r.GetInt32("Id"),
                    FirstName = (string)r["FirstName"],
                    LastName = (string)r["LastName"],
                    BranchName = (string)r["BranchName"],
                    YearName = (string)r["YearName"]
                });
            }
            _connection.Close();
            return View(details);
        }
       
        [HttpGet("{Id}")]
        public ActionResult DeleteStudent(int Id)
        {
            _connection = new SqlConnection("Data Source=DKOTHA-L-5509\\SQLEXPRESS;Initial Catalog=Task1;User ID=sa;Password=Welcome2evoke@1234");
            _connection.Open();
            SqlCommand cmd = new SqlCommand("DeleteStudent", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", Id));
            cmd.ExecuteNonQuery();
            _connection.Close();
            return RedirectToAction("StudentDetails");

        }
        [HttpGet]
        public ActionResult EditStudent(int Id)
        {
            StudentDetails student = new StudentDetails();
            _connection = new SqlConnection("Data Source=DKOTHA-L-5509\\SQLEXPRESS;Initial Catalog=Task1;User ID=sa;Password=Welcome2evoke@1234");
            _connection.Open();
            SqlCommand cmd = new SqlCommand("GetDetails", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                student.Id = (int)r["Id"];
                student.FirstName = (string)r["FirstName"];
                student.LastName = (string)r["LastName"];
                student.BranchName = (string)r["BranchName"];
                student.YearName = (string)r["YearName"];
            }            
            _connection.Close();
            return View(student);
        }
        [HttpPost]
        public ActionResult EditStudent(int Id,StudentDetails student) 
        {
            _connection = new SqlConnection("Data Source=DKOTHA-L-5509\\SQLEXPRESS;Initial Catalog=Task1;User ID=sa;Password=Welcome2evoke@1234");
            _connection.Open();
            SqlCommand cmd = new SqlCommand("EditStudent", _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", Id));
            cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
            cmd.Parameters.AddWithValue("@LastName", student.LastName);
            cmd.Parameters.AddWithValue("@BranchName", student.BranchName);
            cmd.Parameters.AddWithValue("@YearName", student.YearName);
            cmd.ExecuteNonQuery();
            _connection.Close();
            return RedirectToAction("StudentDetails");

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
