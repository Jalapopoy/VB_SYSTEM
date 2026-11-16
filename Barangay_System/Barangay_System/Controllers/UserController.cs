using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Barangay_System.Models;

namespace Barangay_System.Controllers
{
    public class UserController : ApiController
    {
        // GET: /User/

        public IHttpActionResult GetAllUsers()
        {
            IList<UserViewModel> users = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                users = ctx.tblusers.Select(u => new UserViewModel()
                {
                    UserID = u.UserID,
                    Username = u.Username,
                    Role = u.Role,
                    ResidentID = u.ResidentID,
                    Status = u.Status,
                    created_at = u.created_at
                }).ToList<UserViewModel>();
            }

            if (users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // Read One Method
        public IHttpActionResult GetUserById(string id)
        {
            UserViewModel u = null;

            using (var ctx = new db_barangay_systemEntities())
            {
                u = ctx.tblusers.Where(s => s.UserID == id)
                    .Select(s => new UserViewModel()
                    {
                        UserID = s.UserID,
                        Username = s.Username,
                        Role = s.Role,
                        ResidentID = s.ResidentID,
                        Status = s.Status,
                        created_at = s.created_at
                    }).FirstOrDefault<UserViewModel>();
            }

            return Ok(u);
        }

        // Create User Method
        public IHttpActionResult PostNewUser(UserViewModel u)
        {
            if (u == null)
                return BadRequest("Invalid user data.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new db_barangay_systemEntities())
            {
                if (ctx.tblusers.Any(x => x.Username == u.Username))
                    return BadRequest("Username already taken.");

                var existingIDs = ctx.tblusers.Select(x => x.UserID).ToList();
                int maxNumber = 0;

                foreach (var existingID in existingIDs)
                {
                    int number;

                    if (!String.IsNullOrEmpty(existingID) &&
                        existingID.StartsWith("USR-") &&
                        Int32.TryParse(existingID.Substring(4), out number))
                    {
                        if (number > maxNumber)
                            maxNumber = number;
                    }
                }

                u.UserID = "USR-" + (maxNumber + 1).ToString("D3");

                var newUser = new tbluser()
                {
                    UserID = u.UserID,
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role,
                    ResidentID = u.ResidentID,
                    Status = u.Status,
                    created_at = DateTime.Now
                };

                ctx.tblusers.Add(newUser);
                ctx.SaveChanges();
            }

            return Ok("User successfully created.");
        }

        // Update Method
        public IHttpActionResult PutUser(UserViewModel u)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new db_barangay_systemEntities())
            {
                var existingUser = ctx.tblusers
                    .Where(s => s.UserID == u.UserID)
                    .FirstOrDefault<tbluser>();

                if (existingUser != null)
                {
                    existingUser.Username = u.Username;
                    existingUser.Role = u.Role;
                    existingUser.ResidentID = u.ResidentID;
                    existingUser.Status = u.Status;

                    if (!string.IsNullOrWhiteSpace(u.Password))
                        existingUser.Password = u.Password;

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
        public IHttpActionResult DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("Not a valid id");

            using (var ctx = new db_barangay_systemEntities())
            {
                var u = ctx.tblusers
                    .Where(s => s.UserID == id)
                    .FirstOrDefault();

                if (u == null)
                    return NotFound();

                ctx.tblusers.Remove(u);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
