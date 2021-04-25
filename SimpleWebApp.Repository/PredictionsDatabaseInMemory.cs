using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWebApp.Repository
{
	class PredictionsDatabaseInMemory : IPredictionsRepository
	{
		public List<PredictionDto> GetAllPredictions()
		{
			throw new NotImplementedException();
		}

		public PredictionDto GetPredictionById(int id)
		{
			throw new NotImplementedException();
		}

		public void RemovePrediction(int id)
		{
			throw new NotImplementedException();
		}

		public void SavePrediction(PredictionDto prediction)
		{
			throw new NotImplementedException();
		}

		public void UpdatePrediction(PredictionDto prediction)
		{
			throw new NotImplementedException();
		}
	}
}
