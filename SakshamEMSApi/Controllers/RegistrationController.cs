using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SakshamEMSApi.Models;

namespace SakshamEMSApi.Controllers
{
    public class RegistrationController : ApiController
    {
        DBSakshamEntities db = new DBSakshamEntities();
        [HttpPost]
        public IHttpActionResult Register(Student model)
        {
            if (ModelState.IsValid)
            {
                tblStudent objtblStudent = new tblStudent();
                objtblStudent.Name = model.Name;
                objtblStudent.StudentNo = model.StudentNo;
                objtblStudent.Branch = model.Branch;
                objtblStudent.Year = model.Year;
                objtblStudent.ContactNumber = model.ContactNumber;
                objtblStudent.SportsInterested = model.SportsInterested;
                objtblStudent.Hosteler = model.Hosteler;

                db.tblStudents.Add(objtblStudent);
                db.SaveChanges();

                int id = objtblStudent.StudentID;
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, id.ToString()));
            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occureed. Try again."));
        }
    }
}
