using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Barangay_System.Models;

namespace Barangay_System.Controllers
{
    public class HouseholdController : ApiController
    {
        public IHttpActionResult GetAllHouseholds()
        {
            IList<HouseholdViewModel> req = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                req = ctx.tblhouseholds.Select(u => new HouseholdViewModel()
                {
                    HouseholdID = u.HouseholdID,
                    HouseholdNo = u.HouseholdNo,
                    Purok = u.Purok,
                    Address = u.Address,
                    HeadResidentID = u.HeadResidentID
                }).ToList<HouseholdViewModel>();
            }

            return Ok(req);
        }

        // Read One Method
        public IHttpActionResult GetRequestById(string id)
        {
            HouseholdViewModel u = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                u = ctx.tblhouseholds.Where(s => s.HouseholdID == id)
                    .Select(s => new HouseholdViewModel()
                    {
                        HouseholdID = s.HouseholdID,
                        HouseholdNo = s.HouseholdNo,
                        Purok = s.Purok,
                        Address = s.Address,
                        HeadResidentID = s.HeadResidentID
                    }).FirstOrDefault<HouseholdViewModel>();
            }

            if (u == null)
                return NotFound();

            return Ok(u);
        }


        // Create Household Method
        public IHttpActionResult PostNewHousehold(HouseholdViewModel u)
        {
            if (u == null)
                return BadRequest("Invalid Household data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new db_barangay_systemEntities())
            {
                var existingIDs = ctx.tblhouseholds
                    .Select(h => h.HouseholdID)
                    .ToList();

                int maxNumber = 0;

                foreach (var existingID in existingIDs)
                {
                    int number;

                    if (!String.IsNullOrEmpty(existingID) &&
                        existingID.StartsWith("HH-") &&
                        Int32.TryParse(existingID.Substring(3), out number))
                    {
                        if (number > maxNumber)
                            maxNumber = number;
                    }
                }

                u.HouseholdID = "HH-" + (maxNumber + 1).ToString("D3");

                var newHousehold = new tblhousehold()
                {
                    HouseholdID = u.HouseholdID,
                    HouseholdNo = u.HouseholdNo,
                    Purok = u.Purok,
                    Address = u.Address,
                    HeadResidentID = u.HeadResidentID
                };

                ctx.tblhouseholds.Add(newHousehold);
                ctx.SaveChanges();
            }

            return Ok("Household successfully created.");
        }


        public IHttpActionResult PutHousehold(HouseholdViewModel u)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new db_barangay_systemEntities())
            {
                var existingHousehold = ctx.tblhouseholds
                    .Where(s => s.HouseholdID == u.HouseholdID)
                    .FirstOrDefault<tblhousehold>();

                if (existingHousehold != null)
                {
                    existingHousehold.HouseholdNo = u.HouseholdNo;
                    existingHousehold.Purok = u.Purok;
                    existingHousehold.Address = u.Address;
                    existingHousehold.HeadResidentID = u.HeadResidentID;

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
        public IHttpActionResult DeleteHousehold(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Not a valid id");

            using (var ctx = new db_barangay_systemEntities())
            {
                if (ctx.tblresidents.Any(r => r.HouseholdID == id))
                    return BadRequest("This household still has residents.");

                var u = ctx.tblhouseholds
                    .Where(s => s.HouseholdID == id)
                    .FirstOrDefault();

                if (u == null)
                    return NotFound();

                ctx.tblhouseholds.Remove(u);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
