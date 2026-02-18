using System.Linq;
using System.Web.Mvc;
using OnlineAptitudeTest.Models;

public class AccountController : Controller
{
    private WebsterAptitudeDBEntities db = new WebsterAptitudeDBEntities();

    public ActionResult Login() => View();

    [HttpPost]
    public ActionResult Login(string username, string password)
    {
        if (username == "admin" && password == "admin123")
        {
            Session["Role"] = "Manager"; Session["Username"] = "ROOT_ADMIN";
            return RedirectToAction("Index", "Manager");
        }
        var user = db.Candidates.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            Session["CandidateID"] = user.CandidateID;
            Session["Username"] = user.Username;
            return RedirectToAction("Dashboard", "Test");
        }
        ViewBag.Error = "ACCESS_DENIED: INVALID_CREDENTIALS";
        return View();
    }

    public ActionResult Register() => View();

    [HttpPost]
    public ActionResult Register(Candidate c)
    {
        if (ModelState.IsValid)
        {
            db.Candidates.Add(c); db.SaveChanges();
            return RedirectToAction("Login");
        }
        return View(c);
    }

    public ActionResult Logout() { Session.Clear(); return RedirectToAction("Login"); }
}