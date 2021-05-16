using System;
using SimpleWebApp.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWebApp
{
	public class PredictionsManager
	{
		private Random rnd = new Random();
		IPredictionsRepository _repository;

		public PredictionsManager(IPredictionsRepository repository)
		{
			_repository = repository;
		}

		public List<Prediction> GetAllPredictions() 
		{
			return _repository.GetAllPredictions().Select(dto => new Prediction(dto.PredictionText)).ToList();
		}

		public Prediction GetRandomPrediction()
		{
			var predictions = _repository.GetAllPredictions();
			int randomNumber = rnd.Next(0, predictions.Count);
			return new Prediction(predictions[randomNumber].PredictionText);
		}

		public void AddPrediction(string prediction)
		{
			_repository.SavePrediction(new PredictionDto { PredictionText = prediction });
		}

		internal void DeletePrediction(int predictionNumber)
		{
			_repository.RemovePrediction(predictionNumber);
		}

		internal void UpdatePrediction(PredictionUpdateRequest predictionUpdate)
		{
			_repository.UpdatePrediction(new PredictionDto() 
			{ 
				Id = predictionUpdate.PredictionNumber,
				PredictionText = predictionUpdate.NewText
			});
		}
	}
}
