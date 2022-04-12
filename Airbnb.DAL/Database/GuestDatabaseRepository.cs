using System.Data.SqlClient;
using Airbnb.CORE.Models;
using Airbnb.CORE.Repositories;

namespace Airbnb.DAL.Database;

public class GuestDatabaseRepository : IGuestRepo
{
    private SqlConnection _conn;
    public GuestDatabaseRepository(SqlConnection conn)
    {
        _conn = conn;
    }
    public Result<List<Guest>> FindAll()
    {
        Result<List<Guest>> result = new Result<List<Guest>>();
        List<Guest> ret = new List<Guest>();
        try
        {
            _conn.Open();
            string sql = "SELECT * FROM Guest";
            SqlCommand cmd = new SqlCommand(sql, _conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Guest g = new Guest();
                g.Id = (int) reader["guest_id"];
                g.FirstName = (string) reader["first_name"];
                g.LastName = (string) reader["last_name"];
                g.Email = (string) reader["email"];
                g.Phone = (string) reader["phone"];
                g.State = (string) reader["state"];
                ret.Add(g);
            }
        }
        catch (Exception e)
        {
            result.AddMessage(e.ToString());
            return result;
        }
        finally
        {
            _conn.Close();
        }
        result.Value = ret;
        return result;
    }

    // public Result<Guest> FindByEmail(string email)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Result<Guest> Create(Guest guest)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Result<Guest> Update(Guest guest)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Result<Guest> Delete(int id)
    // {
    //     throw new NotImplementedException();
    // }
}