using System;
using System.Data;
using System.Collections;
using System.Web;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

/// <summary>
/// Summary description for DataManager
/// </summary>
public class DataManager
{
    #region GLOBAL ATTRIBUTES

    /// <summary>
    /// An Attribute to store No Of Query Executed.
    /// </summary>
    public static int NoOfQueryExecuted = 0;

    /// <summary>
    /// An Attribute to store Connection String.
    /// </summary>
    public String _ConnectionString = "";

    /// <summary>
    /// An Attribute to store the Connection object if Connection
    /// established otherwise Null.
    /// </summary>
    public SqlConnection _Connection = null;

    //----Exception related properties----------------

    /// <summary>
    /// An Attribute to store System Exception Message
    /// </summary>
    public String _ExceptionMessage = "";

    /// <summary>
    /// An Attribute to store User Defined Exception Message
    /// </summary>
    public String _ExceptionUserMessage = "";

    /// <summary>
    /// An Attribute to store Other Error Message
    /// </summary>
    public String _OtherErrorMessage = "";

    /// <summary>
    /// An Attribute to store Success Message
    /// </summary>
    public String _SuccessMessage = "";

    /// <summary>
    /// An Attribute to store System Exception Object
    /// </summary>
    public Exception _EXP = null;

    #endregion

    #region ENUM
    public enum SqlPramDataType
    {
        BIGINT = SqlDbType.BigInt,
        BINARY = SqlDbType.Binary,
        BIT = SqlDbType.Bit,
        CHAR = SqlDbType.Char,
        DATETIME = SqlDbType.DateTime,
        DECIMAL = SqlDbType.Decimal,
        FLOAT = SqlDbType.Float,
        IMAGE = SqlDbType.Image,
        INT = SqlDbType.Int,
        MONEY = SqlDbType.Money,
        NCHAR = SqlDbType.NChar,
        NTEXT = SqlDbType.NText,
        NVARCHAR = SqlDbType.NVarChar,
        REAL = SqlDbType.Real,
        SMALLDATETIME = SqlDbType.SmallDateTime,
        SMALLINT = SqlDbType.SmallInt,
        SMALLMONEY = SqlDbType.SmallMoney,
        TEXT = SqlDbType.Text,
        TIMESTAMP = SqlDbType.Timestamp,
        TINYINT = SqlDbType.TinyInt,
        VARCHAR = SqlDbType.VarChar
    }

    public enum SqlPramDirection
    {
        IN = ParameterDirection.Input,
        OUT = ParameterDirection.Output
    }
    #endregion

    #region CONSTRUCTORS

    /// <summary>
    /// Default Constructor.
    /// Initializes all the Properties.
    /// Connection String is picked from the web.config
    /// and assigned to the _ConnectionString property.
    /// </summary>
    public DataManager()
    {
        _ConnectionString = ConfigurationManager.ConnectionStrings["NewHRDBConnectionString"].ToString();
        _Connection = null;
        _EXP = null;
        _ExceptionMessage = "";
        _ExceptionUserMessage = "";
        _OtherErrorMessage = "";
        _SuccessMessage = "";
    }

    #endregion

    #region MEMBER METHODS

    private void Initialize_Messages()
    {
        _ExceptionMessage = "";
        _ExceptionUserMessage = "";
        _OtherErrorMessage = "";
        _SuccessMessage = "";
    }

