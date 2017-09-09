using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SakshamEMSApi.Models;
using SakshamEMSApi.Enums;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace SakshamEMSApi.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DBSakshamEntities db = new DBSakshamEntities();

        [HttpGet,ActionName("Login")]
        public ActionResult Login()
        {
            Session["LoggedIn"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.LoginID == "admin" && model.Password == "silive")
            {
                Session["LoggedIn"] = "true";
                return RedirectToAction("Index");
            }
            ViewBag.InvalidCredentials = "Invalid Credentials";
            return View();
        }
        public ActionResult Index(Student model)
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<Student> studentsList = db.tblStudents.Select(x => new Student
            {
                StudentID = x.StudentID,
                Name = x.Name,
                StudentNo = x.StudentNo,
                Branch = x.Branch,
                Year = x.Year,
                ContactNumber = x.ContactNumber,
                SportsInterested = x.SportsInterested,
                Hosteler = x.Hosteler,
                Gender = x.Gender
            }).ToList();

            ViewBag.StudentList = studentsList;

            return View();
        }

        #region EditStudent
        //Action for Editing Student
        public ActionResult EditStudent(int StudentID)
        {
            DBSakshamEntities db = new DBSakshamEntities();

            Student model = new Student();
            if (StudentID > 0)
            {
                tblStudent objtblStudent = db.tblStudents.SingleOrDefault(x => x.StudentID == StudentID);
                model.StudentID = objtblStudent.StudentID;
                model.Name = objtblStudent.Name;
                model.StudentNo = objtblStudent.StudentNo;
                model.Branch = objtblStudent.Branch;
                model.Year = objtblStudent.Year;
                model.ContactNumber = objtblStudent.ContactNumber;
                model.SportsInterested = objtblStudent.SportsInterested;
                model.Hosteler = objtblStudent.Hosteler;
                model.Gender = objtblStudent.Gender;
            }
            return PartialView("EditStudentPartial", model);
        }

        //Function to save edited details of student
        [HttpPost]
        public ActionResult SaveStudentDetails(Student model)
        {
            if (ModelState.IsValid)
            {
                if (model.StudentID > 0) //Edit the Details of students
                {
                    tblStudent objtblStudent = db.tblStudents.SingleOrDefault(x => x.StudentID == model.StudentID);

                    objtblStudent.StudentID = model.StudentID;
                    objtblStudent.Name = model.Name;
                    objtblStudent.StudentNo = model.StudentNo;
                    objtblStudent.Branch = model.Branch;
                    objtblStudent.Year = model.Year;
                    objtblStudent.ContactNumber = model.ContactNumber;
                    objtblStudent.SportsInterested = model.SportsInterested;
                    objtblStudent.Hosteler = model.Hosteler;
                    objtblStudent.Gender = model.Gender;

                    db.SaveChanges();

                    TempData["updated"] = "<script>alert('Details Updated');</script>";
                }

            }
            return RedirectToAction("Index");
        }
        #endregion

        #region DeleteStudent
        //function to delete Student
        [HttpPost]
        public JsonResult DeleteStudent(int StudentID)
        {
            bool result = false;

            tblStudent objtblStudent = db.tblStudents.SingleOrDefault(x => x.StudentID == StudentID);
            if (objtblStudent != null)
            {
                result = true;
                db.tblStudents.Remove(objtblStudent);
            }
            db.SaveChanges();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Excel
        public ActionResult ExportToExcel()
        {
            List<Student> studentsList = db.tblStudents.Select(x => new Student
            {
                StudentID = x.StudentID,
                Name = x.Name,
                StudentNo = x.StudentNo,
                Branch = x.Branch,
                Year = x.Year,
                ContactNumber = x.ContactNumber,
                SportsInterested = x.SportsInterested,
                Hosteler = x.Hosteler,
                Gender = x.Gender
            }).ToList();

            var gridview = new GridView();
            gridview.DataSource = studentsList;
            gridview.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=StudentRegistrations.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gridview.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View("Index");
        }
        #endregion
        public ActionResult Logout()
        {
            Session["LoggedIn"] = null;
            return RedirectToAction("Login","Admin");

        }
    }
}