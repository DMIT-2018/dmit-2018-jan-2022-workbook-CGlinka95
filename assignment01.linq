<Query Kind="Expression" />

//****HOMEWORK****
//List all albums showing the Title, Artist name, Year, and decade of releases (oldies, 70s, 80s, 90s, or modern)
//	Order by decade.

//Pseudo Code Process:
//	collection: Albums
//	selective data set: anonymous data set
//	ordering: ReleaseYear
//	label: Decade

//Albums
//	.OrderBy : ReleaseYear
//	.Select(new{})
//	??? assigning the year (condition ? year : decade)
//	nav property Artist.Name

Albums
	.Select(x => new
	{
		Title = x.Title,
		Decade = x.ReleaseYear <= 1969 ? "Oldies" && 
				 x.ReleaseYear >= 1970 ? "70s" &&
				 x.ReleaseYear >= 1980 ? "80s" &&
				 x.ReleaseYear >= 1990 ? "90s" &&
				 x.ReleaseYear >= 2000 ? "Modern" : x.ReleaseYear,
		Artist = x.Artist.Name
	})
	.OrderByDescending(x => x.Decade)
