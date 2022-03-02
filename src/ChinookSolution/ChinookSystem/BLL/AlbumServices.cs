#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
#endregion

namespace ChinookSystem.BLL
{
    public class AlbumServices
    {
        #region Constructor and Context Dependancy
        private readonly ChinookContext _context;

        // Obtain the context link from IServiceCollection when this set of service is injected into the "outside user"
        internal AlbumServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Services : Queries
        public List<AlbumsListBy> AlbumsByGenre(int genreid,
                                                int pageNumber,
                                                int pageSize,
                                                out int totalrows)
        {
            // Return raw data and let the presentation layer decide ordering
            //      EXCEPT when you are also implementing paging then the ordering must be done after.

            // Paging:
            //  - pageNumber(input), pageSize(input) and totalrows(output) are used in implementing the Paginator process
            //  - The Paginator for this application determines the lines to return to the PageModel for processing
            //  - ***THE QUERY ITSELF DOES NOT CHANGE***
            IEnumerable<AlbumsListBy> info = _context.Tracks
                                              .Where(x => x.GenreId == genreid
                                                       && x.AlbumId.HasValue)
                                              .Select(x => new AlbumsListBy
                                              {
                                                  AlbumId = (int)x.AlbumId, // (int) = typecasting
                                                  Title = x.Album.Title,
                                                  ArtistId = x.Album.ArtistId,
                                                  ReleaseYear = x.Album.ReleaseYear,
                                                  ReleaseLabel = x.Album.ReleaseLabel,
                                                  ArtistName = x.Album.Artist.Name
                                              })
                                              .Distinct()
                                              // Paginator ordering:
                                              .OrderBy(x => x.Title);
            // Obtain the number of total rows for the whole collection 
            totalrows = info.Count();

            // Calculate the number of rows to SKIP in the query collection
            //      the number of rows to skip is dependant on the page number and page size
            // Page 1: skip 0 rows;
            // Page 2: skip page size rows;
            // Page n: skip page size - 1 rows
            int skipRows = (pageNumber - 1) * pageSize;

            // Use the Linq extensions .Skip() and .Take() to obtain the desired rows from the whole query collection; return these rows
            return info.Skip(skipRows).Take(pageSize).ToList();
        }
        #endregion
    }
}
