namespace SimpleWebApp.Repository
{
	public interface IPredictionsRepository
	{
		void SavePrediction(PredictionDto prediction);
		PredictionDto GetPredictionById(int id);
	}
}
