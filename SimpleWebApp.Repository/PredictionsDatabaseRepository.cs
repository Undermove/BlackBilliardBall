using System;
using System.Data;
using System.Data.SqlClient;

namespace SimpleWebApp.Repository
{
	public class PredictionsDatabaseRepository : IPredictionsRepository
	{
		public void SavePrediction(PredictionDto prediction)
		{
            using (IDbConnection db = new SqlConnection("Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;"))
            {
                string sqlQuery = "Insert Into Customers (FirstName, LastName, Email) Values(@FirstName, @LastName, @Email)";

                int rowsAffected = db.Execute(sqlQuery, customer);
            }
        }
	}
}
