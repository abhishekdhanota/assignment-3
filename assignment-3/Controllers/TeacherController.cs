using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using assignment_3.Models;

namespace assignment_3.Controllers
{/// <summary>
/// this controller contains three clases 
/// list class will access the list of teachers
/// while the show class will display alll the attributes of a teacher 
/// </summary>
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET :/Teacher/List
        public ActionResult List(string SearchKey) 
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher>Teacher=controller.ListTeachers(SearchKey); 
            return View(Teacher);
        }
        //GET :/Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller=new TeacherDataController();
            Teacher NewTeacher= controller.FindTeacher(id);
          
            return View(NewTeacher);
        }



        //GET :/Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //POST :/Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("list");
        }

    }
}