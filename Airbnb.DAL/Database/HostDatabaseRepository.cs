using System.Data.SqlClient;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.DAL.Database;

public class HostDatabaseRepository: IHostRepo
{
    private const string tester = "Server=localhost;Database=AirbnbNewYork;User Id=sa;Password=Fhlipoiop122!";
    private SqlConnection _conn;
    
    public HostDatabaseRepository()
    {
        Setup();
    }
    private void Setup()
    {
        _conn = new SqlConnection(tester);
        try
        {
            _conn.Open();
            Console.WriteLine("Connected to database");
            _conn.Close();  
        }catch (Exception e)
        {
            Console.WriteLine("Error connecting to database");
            Console.WriteLine(e.Message);
        }
    }
    
    public Result<List<Host>> FindAll()
    {
        
        Result<List<Host>> result = new Result<List<Host>>();
        List<Host> ret = new List<Host>();
        try
        {
            _conn.Open();
            string sql = "SELECT * FROM Host";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Host host = new Host();
                host.Id = (string) reader["id"];
                host.LastName = (string) reader["last_Name"];
                host.Email = (string) reader["email"];
                host.Phone = (string) reader["phone"];
                host.Address = (string) reader["address"];
                host.City = (string) reader["city"];
                host.State = (string) reader["state"];
                host.PostalCode = (int) reader["postal_code"];
                host.weekdayRate = (int) reader["standard_rate"];
                host.weekendRate = (int) reader["weekend_rate"];
                ret.Add(host);
            }
        }
        catch (Exception e)
        {
            result.AddMessage(e.ToString());
        }
        finally
        {
            _conn.Close();
        }
        result.Value = ret;
        return result;
    }

    // public Result<Host> FindByEmail(string email)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Result<Host> Create(Host host)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Result<Host> Update(Host host)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Result<Host> Delete(Host host)
    // {
    //     throw new NotImplementedException();
    // }
}