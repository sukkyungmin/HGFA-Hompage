using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HangilFA.Pages.Document
{
    public class HgfaDocumentModel : PageModel
    {

        public string Name { get; private set; } = "테스트중";
        public void OnGet()
        {
        }
    }
}
