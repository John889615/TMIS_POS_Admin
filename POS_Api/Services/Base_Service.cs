using Microsoft.Data.SqlClient;
using POS_Api.Translators;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.Menu.POS_MenuItems;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMIS_Common.Sql;

namespace POS_Api.Services
{
    public static class Base_Service
    {
        #region Project User Management

        public static async Task<bool> Current_User_Management(User item, string connectionString)
        {
            try
            {
                using (SqlConnection sqlConn = SqlClient.CreateInstance(connectionString))
                {
                    await sqlConn.OpenAsync();
                    var result = await Current_User_Management(item, sqlConn);

                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in generated code");
                return default;
            }
        }

        public static async Task<bool> Current_User_Management(User item, SqlConnection sqlConn)
        {
            try
            {
                MenuItem resultItem = null;

                using (var reader = await SqlClient.ExecuteReaderStoredProcedureAsync(
                        sqlConn,
                        "Users_add_edit_management",
                        new SqlParameter() { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "@UserID", Value = item.UserID },
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Firstname", Value = item.Firstname },
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Lastname", Value = item.Lastname },
                        new SqlParameter() { DbType = DbType.String, Direction = ParameterDirection.Input, ParameterName = "@Username", Value = item.Username }))
                {
                    if (reader.HasRows)
                    {
                        resultItem = await reader.TranslateSingleAsync<MenuItem>(Menu_Translator.Translate_MenuItem);
                        return true;
                    }
                    else
                    {
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in custom code");
                return default;
            }
        }
        #endregion
    }
}
