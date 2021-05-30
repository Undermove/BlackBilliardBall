using Microsoft.AspNetCore.Mvc;

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
	}
}
