using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Barangay_System.Models;
namespace Barangay_System.Controllers
{
    public class DocumentTypeController : ApiController
    {
        public IHttpActionResult GetAllDocumentTypes()
        {
            IList<DocumentTypeViewModel> req = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                req = ctx.tbldocumenttypes.Select(u => new DocumentTypeViewModel()
                {
                    DocTypeID = u.DocTypeID,
                    Name = u.Name,
                    Description = u.Description,
                    Fee = u.Fee,
                    ProcessingDays = u.ProcessingDays

                }).ToList<DocumentTypeViewModel>();
            }

            return Ok(req);
        }

        // Read One Method
        public IHttpActionResult GetRequestById(string id)
        {
            DocumentTypeViewModel u = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                u = ctx.tbldocumenttypes.Where(s => s.DocTypeID == id)
                    .Select(s => new DocumentTypeViewModel()
                    {
                        DocTypeID = s.DocTypeID,
                        Name = s.Name,
                        Description = s.Description,
                        Fee = s.Fee,
                        ProcessingDays = s.ProcessingDays


                    }).FirstOrDefault<DocumentTypeViewModel>();
            }

            if (u == null)
                return NotFound();

            return Ok(u);
        }

        // Create User Method
        public IHttpActionResult PostNewDocumentType(DocumentTypeViewModel u)
        {
            if (u == null)
                return BadRequest("Invalid document type data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new db_barangay_systemEntities())
            {

                var existingIDs = ctx.tbldocumenttypes
                .Select(d => d.DocTypeID)
                .ToList();

                int maxNumber = 0;

                foreach (var existingID in existingIDs)
                {
                    int number;

                    if (!String.IsNullOrEmpty(existingID) &&
                        existingID.StartsWith("DOC-") &&
                        Int32.TryParse(existingID.Substring(4), out number))
                    {
                        if (number > maxNumber)
                            maxNumber = number;
                    }
                }

                u.DocTypeID = "DOC-" + (maxNumber + 1).ToString("D3");

                var newDoctype = new tbldocumenttype()
                {
                    DocTypeID = u.DocTypeID,
                    Name = u.Name,
                    Description = u.Description,
                    Fee = u.Fee,
                    ProcessingDays = u.ProcessingDays
                };

                ctx.tbldocumenttypes.Add(newDoctype);
                ctx.SaveChanges();
            }

            return Ok("Document Type successfully created.");
        }

        public IHttpActionResult PutDocumentType(DocumentTypeViewModel u)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new db_barangay_systemEntities())
            {
                var existingDocumentType = ctx.tbldocumenttypes
                    .Where(s => s.DocTypeID == u.DocTypeID)
                    .FirstOrDefault<tbldocumenttype>();

                if (existingDocumentType != null)
                {
                    existingDocumentType.DocTypeID = u.DocTypeID;
                    existingDocumentType.Name = u.Name;
                    existingDocumentType.Description = u.Description;
                    existingDocumentType.Fee = u.Fee;
                    existingDocumentType.ProcessingDays = u.ProcessingDays;

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
        public IHttpActionResult DeleteDocumentType(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Not a valid id");

            using (var ctx = new db_barangay_systemEntities())
            {
                if (ctx.tblrequests.Any(r => r.DocTypeID == id))
                    return BadRequest("This document type is used by requests.");

                var u = ctx.tbldocumenttypes
                    .Where(s => s.DocTypeID == id)
                    .FirstOrDefault();

                if (u == null)
                    return NotFound();

                ctx.tbldocumenttypes.Remove(u);
                ctx.SaveChanges();
            }

            return Ok();
    
     }
    }
}
