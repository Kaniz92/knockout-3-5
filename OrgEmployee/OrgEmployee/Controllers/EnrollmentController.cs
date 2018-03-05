using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrgEmployee.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace OrgEmployee.Controllers
{
    public class EnrollmentController : Controller
    {
        private OrganizationDataEntities db = new OrganizationDataEntities();

        //
        // GET: /Enrollment/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllEnrollments()
        {
           //db.Configuration.ProxyCreationEnabled = false;
            var enrollments = db.Enrollments.Include(e => e.Department).Include(e => e.Employee);
            var enrollmentList = new List<Enrollment>();

            foreach(var item in enrollments){
                var employee = new Employee{
                    EmployeeID= item.Employee.EmployeeID,
                    LastName = item.Employee.LastName,
                };
                var department = new Department{
                    DepartmentID= item.Department.DepartmentID,
                    Title=item.Department.Title,
                };
                var Enrollment = new Enrollment{
                    Employee = employee,
                    Department= department,
                    EnrollmentID= item.EnrollmentID,
                    Band = item.Band,
                };
                enrollmentList.Add(Enrollment);
            }

            return Json(enrollmentList, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /Enrollment/Details/5

        public ActionResult Details(int id = 0)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        //
        // GET: /Enrollment/Create

        public ActionResult Create()
        {
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Departments = JsonConvert.SerializeObject(db.Departments.ToList());
            ViewBag.Employees = JsonConvert.SerializeObject(db.Employees.ToList());
            return View();
        }

        //
        // POST: /Enrollment/Create

        [HttpPost]
        public string Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return "success";
            }

            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Title", enrollment.DepartmentID);
            //ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "LastName", enrollment.EmployeeID);
            return "fail";

        }

        //
        // GET: /Enrollment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ViewBag.Departments = JsonConvert.SerializeObject(db.Departments.ToList());
            ViewBag.Employees = JsonConvert.SerializeObject(db.Employees.ToList());
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return null;
            }

        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    ViewBag.InitialData = JsonConvert.SerializeObject(enrollment);

  
            return View();
        }

        //
        // POST: /Enrollment/Edit/5

        [HttpPost]
        public string Edit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return "Enrollment Edited";
            }

            return "Edit Failed";
        }

        //
        // GET: /Enrollment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        //
        // POST: /Enrollment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}