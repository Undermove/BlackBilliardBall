using System.Data;
using System.Data.SqlClient;
using Dapper;
using MySql.Data.MySqlClient;

namespace SimpleWebApp.Repository
{
	public class PredictionsDatabaseRepository : IPredictionsRepository
	{
		public void SavePrediction(PredictionDto prediction)
		{
            using (IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=myDataBase;Uid=root;Pwd=my-secret-pw;"))
            {
                string sqlQuery = "INSERT INTO predictions (PredictionText) Values(@PredictionText)";

                int rowsAffected = db.Execute(sqlQuery, prediction);
            }
        }

        public void GetPredictionById(int id)
        {
            using (IDbConnection db = new MySqlConnection("Server=127.0.0.1;Database=myDataBase;Uid=root;Pwd=my-secret-pw;"))
            {
            }
        }
    }
}
