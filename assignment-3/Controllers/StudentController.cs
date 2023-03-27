using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using assignment_3.Models;

namespace assignment_3.Controllers
{/// <summary>
/// this controller contains three classes 
/// class list will dsiplay list of students
/// class show will display all the specifications related to the student  
/// </summary>
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //GET :/Student/List
        public ActionResult List(string SearchKey)
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Student = controller.ListStudents(SearchKey);
            return View(Student);
        }
        //GET :/Student/Show/{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }
    }
}