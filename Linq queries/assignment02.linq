void Main()
{
	//****HOMEWORK****
	//List all albums of the 60s showing the album title, artist, and various aggregates for albums containing tracks:
	//	for each album show the number of tracks, the longest playing track, the shortest playing track, the total price of all tracks, and
	//	the average playing length of the album tracks
	//Hint: Albums has two navigation properties
	//		Artist points to the single parent record
	//		Tracks points to the collection of child records (Tracks) of that album

	//Thought process:
	//  using aggregates : I need collections for the aggregates
	//	aggregates are against Tracks (collection for each album)
	//	for each album report Title, Artist.Name
	
	//		Which table to start with?
	//			Albums
	//			filter Albums for the 60s and Albums with > 0 tracks
	//			display Title and Artist.Name
	//				how many Tracks on the Album? : .Count()
	//				longest playing track 		  : .Max()
	//				shortest playing track 		  : .Min()
	//				total cost of all tracks 	  : .Sum()
	//				average track length 		  : .Average()
	
	var results = Albums 
					.Where(a => a.ReleaseYear > 1959 && a.ReleaseYear < 1970
													 && a.Tracks.Count() > 0)
					.Select( a => new AlbumsItem {
						Year = a.ReleaseYear,
						Title = a.Title,
						Artist = a.Artist.Name,
						CountOfTracks      = a.Tracks.Count(),
						LongestTrack       = a.Tracks.Max(tr => tr.Milliseconds),
						ShortestTrack      = a.Tracks.Min(tr => tr.Milliseconds),
						TotalCost          = a.Tracks.Sum(tr => tr.UnitPrice),
						AverageTrackLength = a.Tracks.Average(tr => tr.Milliseconds)
					});
	results.Dump();
}

public class AlbumsItem 
{
	public int Year {get;set;}
	public string Title {get;set;}
	public string Artist {get;set;}
	public int CountOfTracks {get;set;}
	public int LongestTrack {get;set;}
	public int ShortestTrack {get;set;}
	public decimal TotalCost {get;set;}
	public double AverageTrackLength {get;set;}
}
