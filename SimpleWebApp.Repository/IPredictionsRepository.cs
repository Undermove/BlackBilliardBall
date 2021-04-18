namespace SimpleWebApp.Repository
{
	public interface IPredictionsRepository
	{
		void SavePrediction(PredictionDto prediction);
		void GetPredictionById(int id)
	}
}
