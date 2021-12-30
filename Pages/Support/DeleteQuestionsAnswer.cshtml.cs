using HangilFA.Areas.Identity.Data;
using HangilFA.Data;
using HangilFA.Model;
using HangilFA.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HangilFA.Pages.Support
{
    public class DeleteQuestionsAnswerModel : PageModel
    {
        private readonly UserManager<HangilFAUser> _userManager;
        private readonly IDataAccessService _dataAccessService;
        private readonly HangilFADBContext _context;

        public DeleteQuestionsAnswerModel(UserManager<HangilFAUser> userManager,
            IDataAccessService dataAccessServic,
            HangilFADBContext context)
        {
            _userManager = userManager;
            _dataAccessService = dataAccessServic;
            _context = context;

        }

        [BindProperty]
        public QuestionsAnswerViewModel questionsanswervwriteviewmodel { get; set; }

        public async Task<IActionResult> OnGetAsync(Int64 id)
        {
            if (_userManager.GetUserName(User) != null)
            {
                var user = await _context.SupporQuestionsAnswer.FindAsync(id);

                if (user.AnswerUserId == _userManager.GetUserId(User) || _userManager.GetUserName(User) == "Admin")
                {

                    questionsanswervwriteviewmodel = new QuestionsAnswerViewModel
                    {
                        Id = user.Id,
                        Content = user.Content,
                        AnswerUser = user.AnswerUser,
                        SupporQuestionsId = user.SupporQuestionsId
                    };

                    return Page();
                }
                return RedirectToPage("/Admin/Pageauthority");

            }

            return RedirectToPage("/Logins/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (questionsanswervwriteviewmodel.Id < 1)
                {
                    return NotFound();
                }

                var viewmodel = await _context.SupporQuestionsAnswer.FindAsync(questionsanswervwriteviewmodel.Id);

                if (viewmodel == null)
                {
                    return NotFound();
                }

                try
                {
                    Guid id = questionsanswervwriteviewmodel.SupporQuestionsId;
                    _context.SupporQuestionsAnswer.Remove(viewmodel);
                    await _context.SaveChangesAsync();
                    await _dataAccessService.SetUpdateQuestionsCountAsync(id,false);
                    return RedirectToPage("ViewQuestions", new { id });
                }
                catch (DbUpdateException)
                {
                    //return RedirectToAction("./Delete",new { id,savechangeserror = })
                }
                //_context.ContextId(id).r
                //await _context.SaveChangesAsync();
            }

            return Page();
        }

    }
}
