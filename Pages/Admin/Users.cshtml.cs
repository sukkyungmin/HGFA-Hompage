using HangilFA.Areas.Identity.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangilFA.Pages.Admin
{
    public class UsersModel : PageModel
    {
		private readonly UserManager<HangilFAUser> _userManager;
		private readonly ILogger<CreateRoleModel> _logger;
		private readonly IDataAccessService _dataAccessService;

		public UsersModel(ILogger<CreateRoleModel> logger,
			UserManager<HangilFAUser> userManager,
			IDataAccessService dataAccessServic)
		{
			_userManager = userManager;
			_logger = logger;
			_dataAccessService = dataAccessServic;
		}

		[BindProperty]
        public IEnumerable<UserViewModel> userviewmodel { get; set; }

		public async Task<IActionResult> OnGetAsync()
        {
            if (_userManager.GetUserName(User) != null)
            {
                if (!await _dataAccessService.GetRoleCheck(_userManager.GetUserId(User), "Users"))
                    return RedirectToPage("Pageauthority");

                var userViewModel = new List<UserViewModel>();

                try
                {
                    var users = await _userManager.Users.ToListAsync();
                    foreach (var item in users)
                    {
                        userViewModel.Add(new UserViewModel()
                        {
                            Id = item.Id,
                            Email = item.Email,
                            UserName = item.UserName,
                            FullName = item.FullName,
                            CreateTime = item.CreateTime
                        });
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, ex.GetBaseException().Message);
                }

                userviewmodel = userViewModel.OrderByDescending(a => a.CreateTime);

                return Page();
            }

            return RedirectToPage("/Logins/Login");
        }


    }
}
