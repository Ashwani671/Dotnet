using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Bank_MVC.Models;

public partial class Customer
{
    [Key]
    public int AccountNumber { get; set; }

    public string FullName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public decimal? Balance { get; set; }

    public virtual ICollection<Transaction> TransactionReciverAccountNavigations { get; set; } = new List<Transaction>();

    public virtual ICollection<Transaction> TransactionSenderAccountNavigations { get; set; } = new List<Transaction>();

    public static Customer CustomerLogin(Login login)
    {
        Customer customer=null; 
        SqlConnection connection = new SqlConnection();
        connection.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = mvc_bank; Integrated Security = True";
        connection.Open();

        // create command object and set properties
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandType = CommandType.Text;
        command.CommandText = "Select * from Customers where Username=@username and Password=@password ";
        // set command parameters
        command.Parameters.AddWithValue("@UserName", login.Username);
        command.Parameters.AddWithValue("@Password", login.Password);

        try
        {
            SqlDataReader sdr = command.ExecuteReader();
            if(sdr.Read())
            {
                customer = new Customer();
                customer.AccountNumber = sdr.GetInt32(0);
                customer.FullName = sdr.GetString(1);
                customer.Username = sdr.GetString(2);
                customer.Password = sdr.GetString(3);
                customer.Balance = sdr.GetDecimal(4);
            }
        }
        catch
        {
            throw new Exception("Invalid Login Credentials");
        }
        finally
        {
            connection.Close();
        }
        return customer;
    }
}
