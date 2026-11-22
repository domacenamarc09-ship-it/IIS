using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<UserAccount> _hasher = new();

    public AccountController(ApplicationDbContext context)   // injected by ASP.NET Core
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        // custom rules: no duplicate ID or email in the database
        if (_context.UserAccounts.Any(u => u.InstructorId == model.InstructorId))
            ModelState.AddModelError("InstructorId", "This Instructor ID is already registered.");

        if (_context.UserAccounts.Any(u => u.Email == model.Email))
            ModelState.AddModelError("Email", "This email address is already registered.");

        if (!ModelState.IsValid) return View(model);   // show messages again

        var user = new UserAccount
        {
            InstructorId = model.InstructorId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };
        user.PasswordHash = _hasher.HashPassword(user, model.Password);

        _context.UserAccounts.Add(user);
        _context.SaveChanges();                        // INSERT into the database

        TempData["Msg"] = "Registration successful! Please log in.";
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = _context.UserAccounts.FirstOrDefault(u =>
            u.Email == model.EmailOrId || u.InstructorId == model.EmailOrId);

        if (user == null || _hasher.VerifyHashedPassword(user, user.PasswordHash,
                model.Password) == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(string.Empty,
                "Invalid Email/Instructor ID or password. Please try again.");
            return View(model);
        }

        HttpContext.Session.SetInt32("UserId", user.Id);               // remember the login
        HttpContext.Session.SetString("FullName", user.FirstName + " " + user.LastName);

        TempData["Msg"] = "Welcome, " + user.FirstName + "! You are now logged in.";
        return RedirectToAction("Dashboard");
    }

    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetInt32("UserId") == null)   // not logged in
            return RedirectToAction("Login");

        ViewBag.FullName = HttpContext.Session.GetString("FullName");
        ViewBag.TotalInstructors = _context.Instructors.Count();
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        TempData["Msg"] = "You have been logged out.";
        return RedirectToAction("Login");
    }
}
