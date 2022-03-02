#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Additional Namepsaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
using WebApp.Helpers;
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

        #region Paginator variables
        // My desired page size
        private const int PAGE_SIZE = 5;
        // Instance for the Paginator class
        public Paginator Pager { get; set; } 
        #endregion

        // CurrentPage value will appear on your url as a Request parameter value
        //      url address...?currentPage=n
        public void OnGet(int? currentPage)
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
                // Installation of the Paginator setup
                //      1) Determine the page number to use with the Paginator
                int pageNumber = currentPage.HasValue ? currentPage.Value : 1;
                //      2) Use the page state to setup data needed for paging 
                PageState current = new PageState(pageNumber, PAGE_SIZE);
                //      3) Total rows in the complete query collection (data needed for paging)
                int totalrows = 0;

                // For efficiency of data being transferred, we will pass the current page number and the desired page size to the backend query
                //      the returned collection with have ONLY the rows of the whole query collection that will actually be shown (PAGE_SIZE or less rows)
                //      the total number of records for the whole query collection will be returned as an out parameter. This value is needed by the Paginator to setup it's display logic
                AlbumsByGenre = _albumServices.AlbumsByGenre((int)GenreId,
                                    pageNumber, PAGE_SIZE, out totalrows);   

                //      4) Once the query is complete, use the returned total rows in instantiating an instance of the Paginator
                Pager = new Paginator(totalrows, current);
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