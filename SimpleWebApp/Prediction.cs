namespace SimpleWebApp
{
	public class Prediction
	{
		public int PredictionId { get; set; }
		public string PredictionString { get; set; }

		public Prediction(){}

		public Prediction(string predictionString)
		{
			PredictionString = predictionString;
		}

		public Prediction(int predictionId, string predictionString)
		{
			PredictionString = predictionString;
			PredictionId = predictionId;
		}
	}
}