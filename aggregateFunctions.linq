<Query Kind="Expression">
  <Connection>
    <ID>a006419e-f549-45ef-9a94-058b66849f19</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//USE OF AGGREGATES IN QUERIES 

/*list all albumns titles, alphabetically, 
	the number of tracks for the albumn
	the total track price for the album
	the average Avg() length of a track for the album in seconds 

	
	// SQL Expression  solution (using C# Expression)
	//Note: Sum() totals a specific field/expression, thus you will likely need to use a delegate to indicate the collection instance attribute to be used. 
	
				//FIND THE PRICE OF EVERYTHING THAT YOU BUY: x.DetailsTable.Sum(y => y.UnitPrice * y.Quantity)
	
	//	    Count() count the number of instancs of the collection reference 
	//		Average() averages a specific field/expression, thus you will ikely need to use a delegate to indicate the collection instance attribute to be used.
from x in Albums
orderby x.Title
where x.Tracks.Count() > 0
select new{
	Title = x.Title,
	NumberOfAlbumTracks = x.Tracks.Count(),
	//You cannot just Sum() the tracks because there could be an situation where there are no TRACKS for an ALBUM
	//	YOU NEED TO WRITE THE WHERE STATEMENT TO CHECK THAT YOU ARE SUMMING A TABLE THAT HAS A COUNT GREATER THAN 0
	TotalPrice = x.Tracks.Sum(y => y.UnitPrice),
	AvgTrackLengthInSeconds = (x.Tracks.Average(y => y.Milliseconds))/1000, //does the overall average and then divides the average by 1000 
	AvgTrackLengthInSecondsInt = x.Tracks.Average(y => y.Milliseconds / 1000) //takes the instance divides that by 1000 and then takes the average
	}
*/	




//Question: What is the media type with the most tracks 
 from x in MediaTypes
 orderby x.Name
 where x.Tracks.Count() > 0
 select new{
 	Name = x.Name,
	NumberOfTracks = x.Tracks.Max(y => y.TrackId)
 }




