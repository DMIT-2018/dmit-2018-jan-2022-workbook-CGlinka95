#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using ChinookSystem.ViewModels;
#endregion

namespace ChinookSystem.BLL
{
    public class AboutServices
    {
        // This class needs to be accessed by an "outside user" (WebApp)
        // Therefore, the class needs to be public

        #region Constructor and Context Dependancy
        private readonly ChinookContext _context;

        // Obtain the context link from IServiceCollection when this set of service is injected into the "outside user"
        internal AboutServices(ChinookContext context)
        {
            _context = context;
        }
        #endregion

        #region Services
        // Services are methods

        // Query to obtain the DbVersion data
        public DbVersionsInfo GetDbVersion()
        {
            // DbVersionInfo is a public "view" of data defined in a class
            // DbVersionInfo can be a class used BOTH internally and by external users
            // DbVersion is an internal entity description used ONLY in the library
            DbVersionsInfo info = _context.DbVersions
                                     .Select(x => new DbVersionsInfo {
                                        Major = x.Major,
                                        Minor = x.Minor,
                                        Build = x.Build,
                                        ReleaseDate = x.ReleaseDate
                                     })
                                     .SingleOrDefault();
            return info;
        }
        #endregion
    }
}
