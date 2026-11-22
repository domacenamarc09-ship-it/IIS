using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class InstructorController : Controller
{
    private readonly ApplicationDbContext _context;

    public InstructorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // runs before every action: only logged-in users may continue
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
            context.Result = RedirectToAction("Login", "Account");
        base.OnActionExecuting(context);
    }

    // ---------- READ (list) ----------
    public IActionResult Index()
        => View(_context.Instructors.OrderBy(i => i.FullName).ToList());

    // ---------- READ (one record) ----------
    public IActionResult Details(int id)
    {
        var instructor = _context.Instructors.Find(id);     // find by primary key
        if (instructor == null) return NotFound();
        return View(instructor);
    }

    // ---------- CREATE ----------
    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Instructor model)
    {
        if (_context.Instructors.Any(i => i.EmailAddress == model.EmailAddress))
            ModelState.AddModelError("EmailAddress",
                "This e-mail address is already used by another instructor.");

        if (!ModelState.IsValid) return View(model);

        _context.Instructors.Add(model);
        _context.SaveChanges();                              // INSERT
        TempData["Msg"] = "Instructor added successfully.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- UPDATE ----------
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var instructor = _context.Instructors.Find(id);
        if (instructor == null) return NotFound();
        return View(instructor);                   // fills the form with current data
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Instructor model)
    {
        var existing = _context.Instructors.Find(model.Id);
        if (existing == null) return NotFound();

        if (_context.Instructors.Any(i => i.Id != model.Id &&
                                          i.EmailAddress == model.EmailAddress))
            ModelState.AddModelError("EmailAddress",
                "This e-mail address is already used by another instructor.");

        if (!ModelState.IsValid) return View(model);

        existing.FullName = model.FullName;
        existing.Age = model.Age;
        existing.Address = model.Address;
        existing.Designation = model.Designation;
        existing.Gender = model.Gender;
        existing.ContactNumber = model.ContactNumber;
        existing.Majorship = model.Majorship;
        existing.BachelorDegree = model.BachelorDegree;
        existing.EmailAddress = model.EmailAddress;
        _context.SaveChanges();                              // UPDATE
        TempData["Msg"] = "Instructor updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    // ---------- DELETE ----------
    [HttpGet]
    public IActionResult Delete(int id)             // confirmation page
    {
        var instructor = _context.Instructors.Find(id);
        if (instructor == null) return NotFound();
        return View(instructor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)    // actual deletion
    {
        var instructor = _context.Instructors.Find(id);
        if (instructor != null)
        {
            _context.Instructors.Remove(instructor);
            _context.SaveChanges();                          // DELETE
        }
        TempData["Msg"] = "Instructor deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
