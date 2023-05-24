using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LataPrzestepneIdenity.Data;
using Microsoft.Extensions.Configuration;
using LataPrzestepneIdenity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using LataPrzestepneIdenity.Interfaces;

namespace LataPrzestepneIdenity.Pages
{
    [Authorize]
    public class HistoryModel : PageModel
    {
        private readonly ILeapYearInterface _leapYearInterface;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration Configuration;

        public HistoryModel(ILeapYearInterface leapYearInterface, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _leapYearInterface = leapYearInterface;
            _userManager = userManager;
            Configuration = configuration;
        }

        [BindProperty]
        
        public PaginatedList<LeapYears> AddedData { get; set; }

        public string CurrentUser ;

        public async Task OnGetAsync(int? pageIndex)
        {
            CurrentUser = _userManager.GetUserId(User);
            pageIndex = pageIndex ?? 1;
            IQueryable<LeapYears> leapYears = _leapYearInterface.getYears();
            var pageSize = Configuration.GetValue("PageSize", 4);
            AddedData = await PaginatedList<LeapYears>.CreateAsync(leapYears.AsNoTracking(), pageIndex.Value, pageSize);
        }

        public IActionResult OnPost()
        {
            int id = Convert.ToInt32(Request.Form["id"]);

            LeapYears? data = _leapYearInterface.checkOwner(id);

           if(data != null)
            {
                _leapYearInterface.deleteRecord(data);
                return RedirectToAction("/History");
            }

           return Page();
        }
    }
}
