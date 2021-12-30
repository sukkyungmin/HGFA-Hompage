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
    public class DeleteQuestionsModel : PageModel
    {
        private readonly UserManager<HangilFAUser> _userManager;
        private readonly IDataAccessService _dataAccessService;
        private readonly HangilFADBContext _context;

        public DeleteQuestionsModel(UserManager<HangilFAUser> userManager,
            IDataAccessService dataAccessServic,
            HangilFADBContext context)
        {
            _userManager = userManager;
            _dataAccessService = dataAccessServic;
            _context = context;

        }

        [BindProperty]
        public QuestionsViewModel Questionsviewmodel { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (_userManager.GetUserName(User) != null)
            {
                var user = await _context.SupporQuestions.FindAsync(id);

                if (user.CreateUserId == _userManager.GetUserId(User) || _userManager.GetUserName(User) == "Admin")
                {

                    Questionsviewmodel = new QuestionsViewModel();

                    var permissions = await _dataAccessService.GetQuestionsAsync(id);

                    Questionsviewmodel = permissions;

                    return Page();
                }
                    return RedirectToPage("/Admin/Pageauthority");

            }

            return RedirectToPage("/Logins/Login");
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var viewmodel = await _context.SupporQuestions.FindAsync(id);

                if (viewmodel == null)
                {
                    return NotFound();
                }

                try
                {
                    _context.SupporQuestions.Remove(viewmodel);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("/Support/Questions");
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
