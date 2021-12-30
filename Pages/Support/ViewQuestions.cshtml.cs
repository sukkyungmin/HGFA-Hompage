using HangilFA.Areas.Identity.Data;
using HangilFA.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HangilFA.Pages.Support
{
    public class ViewQuestionsModel : PageModel
    {
        private readonly UserManager<HangilFAUser> _userManager;
        private readonly IDataAccessService _dataAccessService;

        public ViewQuestionsModel(UserManager<HangilFAUser> userManager,
            IDataAccessService dataAccessServic)
        {
            _userManager = userManager;
            _dataAccessService = dataAccessServic;
        }

        [BindProperty]
        public QuestionsViewModel questionsviewmodel { get; set; }
        [BindProperty]
        public QuestionsAnswerViewModel questionsanswervwriteviewmodel { get; set; }
        [BindProperty]
        public List<QuestionsAnswerViewModel> questionsanswervreadviewmodel { get; set; }



        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var questionsViewModel = new QuestionsViewModel();
            var questionsanswerreadViewModel = new List<QuestionsAnswerViewModel>();

            questionsViewModel = await _dataAccessService.GetQuestionsAsync(id);
            questionsanswerreadViewModel = await _dataAccessService.GetQuestionsAnswerAsync(id);

            questionsviewmodel = questionsViewModel;
            questionsanswervreadviewmodel = questionsanswerreadViewModel;

            if (_userManager.GetUserName(User) != null)
            {
                questionsanswervwriteviewmodel = new QuestionsAnswerViewModel();

                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));

                questionsanswervwriteviewmodel.AnswerUserId = user?.Id.ToString();
                questionsanswervwriteviewmodel.AnswerUser = user?.UserName;
                questionsanswervwriteviewmodel.AnswerUserFullname = user?.FullName;
                questionsanswervwriteviewmodel.SupporQuestionsId = id;
            }

            return Page();

        }


        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessService.SetQuestionsAnswerAsync(questionsanswervwriteviewmodel);
                await _dataAccessService.SetUpdateQuestionsCountAsync(id,true);
                return RedirectToPage("ViewQuestions", new {id});
            }

            return Page();
        }
    }
}
