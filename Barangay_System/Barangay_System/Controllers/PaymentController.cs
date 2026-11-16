using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Barangay_System.Models;

namespace Barangay_System.Controllers
{
    public class PaymentController : ApiController
    {
        public IHttpActionResult GetAllPayments()
        {
            IList<PaymentViewModel> req = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                req = ctx.tblpayments.Select(u => new PaymentViewModel()
                {
                    PaymentID = u.PaymentID,
                    RequestID = u.RequestID,
                    Amount = u.Amount,
                    PaymentMethod = u.PaymentMethod,
                    ORNumber = u.ORNumber,
                    PaymentDate = u.PaymentDate,
                    Status = u.Status
                }).ToList<PaymentViewModel>();
            }

            return Ok(req);
        }

        // Read One Method
        public IHttpActionResult GetPaymentById(string id)
        {
            PaymentViewModel u = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                u = ctx.tblpayments.Where(s => s.PaymentID == id)
                    .Select(s => new PaymentViewModel()
                    {
                        PaymentID = s.PaymentID,
                        RequestID = s.RequestID,
                        Amount = s.Amount,
                        PaymentMethod = s.PaymentMethod,
                        ORNumber = s.ORNumber,
                        PaymentDate = s.PaymentDate,
                        Status = s.Status
                       
                    }).FirstOrDefault<PaymentViewModel>();
            }

            if (u == null)
                return NotFound();

            return Ok(u);
        }

        // Create Payment Method
        public IHttpActionResult PostNewPayment(PaymentViewModel u)
        {
            if (u == null)
                return BadRequest("Invalid Payment data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new db_barangay_systemEntities())
            {
                // Find the selected request
                var existingRequest = ctx.tblrequests
                    .FirstOrDefault(r => r.RequestID == u.RequestID);

                if (existingRequest == null)
                    return BadRequest("Request not found.");

                // Only approved requests can be paid
                if (existingRequest.Status != "Approved")
                    return BadRequest("Only approved requests can be paid.");

                // Prevent duplicate payments
                var existingPayment = ctx.tblpayments
                    .FirstOrDefault(p => p.RequestID == u.RequestID);

                if (existingPayment != null)
                    return BadRequest("This request already has a payment record.");

                // Get the actual document fee
                var documentType = ctx.tbldocumenttypes
                    .FirstOrDefault(d => d.DocTypeID == existingRequest.DocTypeID);

                if (documentType == null)
                    return BadRequest("Document type not found.");

                // Generate PaymentID
                var payments = ctx.tblpayments.ToList();

                int maxNumber = 0;

                foreach (var p in payments)
                {
                    if (!string.IsNullOrEmpty(p.PaymentID) &&
                        p.PaymentID.StartsWith("PAY-"))
                    {
                        int number;

                        if (int.TryParse(p.PaymentID.Substring(4), out number))
                        {
                            if (number > maxNumber)
                                maxNumber = number;
                        }
                    }
                }

                string newPaymentID = "PAY-" + (maxNumber + 1).ToString("000");

                // Generate ORNumber
                int maxOR = 0;

                foreach (var pay in payments)
                {
                    int orNumber;

                    if (!string.IsNullOrEmpty(pay.ORNumber) &&
                        pay.ORNumber.StartsWith("OR-") &&
                        int.TryParse(pay.ORNumber.Substring(3), out orNumber))
                    {
                        if (orNumber > maxOR)
                            maxOR = orNumber;
                    }
                }

                string newORNumber = "OR-" + (maxOR + 1).ToString("D6");

                // Create payment
                var newPayment = new tblpayment()
                {
                    PaymentID = newPaymentID,
                    RequestID = u.RequestID,
                    Amount = documentType.Fee,
                    PaymentMethod = u.PaymentMethod,
                    ORNumber = newORNumber, 
                    PaymentDate = DateTime.Now,
                    Status = "Paid"
                };

                ctx.tblpayments.Add(newPayment);
                ctx.SaveChanges();
            }

            return Ok("Payment successfully created.");
        }

        // Update Method
        public IHttpActionResult PutPayment(PaymentViewModel u)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new db_barangay_systemEntities())
            {
                var existingPayment = ctx.tblpayments
                    .Where(s => s.PaymentID == u.PaymentID)
                    .FirstOrDefault<tblpayment>();

                if (existingPayment != null)
                {
                    existingPayment.PaymentID = u.PaymentID;
                    existingPayment.RequestID = u.RequestID;
                    existingPayment.Amount = u.Amount;
                    existingPayment.PaymentMethod = u.PaymentMethod;
                    existingPayment.ORNumber = u.ORNumber;
                    existingPayment.Status = u.Status;
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
        public IHttpActionResult DeletePayment(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Not a valid id");

            using (var ctx = new db_barangay_systemEntities())
            {
                var u = ctx.tblpayments
                    .Where(s => s.PaymentID == id)
                    .FirstOrDefault();

                if (u == null)
                    return NotFound();

                ctx.tblpayments.Remove(u);
                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