    /// <summary> 
    /// Opens a Connection to Oracle
    /// and assigns the connection object 
    /// to the _Connection property
    /// </summary>
    /// 
    public string connection_string()
    {
        return (ConfigurationManager.ConnectionStrings["HRPortalDBConnectionString"].ConnectionString);
    }
    public void Message(Page Page, string Message)
    {
        ScriptManager.RegisterClientScriptBlock(Page.Page, Page.GetType(), "alert", "javascript:alert('" + Message + "')", true);
    }
    private void Open_Connection()
    {
        try
        {
            this._Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["HRPortalDBConnectionString"].ConnectionString);
           
            this._Connection.Open();
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Connetion Failed.";
            this._Connection = null;
            _EXP = exp;
        }

    }

    /// <summary>
    /// Disposes a opened connection
    /// and initializes the _Connection object to null
    /// </summary>
    private void Dispose_Connection()
    {
        //if (this._Connection != null)
        //{
        //    this._Connection.Close();
        //    this._Connection.Dispose();
        //}
        this._Connection = null;
    }

    /// <summary>
    /// Returns a single table based on the query set by the parameter passed.
    /// </summary>
    /// <param name="sql">A String which is a valid select statement that returns single table.</param>
    /// <returns>A DataTable which contains the query result set.</returns>
    public DataTable GetDataTable(string sql)
    {
        Open_Connection();
        DataTable DT = null;
        try
        {
            if (this._Connection != null)
            {
                
                SqlDataAdapter SQL_Adapter = new SqlDataAdapter(sql, this._Connection);
                DataSet DS = new DataSet();
                SQL_Adapter.Fill(DS);

                NoOfQueryExecuted++;

                if (DS.Tables.Count > 0)
                    DT = DS.Tables[0];
                else
                {
                    _OtherErrorMessage = "No DataTable Found.";
                }
            }
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Select Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return DT;
    }

    public DataSet GetDataSet(string sql)
    {
        Open_Connection();
        DataSet DS = null;
        try
        {
            if (this._Connection != null)
            {
                SqlDataAdapter SQL_Adapter = new SqlDataAdapter(sql, this._Connection);
                DS = new DataSet();
                SQL_Adapter.Fill(DS);
            }
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Select Statement Execution Failed.";
            DS = null;
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return DS;
    }
    public void DBGetDataSet(SqlCommand pcmd, ref DataSet ds)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        Open_Connection();
        pcmd.Connection = this._Connection;
        try
        {
            da.SelectCommand = pcmd;
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            this._ExceptionMessage = ex.Message;
        }
        finally
        {
            Dispose_Connection();
        }
    }
    public long GetRecordCount(string sql)
    {
        Open_Connection();
        long rec_count = 0;
        try
        {
            if (this._Connection != null)
            {
                SqlCommand SQL_Command = new SqlCommand(sql, this._Connection);
                Object Returned_Scaler = SQL_Command.ExecuteScalar();
                if (Returned_Scaler != null)
                    rec_count = Convert.ToInt64(Returned_Scaler.ToString());
            }
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Count Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return rec_count;

    }

    public int GetScalarInt(String sql)
    {
        Object Val = GetScalarValue(sql);
        if (Val == null)
            return 0;
        else
            return Convert.ToInt32(Val.ToString());
    }

    public long GetScalarLong(string sql)
    {
        Object Val = GetScalarValue(sql);
        if (Val == null)
            return 0;
        else
            return Convert.ToInt64(Val.ToString());
    }

    public String GetScalarString(string sql)
    {
        Object Val = GetScalarValue(sql);
        if (Val == null)
            return "";
        else
            return Val.ToString();
    }

    public Object GetScalarValue(string sql)
    {
        Open_Connection();
        Object ret_val = null;
        try
        {
            if (this._Connection != null)
            {
                SqlCommand SQL_Command = new SqlCommand(sql, this._Connection);
                Object Returned_Scaler = SQL_Command.ExecuteScalar();
                if (Returned_Scaler != null)
                    ret_val = Returned_Scaler;
            }
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Select Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return ret_val;

    }

    //public long GetSequenceCurrentValue(string SequenceName)
    //{
    //    return GetScalarLong("select " + SequenceName + ".Currval from dual");
    //}

    public int InsertUpdateDeleteRecord(string sql)
    {
        _SuccessMessage = "";
        Open_Connection();
        int retval = 0;
        try
        {
            if (this._Connection != null)
            {
                SqlCommand SQL_Command = new SqlCommand(sql, this._Connection);
                retval = SQL_Command.ExecuteNonQuery();
                if (retval > 0)
                    _SuccessMessage = "Record effected Successfully.";
                else
                    _OtherErrorMessage = "No Record effected.";
            }
        }
        catch (Exception exp)
        {
            retval = -1;
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Insert or Update or Delete Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return retval;
    }

    /// <summary>
    /// Insert record and return Identity filed
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public Int32 InsertAndReturnIdentity(string sql)
    {

        _SuccessMessage = "";
        Open_Connection();
        int retval = 0;
        try
        {
            if (this._Connection != null)
            {
                //string newSql = sql + ";SELECT  LAST_INSERT_ID();";

                SqlCommand SQL_Command = new SqlCommand(sql, this._Connection);
                retval = SQL_Command.ExecuteNonQuery();
                if (retval > 0)
                {
                    SQL_Command.CommandText = "SELECT @@identity";
                    retval = Convert.ToInt16(SQL_Command.ExecuteScalar());
                    _SuccessMessage = "Record effected Successfully.";
                }
                else
                    _OtherErrorMessage = "No Record effected.";
            }
        }
        catch (Exception exp)
        {
            retval = -1;
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Insert or Update or Delete Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return retval;
    }

    #endregion

    #region IMPLEMENT STORE PROCEUDRE

    public SqlParameter GetParameters(String ParamName, SqlPramDataType PramDataType, SqlPramDirection ParamDirectionType)
    {
        return GetParameters(ParamName, PramDataType, ParamDirectionType, 0, null);
    }

    public SqlParameter GetParameters(String ParamName, SqlPramDataType PramDataType, SqlPramDirection ParamDirectionType, int ParamSize)
    {
        return GetParameters(ParamName, PramDataType, ParamDirectionType, ParamSize, null);
    }

    public SqlParameter GetParameters(String ParamName, SqlPramDataType PramDataType, SqlPramDirection ParamDirectionType, int ParamSize, Object ParamValue)
    {
        SqlParameter SParam;

        if (ParamSize > 0)
            SParam = new SqlParameter(ParamName, (SqlDbType)PramDataType, ParamSize);
        else
            SParam = new SqlParameter(ParamName, (SqlDbType)PramDataType);

        SParam.Direction = (ParameterDirection)ParamDirectionType;
        if (ParamValue != null)
            SParam.Value = ParamValue;
        return SParam;
    }

    public int ExexuteStoreProc(string ProcName, ArrayList Params)
    {
        _SuccessMessage = "";
        Open_Connection();
        int retval = 0;
        try
        {
            //if (this._Connection != null)
            //{
            SqlCommand SQL_Command = new SqlCommand(ProcName, this._Connection);
            SQL_Command.CommandType = CommandType.StoredProcedure;


            for (int i = 0; i < Params.Count; i++)
            {
                SQL_Command.Parameters.Add(Params[i]);
            }

            retval = SQL_Command.ExecuteNonQuery();

            //if (Convert.ToInt32(SQL_Command.Parameters["@i_Ret_Val"].Value.ToString()) != 1)
            //     retval = 0;

            if (retval > 0)
                _SuccessMessage = "Procedure Executed Successfully.";
            //else
            //    _OtherErrorMessage = SQL_Command.Parameters["@nv_ExceptionMessage"].Value.ToString();
            //}
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Insert or Update or Delete Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return retval;
    }

    public int InsertUpdateDeleteRecordSP(string ProcName, ArrayList Params)
    {
        _SuccessMessage = "";
        Open_Connection();
        int retval = 0;
        try
        {
            if (this._Connection != null)
            {
                SqlCommand SQL_Command = new SqlCommand(ProcName, this._Connection);
                SQL_Command.CommandType = CommandType.StoredProcedure;


                for (int i = 0; i < Params.Count; i++)
                {
                    SQL_Command.Parameters.Add(Params[i]);
                }

                retval = SQL_Command.ExecuteNonQuery();

                //if (Convert.ToInt32(SQL_Command.Parameters["@i_Ret_Val"].Value.ToString()) != 1)
                //     retval = 0;

                if (retval > 0)
                    _SuccessMessage = "Procedure Executed Successfully.";
                //else
                //    _OtherErrorMessage = SQL_Command.Parameters["@nv_ExceptionMessage"].Value.ToString();
            }
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Insert or Update or Delete Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return retval;
    }
    public int InsertUpdateDeleteRecordwithoutputSP(string ProcName, ArrayList Params)
    {
        _SuccessMessage = "";
        Open_Connection();
        int retval = 0;
        try
        {
            if (this._Connection != null)
            {
                SqlCommand SQL_Command = new SqlCommand(ProcName, this._Connection);
                SQL_Command.CommandType = CommandType.StoredProcedure;


                for (int i = 0; i < Params.Count; i++)
                {
                    SQL_Command.Parameters.Add(Params[i]);
                }

                retval = SQL_Command.ExecuteNonQuery();

                //if (Convert.ToInt32(SQL_Command.Parameters["@i_Ret_Val"].Value.ToString()) != 1)
                //     retval = 0;

                // if (retval > 0)
                // {
                _SuccessMessage = "Procedure Executed Successfully.";
                retval = Convert.ToInt32(SQL_Command.Parameters["@retval"].Value.ToString());
                // }
                //else
                //    _OtherErrorMessage = SQL_Command.Parameters["@nv_ExceptionMessage"].Value.ToString();
            }
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Insert or Update or Delete Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return retval;
    }

    public Int32 InsertAndReturnIdentitySP(string ProcName, ArrayList Params)
    {
        _SuccessMessage = "";
        Open_Connection();
        int retval = 0;
        try
        {
            if (this._Connection != null)
            {
                SqlCommand SQL_Command = new SqlCommand(ProcName, this._Connection);
                SQL_Command.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < Params.Count; i++)
                {
                    SQL_Command.Parameters.Add(Params[i]);
                }
                retval = SQL_Command.ExecuteNonQuery();

                if (retval > 0)
                {
                    SQL_Command.CommandType = CommandType.Text;
                    SQL_Command.CommandText = "SELECT @@identity";
                    retval = Convert.ToInt32(SQL_Command.ExecuteScalar());
                    _SuccessMessage = "Record effected Successfully.";
                }
                else
                    _OtherErrorMessage = "No Record effected.";
            }
        }
        catch (Exception exp)
        {
            retval = -1;
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Insert or Update or Delete Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return retval;
    }

    public DataTable GetDataTableSP(string ProcName, ArrayList Params)
    {
        _SuccessMessage = "";
        Open_Connection();
        DataTable DT = null;
        int retval = 0;
        try
        {
            if (this._Connection != null)
            {
                SqlCommand SQL_Command = new SqlCommand(ProcName, this._Connection);
                SQL_Command.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < Params.Count; i++)
                {
                    SQL_Command.Parameters.Add(Params[i]);
                }

                SqlDataAdapter SQL_Adapter = new SqlDataAdapter(SQL_Command);
                DataSet DS = new DataSet();
                SQL_Adapter.Fill(DS);

                if (DS.Tables.Count > 0)
                    DT = DS.Tables[0];
                else
                {
                    _OtherErrorMessage = "No DataTable Found.";
                }
            }
        }
        catch (Exception exp)
        {
            this._ExceptionMessage = exp.Message;
            this._ExceptionUserMessage = "Select Statement Execution Failed.";
            _EXP = exp;
        }
        finally
        {
            Dispose_Connection();
        }
        return DT;
    }

    #endregion

    public string GetAllExceptionMessage()
    {
        String msg = "";
        if (_ExceptionMessage != "")
        {
            if (msg != "") msg = msg + "\r\n";
            msg = msg + "System Message : " + this._ExceptionMessage;
        }
        if (_ExceptionUserMessage != "")
        {
            if (msg != "") msg = msg + "\r\n";
            msg = msg + "User Message : " + this._ExceptionUserMessage;
        }
        if (_OtherErrorMessage != "")
        {
            if (msg != "") msg = msg + "\r\n";
            msg = msg + "Other Message : " + this._OtherErrorMessage;
        }
        return msg;
    }

    public static void ResetQueryCount()
    {
        NoOfQueryExecuted = 0;
    }
}