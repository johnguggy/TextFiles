using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace WorkingWithTextFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            CopyDataToTextFile(myDocumentsPath + @"\CustomerList.txt");
            DisplayTextFile(myDocumentsPath + @"\CustomerList.txt");
        }

        static private void CopyDataToTextFile(string fileName)
        {
            try
            {
                // Change the connection string
                // to match with your system.
                string connectionString =
                @"Data Source = LAPTOP-KC2D2QOU\SQLEXPRESS;Initial Catalog = Northwind;Integrated Security = True";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT CustomerId, CompanyName, ContactName, Phone" +
                    " FROM Customers";
                using (connection)
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    using (StreamWriter sw = new StreamWriter(fileName))
                    {
                        while (reader.Read())
                        {
                            string customerRow = String.Format("{0}, {1}, {2}, {3}",
                                reader.GetValue(0),
                                reader.GetValue(1),
                                reader.GetValue(2),
                                reader.GetValue(3));

                            sw.WriteLine(customerRow);
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DisplayTextFile(string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
