using HangilFA.Areas.Identity.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class CreateRoleModel : PageModel
    {
        private readonly UserManager<HangilFAUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<CreateRoleModel> _logger;
        private readonly IDataAccessService _dataAccessService;

        public CreateRoleModel(RoleManager<IdentityRole> roleManager,
            ILogger<CreateRoleModel> logger,
            UserManager<HangilFAUser> userManager,
            IDataAccessService dataAccessServic)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _dataAccessService = dataAccessServic;
        }

        [BindProperty]
        public RoleViewModel roleviewmodel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_userManager.GetUserName(User) == null)
                return RedirectToPage("/Logins/Login");
            if (!await _dataAccessService.GetRoleCheck(_userManager.GetUserId(User), "Create Role"))
                return RedirectToPage("Pageauthority");

            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole() { Name = roleviewmodel.Name });
                if (result.Succeeded)
                {
                    return RedirectToPage("Roles");
                }
                else
                {
                    ModelState.AddModelError("Name", string.Join(",", result.Errors));
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}
