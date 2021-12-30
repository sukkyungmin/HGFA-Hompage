using HangilFA.Areas.Identity.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HangilFA.Pages.Support
{
    public class CreateQuestionsModel : PageModel
    {
		private readonly UserManager<HangilFAUser> _userManager;
		private readonly IDataAccessService _dataAccessService;

		public CreateQuestionsModel(UserManager<HangilFAUser> userManager,
			IDataAccessService dataAccessServic)
		{
			_userManager = userManager;
			_dataAccessService = dataAccessServic;
		}

		[BindProperty]
		public QuestionsViewModel Questionsviewmodel  { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_userManager.GetUserName(User) != null)
            {
                Questionsviewmodel = new QuestionsViewModel();

                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

                Questionsviewmodel.CreateUserId = user?.Id;
                Questionsviewmodel.CreateUser = user?.UserName;
                Questionsviewmodel.CreateFullname = user?.FullName;

                return Page();
            }

            return RedirectToPage("/Logins/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
               
                var counts = await _dataAccessService.GetQuestionsCountAsync(Questionsviewmodel.CreateUserId);

                if(counts >= 3)
                    return RedirectToPage("QuestionsCountOver");

                await _dataAccessService.SetQuestionsAsync(Questionsviewmodel);
                return RedirectToPage("Questions");
            }

            return Page();
        }
    }
}
