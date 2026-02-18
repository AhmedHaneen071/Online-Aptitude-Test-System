using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineAptitudeTest.Models; // Ensure this matches your project namespace
using System.Data.Entity;

namespace OnlineAptitudeTest.Controllers
{
    public class ManagerController : Controller
    {
        private WebsterAptitudeDBEntities db = new WebsterAptitudeDBEntities();

        // 1. DASHBOARD: Shows stats and candidate list
        public ActionResult Index()
        {
            if (Session["Role"]?.ToString() != "Manager") return RedirectToAction("Login", "Account");

            var results = db.Results.ToList();
            var candidates = db.Candidates.ToList();

            // Stats calculation for the Cyber-Panel
            ViewBag.TotalTests = results.Count();
            ViewBag.PassCount = results.Count(r => r.TotalScore >= 50);
            ViewBag.FailCount = results.Count(r => r.TotalScore < 50);

            return View(candidates);
        }

        // 2. QUESTION BANK: List all questions
        public ActionResult ManageQuestions()
        {
            if (Session["Role"]?.ToString() != "Manager") return RedirectToAction("Login", "Account");
            return View(db.Questions.ToList());
        }

        // 3. CREATE QUESTION: GET
        public ActionResult CreateQuestion()
        {
            return View(new Question());
        }

        // 4. CREATE QUESTION: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(Question q)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(q);
                db.SaveChanges();
                return RedirectToAction("ManageQuestions");
            }
            return View(q);
        }

        // 5. EDIT QUESTION: GET (Fixes 404 Error)
        public ActionResult Edit(int id)
        {
            var q = db.Questions.Find(id);
            if (q == null) return HttpNotFound();
            return View("CreateQuestion", q); // Uses the same form as Create
        }

        // 6. EDIT QUESTION: POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question q)
        {
            if (ModelState.IsValid)
            {
                db.Entry(q).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageQuestions");
            }
            return View("CreateQuestion", q);
        }

        // 7. DELETE QUESTION: GET (Called from the list)
        public ActionResult Delete(int id)
        {
            var q = db.Questions.Find(id);
            if (q != null)
            {
                db.Questions.Remove(q);
                db.SaveChanges();
            }
            return RedirectToAction("ManageQuestions");
        }

        // 8. VIEW PROGRESS: Show logs for a specific candidate
        public ActionResult ViewProgress(int id)
        {
            var candidate = db.Candidates.Find(id);
            if (candidate == null) return HttpNotFound();

            ViewBag.User = candidate.Username; // For the Cyber-Panel Header

            // Fetch results and ensure list is passed to view
            var userResults = db.Results.Where(r => r.CandidateID == id)
                                        .OrderByDescending(r => r.TestDate)
                                        .ToList();

            return View(userResults);
        }
    }
}