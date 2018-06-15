using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

//własna klasa do skracania pisania zapytań
// zawartość funkcji nie jest istotna, ważne co przyjmuje i zwraca
// query to nasze zapytanie, connection - połączneie z bazą

namespace quizy
{
    class SQL
    { 
        
        //select
        static public DataSet Select(OleDbConnection connection, string query, string table) {

            DataSet data = new DataSet();
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(query, connection);
                OleDbDataAdapter AdapterTabela = new OleDbDataAdapter(command);
               
                AdapterTabela.Fill(data, table);
            }
            finally {
                connection.Close();
            }

            return data;
        }

        static public String[,] SimpleSelect(OleDbConnection connection, string table, string[] columns, string whereClause)
        {
            string query = "SELECT "+columns+" FROM "+table+" "+whereClause;
            DataSet data = Select(connection, query, table);

            String[,] result = new string[columns.Length,data.Tables[0].Rows.Count];

            for (int i=0; i<data.Tables[0].Rows.Count; i++)
            {
                
            }

            return result;

        }

        static public string[] SimpleSelect(OleDbConnection connection, string table, string column, string whereClause)
        {
            string query = "SELECT " + column + " FROM " + table + " " + whereClause;
            DataSet data = Select(connection, query, table);

            if (data.Tables[0].Rows.Count == 0)
            {
                return null;
            }

           
            String[] result = new string[data.Tables[0].Rows.Count];

            for (int i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                DataRow row = data.Tables[0].Rows[i];
                result[i] = row[column].ToString();
            }

            return result;

        }

        static public void SimpleDelete(OleDbConnection connection, string table, string column, string value)
        {
            string query = null;
            try
            {
                Int32.Parse(value);
            }
            catch(FormatException fe)
            {
                query = "DELETE FROM " + table + " WHERE " + column + "='" + value + "' ;";
                SQL.SimpleQuery(connection, query);
            }

            query = "DELETE FROM " + table + " WHERE " + column + "=" + value + " ;";
            SQL.SimpleQuery(connection, query);

        }

        /// <summary>
        /// returns content of the cell form the first row of a SELECT result and given column
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <param name="strColumn"></param>
        /// <returns></returns>
        static public String SingleCellSelect(OleDbConnection connection, string query)
        {
            String[] splitQuery = query.Split(' ');
            string strColumn = splitQuery[1];
            string table = splitQuery[3];
            
            DataSet data = Select(connection, query, table);
            DataRow row;

            try
            {
                row = data.Tables[0].Rows[0];
            }
            catch(IndexOutOfRangeException e)
            {
                return null;
            }

            DataColumn column = data.Tables[0].Columns[strColumn];
            String cellContent = row[strColumn].ToString();

            return cellContent;
            
        }

        /// <summary>
        /// conducts query exluding SELECT 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        static public void SimpleQuery(OleDbConnection connection, string query)
        {
            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// returns true if table contains value in column
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public bool checkIfContains(OleDbConnection connection, string table, string column, string value)
        {
            string query = "SELECT " +column+ " FROM " +table+ " WHERE " +column+ "='" +value+ "' ;";
            string result = SingleCellSelect(connection, query);

            if (result == null) return false;
            else return true;
        }

        //STAROC select do ładowania grinda, ale pewnie nie tylko 
        static public DataTable LoadDataGrind(OleDbConnection connection, string query)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
            command.CommandText = query;
            DataTable data = new DataTable();
            dataAdapter.Fill(data);
            connection.Close();
            return data;
        }

        //STAROć takie tam do ściągania aktualnej daty - zwraca string z datą
        static public string getCurrDate(OleDbConnection connection)
        {
            string query = "SELECT Year(Date()) AS Rok, MONTH(Date()) AS Mies, DAY(Date()) AS Dzien";

            DataSet data = Select(connection, query, "tabela");

            string Mies = data.Tables["tabela"].Rows[0]["Mies"].ToString();
            string Dzien = data.Tables["tabela"].Rows[0]["Dzien"].ToString();
            if (Int32.Parse(Mies) < 10) Mies = "0" + Mies;
            if (Int32.Parse(Dzien) < 10) Dzien = "0" + Dzien;

            string date = data.Tables["tabela"].Rows[0]["Rok"].ToString() + " - " + Mies + " - " + Dzien;

            return date;
        }
           
    }
}
