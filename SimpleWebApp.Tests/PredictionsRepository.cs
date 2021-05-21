using System.Linq;
using SimpleWebApp.Repository;
using Xunit;

namespace SimpleWebApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void SavePredictionTest()
        {
            PredictionsDatabaseRepository repository = new PredictionsDatabaseRepository();
            repository.SavePrediction("test text");
        }

        [Fact]
        public void GetPredictionByIdTest()
        {
            PredictionsDatabaseRepository repository = new PredictionsDatabaseRepository();
            var result = repository.GetPredictionById(1);
        }

        [Fact]
        public void GetAllPredictionsTest()
        {
            PredictionsDatabaseRepository repository = new PredictionsDatabaseRepository();
            var result = repository.GetAllPredictions();
        }

        [Fact]
        public void RemovePredictionTest()
        {
            PredictionsDatabaseRepository repository = new PredictionsDatabaseRepository();
            repository.RemovePrediction(1);
        }

        [Fact]
        public void UpdatePredictionTest()
        {
            PredictionsDatabaseRepository repository = new PredictionsDatabaseRepository();
            var result = repository.GetAllPredictions().FirstOrDefault();
            repository.UpdatePrediction(new PredictionDto { Id = result.Id, PredictionText = "Some new text2" });
        }
    }
}
