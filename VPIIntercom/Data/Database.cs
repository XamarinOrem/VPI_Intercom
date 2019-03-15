using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using VPIIntercom.Models;

namespace VPIXamarinIntercom.Data
{
    public class Database
    {
        /// <summary>
        /// Declaration part
        /// </summary>
        readonly SQLiteConnection database;

        /// <summary>
        /// Constructor part
        /// </summary>
        /// <param name="dbPath"></param>
        public Database(string dbPath)
        {
            try
            {
                // Intialization of the database while dbpath is getting from the platform specific code.
                database = new SQLiteConnection(dbPath);

                // Creation of the login table that will save all the details that will be retireved from the login response.
                database.CreateTable<SaveLoginResponse>();
            }

            catch (Exception) { }
            finally { }
        }

        #region Login
        /// <summary>
        /// This is the method of get type which will return all the details of the login which we have stored 
        /// in the table when we logged in to the application.
        /// </summary>
        /// <returns></returns>
        public SaveLoginResponse GetLoginUser()
        {
            var user = new SaveLoginResponse();
            try
            {
                // getting the login details from the table
                user = database.Table<SaveLoginResponse>().First();
            }
            catch (Exception) { }
            finally { }
            return user; // returning the deatils of the logged in user.
        }

        /// <summary>
        /// this is the method that will store all the details of the logged in user into the table
        /// which will help further for getting all the details.
        /// </summary>
        /// <param name="objLoggedUser"></param>
        /// <returns></returns>
        public int SaveLoggedUser(SaveLoginResponse objLoggedUser)
        {
            int status = 0;
            try
            {
                // deleting all the records from the logged in table as if there is any record present.
                database.DeleteAll<SaveLoginResponse>();

                // saving the login details into the login table.
                status = database.Insert(objLoggedUser);
            }
            catch (Exception)
            {
                status = 0;
            }
            finally { }
            return status; // returning the status either 1 or 0.
        }

        /// <summary>
        /// This is the method that will clear all the details of logged in user from the table. 
        /// </summary>
        /// <returns></returns>
        public int ClearLoginDetails()
        {
            var status = 0;
            try
            {
                // getting all the data first from login table in order to perform delete operation.
                var data = database.Table<SaveLoginResponse>().ToList();

                foreach (var item in data)
                {
                    // deleting all the details from the login table.
                    status = database.Delete(item);
                }

            }
            catch (Exception)
            {
                status = 0;
            }
            finally { }
            return status; // returning the status either 0 or 1.
        }


#endregion
    }
}
