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

            return Ok();
        }
    }
}
