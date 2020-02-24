using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCRegistrationForm.ADO;
using MVCRegistrationForm.Models;
namespace MVCRegistrationForm.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Index()
        {
            ADOOperations operations = new ADOOperations();

            return View(operations.View());
        }
        public ActionResult Get()
        {
            ADOOperations operations = new ADOOperations();

            return View(operations.AdapterView());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentModel studentModel)
        {
            ADOOperations operations = new ADOOperations();
            operations.Insert(studentModel);
            return RedirectToAction("Get");
        }
        public ActionResult ConnectedCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConnectedCreate(StudentModel studentModel)
        {
            ADOOperations operations = new ADOOperations();
            operations.ConnectedInsert(studentModel);
            return RedirectToAction("Index");
        }
        public ActionResult Edit()
        {

        }
    }
}