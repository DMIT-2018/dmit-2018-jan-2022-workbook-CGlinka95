#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namepsaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.Pages.SamplePages
{
    public class AlbumsByGenreQueryModel : PageModel
    {
        #region Private variable and DI constructor
        private readonly ILogger<IndexModel> _logger;
        private readonly AlbumServices _albumServices;
        private readonly GenreServices _genreServices;

        public AlbumsByGenreQueryModel(ILogger<IndexModel> logger,
                            AlbumServices albumservices,
                            GenreServices genreservices)
        {
            _logger = logger;
            _albumServices = albumservices;
            _genreServices = genreservices;
        }
        #endregion

        #region Feedback and ErrorHandling
        [TempData]
        public string FeedBack { get; set; }
        public bool HasFeedBack => !string.IsNullOrWhiteSpace(FeedBack);

        [TempData]
        public string ErrorMsg { get; set; }
        public bool HasErrorMsg=> !string.IsNullOrWhiteSpace(ErrorMsg);
        #endregion

        [BindProperty]
        public List<SelectionList> GenreList { get; set; }

        [BindProperty (SupportsGet = true)]
        public int? GenreId { get; set; }

        [BindProperty]
        public List<AlbumsListBy> AlbumsByGenre { get; set; }
        public void onGet()
        {
            // OnGet is executed as the page is first processed (as it comes up)

            // Consume a service: GetAllGenres in registered services of _genreServices
            GenreList = _genreServices.GetAllGenres();
            // Sort the List<T> using the method .Sort
            GenreList.Sort((x,y) => x.DisplayText.CompareTo(y.DisplayText));

            // Remember that this method executes as the page FIRST comes up, BEFORE anything has happened on the page (including the FIRST display)
            // Any code in this method MUST handle the possibility of missing data for the query argument
            if (GenreId.HasValue && GenreId.Value > 0)
            {
                AlbumsByGenre = _albumServices.AlbumsByGenre((int)GenreId);
            }
        }

        public IActionResult OnPost()
		{
            if(GenreId == 0)
			{
                FeedBack = "You did not select a genre.";
			}
            else
			{
                FeedBack = $"You selected genre id of {GenreId}";
			}
            return RedirectToPage(new {GenreId = GenreId}); // Causes a Get request which forces OnGet execution
		}
    }
}