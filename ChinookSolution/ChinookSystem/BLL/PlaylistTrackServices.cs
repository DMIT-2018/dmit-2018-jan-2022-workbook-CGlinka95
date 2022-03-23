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
    public class PlaylistTrackServices
    {
        #region Constructor and Context Dependancy
        private readonly ChinookContext _context;

        // Obtain the context link from IServiceCollection when this set of service is injected into the "outside user"
        internal PlaylistTrackServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Queries
        public List<PlaylistTrackInfo> PlaylistTrack_GetUserPlaylistTracks(string playlistname,
                                                                           string username);
        {
            IEnumerable<PlaylistTrackInfo> info = _context.PlaylistTracks
                                                 .Where(x => x.Playlist.Name.Equals(playlistname)
                                                             && x.Playlist.UserName.Equals(username))
                                                 .Select(x => new PlaylistTrackInfo
                                                 {
                                                     TrackId = x.TrackId,
                                                     TrackNumber = x.TrackNumber,
                                                     SongName = x.SongName,
                                                     Milliseconds = x.Milliseconds
                                                 })
                                                .OrderBy(x => x.TrackNumber);
        }
        #endregion

        #region Commands

        #endregion
    }
}
