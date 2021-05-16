using System.Collections.Generic;

namespace SimpleWebApp.Repository
{
	public interface IPredictionsRepository
	{
		void SavePrediction(string prediction);
		PredictionDto GetPredictionById(int id);
		List<PredictionDto> GetAllPredictions();
		void RemovePrediction(int id);
		void UpdatePrediction(PredictionDto prediction);
	}
}
