

using Order.OutboxPublisher.Model;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Order.OutboxPublisher.Dapper;

public static class OutBoxDapper
{

    public static async Task<List<OutBox>> GetOutBoxes(this IDbConnection dbConnection)
    {
        if (dbConnection.State == ConnectionState.Closed)
        {
            dbConnection.Open();
        }
        try
        {
            var sqlOutBox = "SELECT * FROM OutBoxs Where PublishedAt IS NULL";
            var outBoxes = await dbConnection.QueryAsync<OutBox>(sqlOutBox);

            dbConnection.Close();

            return outBoxes.ToList();
        }
        catch (Exception ex)
        {
            dbConnection.Close();
        }

        return new List<OutBox>();

    }

    public static void UpdatePublishedDate(this IDbConnection dbConnection,
        IEnumerable<long> OutBoxIds)
    {
        try
        {
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            dbConnection.Execute("UPDATE OutBoxs Set PublishedAt=@publishedAt Where Id IN @ids",
                  param: new { publishedAt = DateTime.UtcNow, ids = OutBoxIds });

            dbConnection.Close();


        }
        catch (Exception ex)
        {
            dbConnection.Close();
        }


    }
}
