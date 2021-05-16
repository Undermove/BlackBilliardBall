using SimpleWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWebApp
{
	public class PredictionsManager
	{
		private Random rnd = new Random();
		private IPredictionsRepository _predictionsRepository;

		public PredictionsManager(IPredictionsRepository predictionsRepository)
		{
			_predictionsRepository = predictionsRepository;
		}

		public List<Prediction> GetAllPredictions() 
		{
			return _predictionsRepository.GetAllPredictions().Select(dto => new Prediction(dto.PredictionText)).ToList();
		}

		public Prediction GetRandomPrediction()
		{
			var predictions = _predictionsRepository.GetAllPredictions();
			int randomNumber = rnd.Next(0, predictions.Count);
			var predictionDto = predictions[randomNumber];
			return new Prediction(predictionDto.PredictionText);
		}

		public void AddPrediction(string prediction)
		{
			_predictionsRepository.SavePrediction(new PredictionDto() { PredictionText = prediction });
		}

		internal void DeletePrediction(int predictionNumber)
		{
			_predictionsRepository.RemovePrediction(predictionNumber);
		}

		internal void UpdatePrediction(PredictionUpdateRequest predictionUpdate)
		{
			_predictionsRepository.UpdatePrediction(new PredictionDto() { Id = predictionUpdate.PredictionNumber, PredictionText = predictionUpdate.NewText });
		}
	}
}
