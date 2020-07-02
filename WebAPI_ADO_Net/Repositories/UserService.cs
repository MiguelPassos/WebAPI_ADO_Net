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
    public class UserService : BaseRepository, IUserService
    {
        public Task<User> GetUserAsync(long idUser)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM User WITH(NOLOCK) WHERE ID = @ID", Connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", idUser);

                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                User user = null;

                if (reader.Read())
                {
                    user = new User();
                    user.ID = Convert.ToInt32(reader["ID"]);
                    user.Name = reader["NAME"].ToString();
                    user.Email = reader["EMAIL"].ToString();
                    user.RegistrationDate = Convert.ToDateTime(reader["REGISTRATION_DT"]);
                    user.IsActive = Convert.ToBoolean(reader["IS_ACTIVE"]);
                }

                reader.Close();
                return Task.FromResult(user);
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

        public Task<User> GetUserAsync(string name, string email)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM User WITH(NOLOCK) WHERE NAME = @NAME AND EMAIL = @EMAIL", Connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@NAME", name);
                command.Parameters.AddWithValue("@EMAIL", email);

                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                User user = null;

                if (reader.Read())
                {
                    user = new User();
                    user.ID = Convert.ToInt32(reader["ID"]);
                    user.Name = reader["NAME"].ToString();
                    user.Email = reader["EMAIL"].ToString();
                    user.RegistrationDate = Convert.ToDateTime(reader["REGISTRATION_DT"]);
                    user.IsActive = Convert.ToBoolean(reader["IS_ACTIVE"]);
                }

                reader.Close();
                return Task.FromResult(user);
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

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            List<User> contacts = new List<User>();

            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users WITH(NOLOCK)", Connection);
                command.CommandType = CommandType.Text;

                Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User();
                    user.ID = Convert.ToInt32(reader["ID"]);
                    user.Email = reader["EMAIL"].ToString();
                    user.RegistrationDate = Convert.ToDateTime(reader["REGISTRATION_DT"]);
                    user.IsActive = Convert.ToBoolean(reader["IS_ACTIVE"]);

                    contacts.Add(user);
                }

                reader.Close();
                return Task.FromResult(contacts.AsEnumerable());
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

        public Task<bool> InsertUserAsync(User user)
        {
            try
            {
                User userDB = GetUserAsync(user.Name, user.Email).Result;

                if (userDB != null)
                    return Task.FromResult(false);

                SqlCommand command = new SqlCommand("INSERT INTO Users VALUES (@NAME, @EMAIL, @REGISTRATION_DT)", Connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@NAME", user.Name);
                command.Parameters.AddWithValue("@EMAIL", user.Email);
                command.Parameters.AddWithValue("@REGISTRATION_DT", DateTime.Now);

                Connection.Open();
                command.ExecuteNonQuery();

                return Task.FromResult(true);
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

        public Task<User> UpdateUserAsync(long idUser, User user)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE Users SET NAME = @NAME, EMAIL = @EMAIL, IS_ACTIVE = @ACTIVE WHERE ID = @ID;", Connection);                
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@ID", idUser);
                command.Parameters.AddWithValue("@NAME", user.Name);
                command.Parameters.AddWithValue("@EMAIL", user.Email);
                command.Parameters.AddWithValue("@ACTIVE", user.IsActive);

                Connection.Open();
                command.ExecuteNonQuery();

                user = GetUserAsync(user.ID).Result;

                return Task.FromResult(user);
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
        
        public Task<bool> DeleteUserAsync(long idUser)
        {
            try
            {
                User user = GetUserAsync(idUser).Result;

                if (user == null)
                    return Task.FromResult(false);

                SqlCommand command = new SqlCommand("DELETE FROM Users WHERE ID = @ID", Connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@IDUSER", idUser);

                Connection.Open();
                command.ExecuteNonQuery();

                return Task.FromResult(true);
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
