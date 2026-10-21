using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MP1_MOD1_ITPE3_JEM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "This is my Home Page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AllAboutMeJEM()
        {
            ViewBag.NameJEM = "Name: John Efraim Macalino Mallari";
            ViewBag.BirthdayJEM = "Birthday: October 11, 2005";
            ViewBag.GenderJEM = "Gender: Male";
            ViewBag.HomeJEM = "Home Address: Dila-dila, Santa Rita, Pampanga";
            ViewBag.EmailJEM = "Email Address: johnefraimmallari2817@gmail.com";

            ViewBag.FatherJEM = "Father's Name: Marcelo Macalino Mallari";
            ViewBag.MotherJEM = "Mother's Name: Lucia Macalino Mallari";
            ViewBag.Sibling1JEM = "Sibling 1: Melvin Macalino Mallari";
            ViewBag.Sibling2JEM = "Sibling 2: Michelle Mallari Cuenco";
            ViewBag.Sibling3JEM = "Sibling 3: Mark Owen Macalino Mallari";
            ViewBag.Sibling4JEM = "Sibling 4: Marco Loren Macalino Mallari";
            ViewBag.Sibling5JEM = "Sibling 5: Niño Noel Macalino Mallari";
            ViewBag.Sibling6JEM = "Sibling 6: Emmanuel Macalino Mallari0";

            ViewBag.PrimaryJEM = "Primary School: Wenceslao Village Elementary";
            ViewBag.PDGraduatedJEM = "2018";
            ViewBag.JuniorJEM = "Junior High School: Wenceslao Village High School";
            ViewBag.JHDGraduatedJEM = "2022";
            ViewBag.SeniorJEM = "Senior High School: San Roque Dau High School";
            ViewBag.SHDGraduatedJEM = "2024";
            ViewBag.TertiaryJEM = "Tertiary School: Mary The Queen College";
            ViewBag.CourseJEM = "Bachelor of Science in Information Technology";

            return View();
        }
    }
}
