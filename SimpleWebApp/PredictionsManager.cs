using System;
using SimpleWebApp.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SimpleWebApp
{
	public class PredictionsManager
	{
		private Random rnd = new Random();
		IPredictionsRepository _repository = new PredictionsDatabaseRepository();

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
			int randomNumber = rnd.Next(0, predictions.Count);
			return predictions[randomNumber];
		}

		public void AddPrediction(string prediction)
		{
			predictions.Add(new Prediction(prediction));
		}

		internal void DeletePrediction(int predictionNumber)
		{
			predictions.RemoveAt(predictionNumber);
		}

		internal void UpdatePrediction(PredictionUpdateRequest predictionUpdate)
		{
			predictions[predictionUpdate.PredictionNumber] = new Prediction(predictionUpdate.NewText);
		}
	}
}
