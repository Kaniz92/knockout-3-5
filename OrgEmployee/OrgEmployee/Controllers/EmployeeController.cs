using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrgEmployee.Models;
using System.Web.Script.Serialization;

namespace OrgEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private OrganizationDataEntities db = new OrganizationDataEntities();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public JsonResult GetAllEmployee()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Employees, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public string Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return "success";
            }

            return "fail";
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return null;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.InitialData = serializer.Serialize(employee);
            return View();
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public string Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return "Employee Edited";
            }
            return "Edit Failed";
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.InitialData = serializer.Serialize(employee);
            return View();
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost]
        public string Delete(Employee employee)
        {
            Employee employeeDetail = db.Employees.Find(employee.EmployeeID);
            db.Employees.Remove(employeeDetail);
            db.SaveChanges();
            return "Employee Deleted";
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
