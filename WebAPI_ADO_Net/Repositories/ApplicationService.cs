using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_ADO_Net.Models;
using WebAPI_ADO_Net.Repositories.Base;
using WebAPI_ADO_Net.Repositories.Interfaces;

namespace WebAPI_ADO_Net.Repositories
{
    public class ApplicationService : BaseRepository, IApplicationService
    {
        public Task<ApplicationData> GetApplicationAsync(string appName, string appPassword)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Applications WITH(NOLOCK) WHERE NAME = @APPNAME AND PASSWORD = @APPPASSWORD AND IS_ACTIVE = 1", Connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@APPNAME", appName);
                command.Parameters.AddWithValue("@APPPASSWORD", appPassword);

                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ApplicationData application = null;

                if (reader.Read())
                {
                    application = new ApplicationData();
                    application.ID = Convert.ToInt64(reader["ID"]);
                    application.Name = reader["NAME"].ToString();
                    application.Password = reader["PASSWORD"].ToString();
                    application.Description = reader["DESCRIPTION"].ToString();
                    application.RegistrationDate = Convert.ToDateTime(reader["REGISTRATION_DT"]);
                    application.IsActive = Convert.ToBoolean(reader["IS_ACTIVE"]);
                }

                return Task.FromResult(application);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
