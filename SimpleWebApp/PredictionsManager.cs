using System;
using System.Collections.Generic;
	
namespace SimpleWebApp
{
	public class PredictionsManager
	{
		private Random rnd = new Random(); 
		private List<Prediction> predictions = new List<Prediction>() 
		{
			new Prediction("Да"),
			new Prediction("Нет"),
			new Prediction("Не знаю")
		};

		public List<Prediction> GetAllPredictions() 
		{
			return predictions;
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
	}
}
