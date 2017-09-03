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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
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
                using (DBSakshamEntities db = new DBSakshamEntities())
                {
                    db.tblStudents.Add(objtblStudent);
                    db.SaveChanges();
                }

                string name = objtblStudent.Name;
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK,name));
            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occureed. Try again."));
        }
    }
}
