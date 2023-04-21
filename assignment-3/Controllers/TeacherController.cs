using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using assignment_3.Models;
using System.Diagnostics;
using Google.Protobuf.WellKnownTypes;
using Mysqlx.Datatypes;

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
            IEnumerable<Teacher> Teacher = controller.ListTeachers(SearchKey);
            return View(Teacher);
        }
        //GET :/Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }



        //GET :/Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //POST :/Teacher/Delete/{id}
        public
            ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("list");
        }

        //GET :Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string Salary, string EmployeeNumber)
        {
            Debug.WriteLine("i have accessed the create method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);


            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeNo = EmployeeNumber;
            NewTeacher.Salary = Salary;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        //get: /Author/update/{id}
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }



        /// <summary>
        /// this is a post request used to update the actual
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TeacherFname"></param>
        /// <param name="TeacherLname"></param>
        /// <param name="Salary"></param>
        /// <param name="EmployeeNumber"></param>
        /// <returns></returns>
        /// <example>
        /// POST : /Author/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Loves Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        //POST:/Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id,string TeacherFname, string TeacherLname, string Salary, string EmployeeNumber)
        {

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeNo = EmployeeNumber;
            TeacherInfo.Salary = Salary;
            TeacherInfo.TeacherId = id;


            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id,TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}
