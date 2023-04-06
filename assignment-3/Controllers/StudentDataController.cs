using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using assignment_3.Models;


namespace assignment_3.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Teachers table of our blog database.
        /// <summary>
        /// Returns a list of students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of Students (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/liststudents/{searchkey}")]
        public IEnumerable<Student> ListStudents(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ', studentlname)) like lower(@key) or lower(studentnumber) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Student> Students = new List<Student> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int studentId = Convert.ToInt32(ResultSet["studentid"]);
                string studentfname = ResultSet["studentfname"].ToString();
                string studentlname = ResultSet["studentlname"].ToString();
                string studentno = ResultSet["studentnumber"].ToString();
                string enroldate = ResultSet["enroldate"].ToString();
                
                Student NewStudent = new Student();
                NewStudent.studentId = studentId;
                NewStudent.studentfname = studentfname;
                NewStudent.studentlname = studentfname;
                NewStudent.studentno = studentno;
                NewStudent.enroldate = enroldate;

                //Add the Teacher Name to the List
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return Students;
        }

        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from students where studentid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {

                int studentId = Convert.ToInt32(ResultSet["studentid"]);
                string studentfname = ResultSet["studentfname"].ToString();
                string studentlname = ResultSet["studentlname"].ToString();
                string studentno = ResultSet["studentnumber"].ToString();
                string enroldate = ResultSet["enroldate"].ToString();

                NewStudent.studentId = studentId;
                NewStudent.studentfname = studentfname;
                NewStudent.studentlname = studentfname;
                NewStudent.studentno = studentno;
                NewStudent.enroldate = enroldate;
            }
            return NewStudent;
        }

    }
}
