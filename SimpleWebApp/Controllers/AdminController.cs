using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SimpleWebApp.Controllers
{
	[ApiController]
	//[Route("{controller}/{action}")]
	public class AdminController : ControllerBase
	{
		PredictionsManager _predictionsManager;

		public AdminController(PredictionsManager predictionsManager)
		{
			_predictionsManager = predictionsManager;
		}

		[HttpGet]
		[Route("getPredictions")]
		public ActionResult GetPredictions()
		{
			var responseData = _predictionsManager.GetAllPredictions();
			return Ok(responseData);
		}

		[HttpPost]
		[Route("addPrediction")]
		public ActionResult AddPrediction([FromBody] Prediction prediction)
		{
			if (prediction == null)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}

			try
			{
				_predictionsManager.AddPrediction(prediction.PredictionString);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			
			return Ok();
		}

		[HttpDelete]
		[Route("deletePrediction")]
		public ActionResult DeletePrediction([FromBody] int id)
		{
			_predictionsManager.DeletePrediction(id);
			return Ok();
		}

		[HttpPut]
		[Route("updatePrediction")]
		public ActionResult UpdatePrediction([FromBody] PredictionUpdateRequest updateRequest)
		{
			_predictionsManager.UpdatePrediction(updateRequest);
			return Ok();
		}
	}
}
