using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using MongoDB.Driver;
using MongoDBSelect;
using MongoDB.Bson;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var mongoUri = "mongodb://localhost:27017";

		IMongoClient client;
		IMongoCollection<Data> collection;
		try
		{
			client = new MongoClient(mongoUri);
		}
		catch (Exception e)
		{
			Console.WriteLine("There was a problem connecting to your " +
				"Atlas cluster. Check that the URI includes a valid " +
				"username and password, and that your IP address is " +
				$"in the Access List. Message: {e.Message}");
			Console.WriteLine(e);
			Console.WriteLine();
			return;
		}
		var dbName = "admin";
		var collectionName = "Measurements";

		collection = client.GetDatabase(dbName)
		   .GetCollection<Data>(collectionName);
		//var deleteFilter = Builders<Data>.Filter.Empty; 
		//await collection.DeleteManyAsync(deleteFilter);
		int i = 0;
		while (true)
		{
			var stopwatch = new Stopwatch();

			var oneMinuteAgo = DateTime.UtcNow.AddMilliseconds(-100);
			stopwatch.Start();
			var data = collection.Find(x => (x.DateTime > oneMinuteAgo && x.DateTime < DateTime.UtcNow) && x.temperature < 0 && (x.roomId == 20 || x.roomId == 21 || x.roomId == 22)).ToList();
			stopwatch.Stop();
			Console.WriteLine($"i: {i}, Count: {data.Count} time: {stopwatch.ElapsedMilliseconds} ms");
			//stopwatch.Reset();
			await Task.Delay(0);
			i++;
		}
		//Mongo DB 5237 and 442 за 1 сек
		//SQL 1450 and 770 за 1 сек
	}
}