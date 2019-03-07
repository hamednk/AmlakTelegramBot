using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class Dal
{
   public string connectionString = "Data Source=.;Initial Catalog=AmlakSazBot;Integrated Security=true";
    public void Execute(string ProcedureName)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand(ProcedureName, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException x)
            {
                Result = x.Number.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }

    public void Execute(string ProcedureName, Dictionary<string, object> Parameters)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand(ProcedureName, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                if (Parameters != null)
                {
                    foreach (KeyValuePair<string, object> Parameter in Parameters)
                    {
                        cmd.Parameters.AddWithValue(Parameter.Key, Parameter.Value);
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (SqlException x)
            {
                Result = x.Number.ToString();
            }
            finally
            {
                con.Close();
            }
        }
    }

    public DataTable ExecuteReader(string ProcedureName)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand(ProcedureName, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter adapter = new SqlDataAdapter()
                {
                    SelectCommand = cmd
                };
                con.Open();
                adapter.Fill(dt);
            }
            catch (SqlException x)
            {
                Result = x.Number.ToString();
            }
            finally
            {
                con.Close();
            }
        }
        return dt;
    }

    public DataTable ExecuteReader(string ProcedureName, Dictionary<string, object> Parameters)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                SqlCommand cmd = new SqlCommand(ProcedureName, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter adapter = new SqlDataAdapter()
                {
                    SelectCommand = cmd
                };
                con.Open();
                if (Parameters != null)
                {
                    foreach (KeyValuePair<string, object> Parameter in Parameters)
                    {
                        cmd.Parameters.AddWithValue(Parameter.Key, Parameter.Value);
                    }
                }
                adapter.Fill(dt);
            }
            catch (SqlException x)
            {
                Result = x.Number.ToString();
            }
            finally
            {
                con.Close();
            }
        }
        return dt;
    }

    public string Result { get; set; }

}
