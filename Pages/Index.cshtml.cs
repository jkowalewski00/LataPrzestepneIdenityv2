using LataPrzestepneIdenity.Data;
using LataPrzestepneIdenity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using LataPrzestepneIdenity.Interfaces;

namespace LataPrzestepneIdenity.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILeapYearInterface _leapYearInterface;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, ILeapYearInterface leapYearInterface, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _leapYearInterface = leapYearInterface;
            _userManager = userManager;
        }

        [BindProperty]
        public LeapYears LeapYears { get; set; }

        [BindProperty(SupportsGet = true)]

        public String? Name { get; set; }

        public String AlertMessage { get; set; }

        public ClaimsPrincipal GetUser()
        {
            return User;
        }

        public IActionResult OnPost(ClaimsPrincipal user)
        {
            if(LeapYears.BirthYear == 0)
            {
                ModelState.Remove("LeapYears.BirthYear");
                ModelState.AddModelError("LeapYears.BirthYear", "Pole Rok urodenia jest wymagane");
            }
            
            LeapYears.CheckYear();
            LeapYears.UserID = _userManager.GetUserId(User);
            LeapYears.UserName = _userManager.GetUserName(User);
            LeapYears.Date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
           _leapYearInterface.addRecord(LeapYears);

            if (LeapYears.LeapYear)
            {
                AlertMessage = $"{LeapYears.FirstName} urodził się w {LeapYears.BirthYear} roku. To był rok przestępny.";

            }
            else
            {
                AlertMessage = $"{LeapYears.FirstName} urodził się w {LeapYears.BirthYear} roku. To nie był rok przestępny.";
            }
            return Page();
        }

        public void OnGet()
        {

            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = "User";
            }

        }
    }
}