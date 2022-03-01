﻿#nullable disable
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
        public List<AlbumsListBy> AlbumsByGenre(int genreid)
        {
            // Return raw data and let the presentation layer decide ordering 
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
                                              });
            return info.ToList();
        }
        #endregion
    }
}
