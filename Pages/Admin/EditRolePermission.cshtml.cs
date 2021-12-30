using HangilFA.Areas.Identity.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class EditRolePermissionModel : PageModel
    {

        private readonly IDataAccessService _dataAccessService;
        private readonly UserManager<HangilFAUser> _userManager;

        public EditRolePermissionModel(IDataAccessService dataAccessService,
                        UserManager<HangilFAUser> userManager)
        {

            _dataAccessService = dataAccessService;
            _userManager = userManager; 
        }

        [BindProperty]
        public List<NavigationMenuViewModel> navigationmenuviewmodel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
		{
            if (_userManager.GetUserName(User) == null || _userManager.GetUserName(User) != "Admin")
                return RedirectToPage("Pageauthority");

            var permissions = new List<NavigationMenuViewModel>();
            if (!string.IsNullOrWhiteSpace(id))
            {
                permissions = await _dataAccessService.GetPermissionsByRoleIdAsync(id,2);
            }

            navigationmenuviewmodel = permissions;

            return Page();
        }

		public async Task<IActionResult> OnPostAsync(string id)
		{
            if (ModelState.IsValid)
            {
                var permissionIds = navigationmenuviewmodel.Where(x => x.Permitted).Select(x => x.Id);

                await _dataAccessService.SetPermissionsByRoleIdAsync(id, permissionIds);
                return RedirectToPage("Roles");
            }

            return Page();
        }

	}
}
