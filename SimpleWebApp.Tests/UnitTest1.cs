using SimpleWebApp.Repository;
using Xunit;

namespace SimpleWebApp.Tests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			PredictionsDatabaseRepository repository = new PredictionsDatabaseRepository();
			repository.SavePrediction(new PredictionDto() { PredictionText = "test text"});
		}
	}
}
