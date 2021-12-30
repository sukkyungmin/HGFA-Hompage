using HangilFA.Areas.Identity.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class CreateNoticesModel : PageModel
    {
		private readonly UserManager<HangilFAUser> _userManager;
		private readonly IDataAccessService _dataAccessService;

		public CreateNoticesModel(UserManager<HangilFAUser> userManager,
			IDataAccessService dataAccessServic)
		{
			_userManager = userManager;
			_dataAccessService = dataAccessServic;
		}

        [BindProperty]
        public NoticesViewModel Noticesviewmodel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_userManager.GetUserName(User) != null)
            {
                if (!await _dataAccessService.GetRoleCheck(_userManager.GetUserId(User), "Notices"))
                    return RedirectToPage("Pageauthority");

                Noticesviewmodel = new NoticesViewModel();

                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

                Noticesviewmodel.CreateUser = user?.UserName;
                Noticesviewmodel.CreateFullname = user?.FullName;

                return Page();
            }

            return RedirectToPage("/Logins/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _dataAccessService.SetNoticesAsync(Noticesviewmodel);
                return RedirectToPage("Notices");
            }

            return Page();
        }

    }
}
