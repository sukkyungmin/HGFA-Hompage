using HangilFA.Areas.Identity.Data;
using HangilFA.Data;
using HangilFA.Model;
using HangilFA.Services;
using HangilFA.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class EditNoticesModel : PageModel
    {
        private readonly UserManager<HangilFAUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        public EditNoticesModel(UserManager<HangilFAUser> userManager,
            IDataAccessService dataAccessServic)
        {
            _userManager = userManager;
            _dataAccessService = dataAccessServic;

        }

        [BindProperty]
        public NoticesViewModel noticesViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (_userManager.GetUserName(User) != null)
            {
                if (!await _dataAccessService.GetRoleCheck(_userManager.GetUserId(User), "Notices"))
                    return RedirectToPage("Pageauthority");

                noticesViewModel = new NoticesViewModel();

                var permissions = await _dataAccessService.GetNoticesAsync(id);

                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

                permissions.ModifyUser = user?.UserName;
                permissions.ModifyFullname = user?.FullName;
                permissions.ModifyTime = DateTime.Now;

                noticesViewModel = permissions;

                return Page();
            }

            return RedirectToPage("/Logins/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _dataAccessService.SetUpdateNoticesAsync(noticesViewModel);
                return RedirectToPage("Notices");
            }

            return Page();
        }
    }
}
