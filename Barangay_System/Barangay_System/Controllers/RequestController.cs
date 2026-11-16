using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Barangay_System.Models;

namespace Barangay_System.Controllers
{
    public class RequestController : ApiController
    {
        public IHttpActionResult GetAllRequests()
        {
            IList<RequestViewModel> req = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                req = ctx.tblrequests.Select(u => new RequestViewModel()
                {
                    RequestID = u.RequestID,
                    ResidentID = u.ResidentID,
                    DocTypeID = u.DocTypeID,
                    Purpose = u.Purpose,
                    DateRequested = u.DateRequested,
                    Status = u.Status,
                    ProcessedBy = u.ProcessedBy,
                    DateReleased = u.DateReleased,
                    Remarks = u.Remarks
                }).ToList<RequestViewModel>();
            }

            return Ok(req);
        }

        // Read One Method
        public IHttpActionResult GetRequestById(string id)
        {
            RequestViewModel u = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                u = ctx.tblrequests.Where(s => s.RequestID == id)
                    .Select(s => new RequestViewModel()
                    {
                        RequestID = s.RequestID,
                        ResidentID = s.ResidentID,
                        DocTypeID = s.DocTypeID,
                        Purpose = s.Purpose,
                        DateRequested = s.DateRequested,
                        Status = s.Status,
                        ProcessedBy = s.ProcessedBy,
                        DateReleased = s.DateReleased,
                        Remarks = s.Remarks
                    }).FirstOrDefault<RequestViewModel>();
            }

            if (u == null)
                return NotFound();

            return Ok(u);
        }

        // Create User Method
        public IHttpActionResult PostNewRequest(RequestViewModel u)
        {
            if (u == null)
                return BadRequest("Invalid Request data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new db_barangay_systemEntities())
            {
                var newRequest = new tblrequest()
                {
                    RequestID = u.RequestID,
                    ResidentID = u.ResidentID,
                    DocTypeID = u.DocTypeID,
                    Purpose = u.Purpose,
                    DateRequested = DateTime.Now,
                    Status = "Pending",
                    ProcessedBy = null, 
                    DateReleased = null,
                    Remarks = u.Remarks
                };

                ctx.tblrequests.Add(newRequest);
                ctx.SaveChanges();
            }

            return Ok("Request successfully created.");
        }

        // Update Method
        public IHttpActionResult PutRequest(RequestViewModel u)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new db_barangay_systemEntities())
            {
                var existingRequest = ctx.tblrequests
                    .Where(s => s.RequestID == u.RequestID)
                    .FirstOrDefault<tblrequest>();

                if (existingRequest != null)
                {
                    existingRequest.ResidentID = u.ResidentID;
                    existingRequest.DocTypeID = u.DocTypeID;
                    existingRequest.Purpose = u.Purpose;
                    existingRequest.Status = u.Status;
                    existingRequest.Remarks = u.Remarks;

                    // Preserve the existing request date
                    // Do not allow it to be changed during editing

                    // Preserve ProcessedBy unless a new value is provided
                    if (string.IsNullOrEmpty(existingRequest.ProcessedBy) &&
                    !string.IsNullOrEmpty(u.ProcessedBy))
                        {
                            existingRequest.ProcessedBy = u.ProcessedBy;
                        }

                    // Automatically set DateReleased
                    if (u.Status == "Released" &&
                        existingRequest.DateReleased == null)
                    {
                        existingRequest.DateReleased = DateTime.Now;
                    }

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
        public IHttpActionResult DeleteRequest(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Not a valid id");

            using (var ctx = new db_barangay_systemEntities())
            {
                var payment = ctx.tblpayments.FirstOrDefault(p => p.RequestID == id);
                if (payment != null && payment.Status == "Paid")
                    return BadRequest("This request has a paid payment and cannot be deleted.");
                if (payment != null)
                    ctx.tblpayments.Remove(payment);

                var u = ctx.tblrequests
                    .Where(s => s.RequestID == id)
                    .FirstOrDefault();

                if (u == null)
                    return NotFound();

                ctx.tblrequests.Remove(u);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
