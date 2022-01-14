using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Xml;

namespace PokemonRPG.Configs
{
    public static class SQLData
    {
        public static DataTable MakeDT(List<DataTableFeatures> Columns)
        {
            DataTable DT = new DataTable();
            foreach (var col in Columns)
            {
                DataColumn column = new DataColumn();
                column.ColumnName = col.ColumnName;
                column.DataType = col.ColumnType;
                DT.Columns.Add(column);
            }

            return DT;
        }
        public static string GenerateSQLConString(string DatabaseName)
        {
            return
                $"Server={ConfigurationManager.AppSettings["SQLIP"]}; Database={DatabaseName}; User Id={ConfigurationManager.AppSettings["Username"]}; Password={ConfigurationManager.AppSettings["Password"]}";
        }
        public static DataTable DatatableFill(string DatabaseName, string TableName, string ConString, string Where = null)
        {
            #region preset strings
            DataTable dbDATA = new DataTable();
            SqlConnection conn;
            #endregion

            try
            {
                //make MySQL Cnnection
                conn = new SqlConnection(ConString);
                // Make MySQL COmmand
                string query = $"SELECT * FROM [{DatabaseName}].[dbo].[{TableName}]"; // +" WHERE HERE IF NEEDED";
                if (!string.IsNullOrWhiteSpace(Where))
                    query += $" WHERE {Where}";

                SqlCommand cmd = new SqlCommand(query, conn);
                //from this point the connection to the DB is live
                conn.Open();
                // create data adapter
                using (var da = new SqlDataAdapter(cmd))
                {
                    //Fill the table!
                    da.Fill(dbDATA);
                }
                //close afer we are done
                conn.Close();
                //empty the buffer of the command we created to prevent interception
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                return null;
            }
            return dbDATA;
        }

        public static void SQLInsert(DataTable DT, string DatabaseName, string TableName, string ConString)
        {
            StringBuilder SQLSTR = new StringBuilder();
            StringBuilder colNames = new StringBuilder();
            StringBuilder colValues = new StringBuilder();
            SQLSTR.Append($"INSERT INTO [{DatabaseName}].[dbo].[{TableName}] ");
            colNames.Append("(");
            colValues.Append("VALUES (");
            int ParameterCount = 1;
            foreach (DataColumn col in DT.Columns)
            {

                colNames.Append($"[{col.ColumnName}]");
                colValues.Append($"@Val{ParameterCount}");


                if (ParameterCount != DT.Columns.Count)
                {
                    colNames.Append(",");
                    colValues.Append(",");
                }
                else
                {
                    colNames.Append(") ");
                    colValues.Append(")");
                }
                ParameterCount++;
            }
            using (var conn = new SqlConnection(ConString))
            {
                conn.Open();
                var SQLString = $"{SQLSTR}{colNames}{colValues}";

                using (var Command = new SqlCommand(SQLString, conn))
                {
                    for (int i = 1; i < ParameterCount; i++)
                    {
                        SqlParameter Param = new SqlParameter
                        {
                            ParameterName = $"@Val{i}",
                            SqlDbType = GetSqlType(DT.Columns[i - 1].DataType),
                            Size = DT.Columns[i - 1].MaxLength
                        };

                        Command.Parameters.Add(Param);
                    }
                    

                    foreach (DataRow row in DT.Rows)
                    {
                        try
                        {
                            for (int i = 1; i < ParameterCount; i++)
                            {
                                Command.Parameters[$"@Val{i}"].Value = row[i - 1];
                            }
                            Command.ExecuteNonQuery();
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex}");
                            break;
                        }

                    }
                }
            }
        }

        public static void SQLDelete(DataTable DT, string DatabaseName, string TableName, string ConString, bool OutputRowDebug = false)
        {
            StringBuilder SQLSTR = new StringBuilder();
            SQLSTR.Append($"DELETE FROM [{DatabaseName}].[dbo].[{TableName}] WHERE ");
            int ParameterCount = 1;
            foreach (DataColumn col in DT.Columns)
            {
                SQLSTR.Append($"[{col.ColumnName}] = @Val{ParameterCount}");

                if (ParameterCount != DT.Columns.Count)
                    SQLSTR.Append(" AND ");
                ParameterCount++;
            }

            using (var conn = new SqlConnection(ConString))
            {
                conn.Open();
                var SQLString = SQLSTR.ToString();

                using (var Command = new SqlCommand(SQLString, conn))
                {
                    for (int i = 1; i < ParameterCount; i++)
                        Command.Parameters.Add($"@Val{i}", GetSqlType(DT.Columns[i - 1].DataType), DT.Columns[i - 1].MaxLength);
                    foreach (DataRow row in DT.Rows)
                    {
                        try
                        {
                            for (int i = 1; i < ParameterCount; i++)
                            {
                                Command.Parameters[$"@Val{i}"].Value = row[i - 1];
                            }
                            Command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex}");
                            break;
                        }

                    }
                }
            }
        }


        public static SqlDbType GetSqlType(Type type)
        {
            if (type == typeof(string))
                return SqlDbType.NVarChar;
            if (type == typeof(XmlDocument))
                return SqlDbType.Xml;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = Nullable.GetUnderlyingType(type);

            var param = new SqlParameter("", Activator.CreateInstance(type));
            return param.SqlDbType;

        }
    }
}