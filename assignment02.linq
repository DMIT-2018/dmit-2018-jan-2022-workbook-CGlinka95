<Query Kind="Program">
  <Connection>
    <ID>8cc1b452-ffbd-4f48-bcdf-7c438b0d7cfb</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

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
	//  collection : Albums from 60s
	//  selective dataset : anonymous dataset
	//	ReleaseYear
	//	Title
	//	Artist.Name
	//	nested query : Tracks
	//		aggragate operators : .Max(Milliseconds) - longest playing track
	//							  .Min(Milliseconds) - shortest playing track
	//							  .Sum(UnitPrice) - total price of all tracks
	//							  .Average(Milliseconds) - average playing length of tracks
	
	var results = Albums 
					.Where(a => a.ReleaseYear > 1959 && a.ReleaseYear < 1970)
					.Select( a => new AlbumsItem {
						Year = a.ReleaseYear,
						Title = a.Title,
						Artist = a.Artist.Name,
						TracksList = a.Tracks
										.Select(t => new TracksItem {
											LongestPlaying = Tracks.Max(t => t.Milliseconds),
											ShortestPlaying = Tracks.Min(t => t.Milliseconds),
											TotalPrice = Tracks.Sum(t => t.UnitPrice),
											AveragePlaying = Tracks.Average(t => t.Milliseconds)
										})
					});
	results.Dump();
}

public class AlbumsItem 
{
	public int Year {get;set;}
	public string Title {get;set;}
	public string Artist {get;set;}
	public IEnumerable<TracksItem> TracksList {get;set;}
}

public class TracksItem
{
	public int LongestPlaying {get;set;}
	public int ShortestPlaying {get;set;}
	public decimal TotalPrice {get;set;}
	public double AveragePlaying {get;set;}
}
