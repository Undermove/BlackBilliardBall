using System;
using NUnit.Framework;
using SimpleWebApp.Repository;

namespace SimpleWebApp.Tests
{
	public class Tests : IDisposable
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

        [Test]
        public void Test2()
        {
            PredictionsDatabaseRepository repository = new PredictionsDatabaseRepository();
            var prediction = repository.GetPrediction(1);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}