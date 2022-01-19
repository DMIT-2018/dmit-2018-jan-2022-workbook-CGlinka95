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
		Artist = x.Artist.Name,
		Year = x.ReleaseYear,
		Decade = x.ReleaseYear < 1970 ? "Oldies" :
				 	x.ReleaseYear < 1980 ? "70s" :
					x.ReleaseYear < 1990 ? "80s" :
					x.ReleaseYear < 2000 ? "90s" : "Modern"
	})
	.OrderBy(x => x.Year) 
