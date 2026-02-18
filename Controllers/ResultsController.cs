using System.Linq;
using System.Web.Mvc;
using OnlineAptitudeTest.Models;

public class ResultsController : Controller
{
    private WebsterAptitudeDBEntities db = new WebsterAptitudeDBEntities();

    // Manager View: All Clear Candidates
    public ActionResult AptiClearList()
    {
        var clearList = db.Results.Where(r => r.TotalScore >= 50).ToList();
        return View(clearList);
    }

    // PDF/Print Style Report
    public ActionResult Report()
    {
        var allResults = db.Results.OrderByDescending(r => r.TestDate).ToList();
        return View(allResults);
    }
}