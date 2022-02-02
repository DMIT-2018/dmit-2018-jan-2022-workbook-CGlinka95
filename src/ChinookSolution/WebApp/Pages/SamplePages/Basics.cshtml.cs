using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SamplePages
{
    public class BasicsModel : PageModel
    {
        //Basically this is an objec, treat it as such...
        //  > data fields
        public string MyName;
        //  > properties
        //  > constructors
        //  > behaviors (aka methods)
        public void OnGet()
        {
            //executes in response to a Get Request from the browser
            //when the page is "first" accessed, the browser issues a Get request
            //when the page is refreshed, WITHOUT a Post reques, the browser issues a Get request
            //when the page is retrieved in response to a form's POST using RedirectToPage()
            //IF NO RedirectToPage() is used on the POST, there is NO Get request issued

            //Server-side processing:
            //  contains no html

            Random rng = new Random();
            int oddeven = rng.Next(0, 25);
            if(oddeven % 2 == 0)
			{
                MyName = $"Chris is even {oddeven}";
			}
            else
			{
                MyName = null;
			}
        }
    }
}
