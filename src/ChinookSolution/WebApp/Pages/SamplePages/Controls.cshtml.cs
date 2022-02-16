using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SamplePages
{
    public class ControlsModel : PageModel
    {
        [TempData]
        public string Feedback { get; set; }

        [BindProperty]
        public string EmailText { get; set; }

        [BindProperty]
        public string PasswordText { get; set; }

        [BindProperty]
        public DateTime DateText { get; set; } = DateTime.Today;

        [BindProperty]
        public TimeSpan TimeText { get; set; } = DateTime.Now.TimeOfDay;

        public string Meal { get; set; }

        //assume that this array is actually data retrieved from the database
        public string[] Meals { get; set; } = new[] { "breakfast", "lunch", "dinner", "snacks" };

        public bool AcceptanceBox { get; set; }

        public string MessageBody { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostTextBox()
        {
            Feedback = $"Email {EmailText}; Password {PasswordText}; Date {DateText}; Time {TimeText};";
            return Page();
        }

        public IActionResult OnPostRadioCheckArea()
        {
            Feedback = $"Meal {Meal}; Acceptance {AcceptanceBox}; Message {MessageBody};";
            return Page();
        }
    }
}
