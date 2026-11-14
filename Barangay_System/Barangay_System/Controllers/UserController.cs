
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
                    Password = u.Password,
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
                        Password = s.Password,
                        Role = s.Role,
                        ResidentID = s.ResidentID,
                        Status = s.Status,
                        created_at = s.created_at
                    }).FirstOrDefault<UserViewModel>();
            }

            if (u == null)
            {
                return NotFound();
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
                    existingUser.UserID = u.UserID;
                    existingUser.Username = u.Username;
                    existingUser.Password = u.Password;
                    existingUser.Role = u.Role;
                    existingUser.ResidentID = u.ResidentID;
                    existingUser.Status = u.Status;
                    existingUser.created_at = DateTime.Now;

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
