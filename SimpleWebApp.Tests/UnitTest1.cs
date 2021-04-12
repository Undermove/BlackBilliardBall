using NUnit.Framework;
using SimpleWebApp.Repository;

namespace SimpleWebApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            PredictionsDatabaseRepository repository = new PredictionsDatabaseRepository();
            repository.SavePrediction(new PredictionDto(){PredictionText="test text"});
        }
    }
}