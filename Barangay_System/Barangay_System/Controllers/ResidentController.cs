using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Barangay_System.Models;

namespace Barangay_System.Controllers
{
    public class ResidentController : ApiController
    {
        //
        // GET: /Resident/

        public IHttpActionResult GetAllResidents()
        {
            IList<ResidentViewModel> resis = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                resis = ctx.tblresidents.Select(u => new ResidentViewModel()
                {
                    ResidentID = u.ResidentID,
                    HouseholdID = u.HouseholdID,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Birthdate = u.Birthdate,
                    Sex = u.Sex,
                    CivilStatus = u.CivilStatus,
                    ContactNo = u.ContactNo,
                    Email = u.Email,
                    Occupation = u.Occupation
                }).ToList<ResidentViewModel>();
            }

            return Ok(resis);
        }

        // Read One Method
        public IHttpActionResult GetResidentById(string id)
        {
            ResidentViewModel u = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                u = ctx.tblresidents.Where(s => s.ResidentID == id)
                    .Select(s => new ResidentViewModel()
                    {
                        ResidentID = s.ResidentID,
                        HouseholdID = s.HouseholdID,
                        FirstName = s.FirstName,
                        MiddleName = s.MiddleName,
                        LastName = s.LastName,
                        Birthdate = s.Birthdate,
                        Sex = s.Sex,
                        CivilStatus = s.CivilStatus,
                        ContactNo = s.ContactNo,
                        Email = s.Email,
                        Occupation = s.Occupation
                    }).FirstOrDefault<ResidentViewModel>();
            }

            if (u == null)
                return NotFound();

            return Ok(u);
        }


        // Create Resident Method
        public IHttpActionResult PostNewResident(ResidentViewModel u)
        {
            if (u == null)
                return BadRequest("Invalid resident data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new db_barangay_systemEntities())
            {
                // Generate ResidentID
                var existingIDs = ctx.tblresidents
                    .Select(r => r.ResidentID)
                    .ToList();

                int maxNumber = 0;

                foreach (var existingID in existingIDs)
                {
                    int number;

                    if (!String.IsNullOrEmpty(existingID) &&
                        existingID.StartsWith("RES-") &&
                        Int32.TryParse(existingID.Substring(4), out number))
                    {
                        if (number > maxNumber)
                            maxNumber = number;
                    }
                }

                u.ResidentID = "RES-" + (maxNumber + 1).ToString("D3");

                var newResident = new tblresident()
                {
                    ResidentID = u.ResidentID,
                    HouseholdID = u.HouseholdID,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Birthdate = u.Birthdate,
                    Sex = u.Sex,
                    CivilStatus = u.CivilStatus,
                    ContactNo = u.ContactNo,
                    Email = u.Email,
                    Occupation = u.Occupation
                };

                ctx.tblresidents.Add(newResident);
                ctx.SaveChanges();
            }

            return Ok("Resident successfully created.");
        }

        // Update Method
        public IHttpActionResult PutResident(ResidentViewModel u)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new db_barangay_systemEntities())
            {
                var existingResident = ctx.tblresidents
                    .Where(s => s.ResidentID == u.ResidentID)
                    .FirstOrDefault<tblresident>();

                if (existingResident != null)
                {
                    existingResident.ResidentID = u.ResidentID;
                    existingResident.HouseholdID = u.HouseholdID;
                    existingResident.FirstName = u.FirstName;
                    existingResident.MiddleName = u.MiddleName;
                    existingResident.LastName = u.LastName;
                    existingResident.Birthdate = u.Birthdate;
                    existingResident.Sex = u.Sex;
                    existingResident.CivilStatus = u.CivilStatus;
                    existingResident.ContactNo = u.ContactNo;
                    existingResident.Email = u.Email;
                    existingResident.Occupation = u.Occupation;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // Delete Method
        public IHttpActionResult DeleteResident(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Not a valid id");

            using (var ctx = new db_barangay_systemEntities())
            {
                var u = ctx.tblresidents
                    .Where(s => s.ResidentID == id)
                    .FirstOrDefault();

                if (u == null)
                    return NotFound();


                if (ctx.tblrequests.Any(r => r.ResidentID == id) ||
                    ctx.tblusers.Any(x => x.ResidentID == id) ||
                    ctx.tblhouseholds.Any(h => h.HeadResidentID == id))
                    return BadRequest("This resident is still used by other records.");

                ctx.tblresidents.Remove(u);
                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
