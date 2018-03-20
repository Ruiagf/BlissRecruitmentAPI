namespace Ruiagf.Framework.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public static class DalUtilMethods
    {
        private const short ErrorCodeDeadlock = 1205;
        private const int DefaultCommandTimeoutInSeconds = 30;

        private static uint timesToRetryAfterDeadlock = 3;

        public static uint TimesToRetryAfterDeadlock
        {
            get { return timesToRetryAfterDeadlock; }
            set { timesToRetryAfterDeadlock = value; }
        }

        public static void RetryAfterDeadlock(SqlCommand command)
        {
            uint retryCount = 0;

            while (retryCount < TimesToRetryAfterDeadlock)
            {
                try
                {
                    command.ExecuteNonQuery();

                    return;
                }
                catch (SqlException e)
                {
                    if (e.Number != ErrorCodeDeadlock)
                    {
                        throw;
                    }

                    if (++retryCount == TimesToRetryAfterDeadlock)
                    {
                        throw new TimesToRetryAfterDeadlockExceededException("Times to retry after deadlock exceeded", e);
                    }
                }
            }
        }

        public static IList<T> GetList<T>(string commandText, string connectionString) where T : DataTransferObject, new()
        {
            return GetList<T>(commandText, CommandType.StoredProcedure, connectionString, null, null);
        }

        public static IList<T> GetList<T>(string commandText, string connectionString, SqlParameter[] parameterList) where T : DataTransferObject, new()
        {
            return GetList<T>(commandText, CommandType.StoredProcedure, connectionString, parameterList, null);
        }

        public static IList<T> GetList<T>(string commandText, string connectionString, SqlParameter[] parameterList, int commandTimeoutInSecs) where T : DataTransferObject, new()
        {
            return GetList<T>(commandText, CommandType.StoredProcedure, connectionString, parameterList, commandTimeoutInSecs);
        }

        public static IList<T> GetList<T>(string commandText, CommandType commandType, string connectionString, SqlParameter[] parameterList, int? commandTimeout) where T : DataTransferObject, new()
        {
            IList<T> res = new List<T>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    command.CommandTimeout = commandTimeout.HasValue ? commandTimeout.Value : DefaultCommandTimeoutInSeconds;

                    if (parameterList != null)
                    {
                        foreach (SqlParameter s in parameterList)
                        {
                            if (s != null)
                            {
                                command.Parameters.Add(s);
                            }
                        }
                    }

                    connection.Open();

                    DalUtilMethods.RetryAfterDeadlock<T>(res, command);

                    return res;
                }
            }
        }

        public static int Execute(string commandText, string connectionString)
        {
            return Execute(commandText, CommandType.StoredProcedure, connectionString, null, null);
        }

        public static int Execute(string commandText, string connectionString, SqlParameter[] sqlParameterList)
        {
            return Execute(commandText, CommandType.StoredProcedure, connectionString, sqlParameterList, null);
        }

        public static int Execute(string commandText, string connectionString, SqlParameter[] sqlParameterList, int commandTimeoutInSecs)
        {
            return Execute(commandText, CommandType.StoredProcedure, connectionString, sqlParameterList, commandTimeoutInSecs);
        }

        public static int Execute(string commandText, CommandType commandType, string connectionString, SqlParameter[] parameterList, int? commandTimeout)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    command.CommandTimeout = commandTimeout.HasValue ? commandTimeout.Value : DefaultCommandTimeoutInSeconds;

                    if (parameterList != null)
                    {
                        foreach (SqlParameter s in parameterList)
                        {
                            if (s != null)
                            {
                                command.Parameters.Add(s);
                            }
                        }
                    }

                    var returnValue = command.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;

                    connection.Open();

                    DalUtilMethods.RetryAfterDeadlock(command);

                    return Convert.ToInt32(returnValue.Value);
                }
            }
        }

        public static IList<T> GetList<T>(string commandText, IDbConnection dbConnection) where T : DataTransferObject, new()
        {
            return GetList<T>(commandText, CommandType.StoredProcedure, dbConnection, null, null);
        }

        public static IList<T> GetList<T>(string commandText, IDbConnection dbConnection, SqlParameter[] parameterList) where T : DataTransferObject, new()
        {
            return GetList<T>(commandText, CommandType.StoredProcedure, dbConnection, parameterList, null);
        }

        public static IList<T> GetList<T>(string commandText, IDbConnection dbConnection, SqlParameter[] parameterList, int commandTimeoutInSecs) where T : DataTransferObject, new()
        {
            return GetList<T>(commandText, CommandType.StoredProcedure, dbConnection, parameterList, commandTimeoutInSecs);
        }

        public static IList<T> GetList<T>(string commandText, CommandType commandType, IDbConnection dbConnection, SqlParameter[] parameterList, int? commandTimeout) where T : DataTransferObject, new()
        {
            IList<T> res = new List<T>();

            using (SqlCommand command = new SqlCommand(commandText, (SqlConnection)dbConnection))
            {
                command.CommandType = commandType;
                command.CommandTimeout = commandTimeout.HasValue ? commandTimeout.Value : DefaultCommandTimeoutInSeconds;

                if (parameterList != null)
                {
                    foreach (var s in parameterList)
                    {
                        if (s != null)
                        {
                            command.Parameters.Add(s);
                        }
                    }
                }

                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                DalUtilMethods.RetryAfterDeadlock<T>(res, command);

                return res;
            }
        }

        public static int Execute(string commandText, IDbConnection dbConnection)
        {
            return Execute(commandText, CommandType.StoredProcedure, dbConnection, null, null);
        }

        public static int Execute(string commandText, IDbConnection dbConnection, SqlParameter[] sqlParameterList)
        {
            return Execute(commandText, CommandType.StoredProcedure, dbConnection, sqlParameterList, null);
        }

        public static int Execute(string commandText, IDbConnection dbConnection, SqlParameter[] sqlParameterList, int commandTimeoutInSecs)
        {
            return Execute(commandText, CommandType.StoredProcedure, dbConnection, sqlParameterList, commandTimeoutInSecs);
        }

        public static int Execute(string commandText, CommandType commandType, IDbConnection dbConnection, SqlParameter[] parameterList, int? commandTimeout)
        {
            using (SqlCommand command = new SqlCommand(commandText, (SqlConnection)dbConnection))
            {
                command.CommandType = commandType;
                command.CommandTimeout = commandTimeout.HasValue ? commandTimeout.Value : DefaultCommandTimeoutInSeconds;

                if (parameterList != null)
                {
                    foreach (SqlParameter s in parameterList)
                    {
                        if (s != null)
                        {
                            command.Parameters.Add(s);
                        }
                    }
                }

                var returnValue = command.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;

                if(dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                DalUtilMethods.RetryAfterDeadlock(command);

                return Convert.ToInt32(returnValue.Value);
            }
        }

        private static void RetryAfterDeadlock<T>(IList<T> res, SqlCommand command) where T : DataTransferObject, new()
        {
            uint retryCount = 0;

            while (retryCount < TimesToRetryAfterDeadlock)
            {
                try
                {
                    DoRead<T>(res, command);

                    return;
                }
                catch (SqlException e)
                {
                    if (e.Number != ErrorCodeDeadlock)
                    {
                        throw;
                    }

                    if (++retryCount == TimesToRetryAfterDeadlock)
                    {
                        throw new TimesToRetryAfterDeadlockExceededException("Times to retry after deadlock exceeded", e);
                    }
                }
            }
        }

        private static void DoRead<T>(IList<T> res, SqlCommand command) where T : DataTransferObject, new()
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                T t;

                while (reader.Read())
                {
                    t = new T();

                    t.Fill(reader);

                    res.Add(t);
                }
            }
        }
    }
}