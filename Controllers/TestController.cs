using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineAptitudeTest.Models; // Ensure namespace matches your project

namespace OnlineAptitudeTest.Controllers
{
    public class TestController : Controller
    {
        private WebsterAptitudeDBEntities db = new WebsterAptitudeDBEntities();

        // 1. USER DASHBOARD
        public ActionResult Dashboard()
        {
            if (Session["CandidateID"] == null) return RedirectToAction("Login", "Account");
            return View();
        }

        // 2. CANDIDATE PROFILE (Fixes 404 Error)
        public ActionResult CandidateProfile()
        {
            if (Session["CandidateID"] == null) return RedirectToAction("Login", "Account");

            int cid = Convert.ToInt32(Session["CandidateID"]);
            var user = db.Candidates.Find(cid);

            if (user == null) return HttpNotFound();
            return View(user);
        }

        // 3. INSTRUCTIONS PAGE
        public ActionResult Instructions()
        {
            if (Session["CandidateID"] == null) return RedirectToAction("Login", "Account");
            return View();
        }

        // 4. MY RECORDS (History)
        public ActionResult MyRecords()
        {
            if (Session["CandidateID"] == null) return RedirectToAction("Login", "Account");

            int cid = Convert.ToInt32(Session["CandidateID"]);
            var history = db.Results.Where(r => r.CandidateID == cid)
                                   .OrderByDescending(r => r.TestDate)
                                   .ToList();
            return View(history);
        }

        // 5. TAKE TEST (Fetches Questions)
        public ActionResult TakeTest()
        {
            if (Session["CandidateID"] == null) return RedirectToAction("Login", "Account");

            var questions = db.Questions.ToList();
            return View(questions);
        }

        // 6. SUBMIT TEST (Logic to calculate score)
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SubmitTest(FormCollection form)
        {
            if (Session["CandidateID"] == null) return RedirectToAction("Login", "Account");

            int score = 0;
            var questions = db.Questions.ToList();

            foreach (var q in questions)
            {
                string submittedAnswer = form["q_" + q.QuestionID];

                if (!string.IsNullOrEmpty(submittedAnswer))
                {
                    // Trim use karein taake extra spaces match fail na karein
                    if (submittedAnswer.Trim().Equals(q.CorrectAnswer.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        score += 10;
                    }
                }
            }

            Result res = new Result
            {
                CandidateID = Convert.ToInt32(Session["CandidateID"]),
                TotalScore = score,
                TestDate = DateTime.Now
            };

            db.Results.Add(res);
            db.SaveChanges();

            return RedirectToAction("FinalResult", new { id = res.ResultID });
        }

        // 7. FINAL RESULT VIEW
        public ActionResult FinalResult(int id)
        {
            if (Session["CandidateID"] == null) return RedirectToAction("Login", "Account");

            var result = db.Results.Find(id);
            if (result == null) return HttpNotFound();

            return View(result);
        }
    }
}