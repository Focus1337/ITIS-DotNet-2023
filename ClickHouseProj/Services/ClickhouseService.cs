using System.Data;
using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;
using ClickHouseProj.Models;

namespace ClickHouseProj.Services;

public class ClickhouseService : IDataService
{
    private readonly ClickHouseConnection _connection;

    public ClickhouseService() =>
        _connection = new ClickHouseConnection("Host=localhost;Protocol=http;Port=8123;");

    public async Task CreateTable(string tableName)
    {
        var query =
            $"CREATE TABLE IF NOT EXISTS default.{tableName}\n(\ndata_id UInt32,\nname String\n)\nENGINE = MergeTree()\nPRIMARY KEY(data_id)";
        Console.WriteLine(query);
        await _connection.ExecuteScalarAsync(query);
    }

    public async Task DeleteTable(string tableName)
    {
        var query =
            $"DROP TABLE {tableName}";
        Console.WriteLine(query);
        await _connection.ExecuteScalarAsync(query);
    }

    public async Task<List<Data>> PutData(string tableName, int dataId, string userName)
    {
        var query =
            $"INSERT INTO default.{tableName} (data_id, name) VALUES\n({dataId}, '{userName}')";
        Console.WriteLine(query);
        await _connection.ExecuteScalarAsync(query);

        return await GetItemsListFromTable(tableName, $"data_id = {dataId}");
    }

    public async Task RemoveData(string tableName, int dataId)
    {
        var query =
            $"DELETE FROM {tableName} WHERE data_id = {dataId}";
        Console.WriteLine(query);
        await _connection.ExecuteScalarAsync(query);
    }

    public async Task UpdateData(string tableName, int dataId, string dataName) =>
        await PutData(tableName, dataId, dataName);

    public async Task<List<Data>> ReadTable(string tableName, string? condition) =>
        await GetItemsListFromTable(tableName, condition);

    public async Task<List<Data>> ReadTableWithAndFilter(string tableName, string firstCondition,
        string secondCondition) =>
        await GetItemsListFromTable(tableName, $"{firstCondition} AND {secondCondition}");

    public async Task<List<Data>> ReadTableWithOrFilter(string tableName,
        string firstCondition, string secondCondition) =>
        await GetItemsListFromTable(tableName, $"{firstCondition} OR {secondCondition}");

    public async Task<List<Data>> GetItemsListFromTable(string tableName, string? filter = default)
    {
        var sql = $"SELECT * FROM {tableName}" + (filter is not null ? $" WHERE {filter}" : null);
        Console.WriteLine(sql);
        var res = _connection.ExecuteDataTable(sql);
        var rows = res.Rows;

        return (from DataRow row in rows select new Data(int.Parse(row["data_id"].ToString()), row["name"].ToString()))
            .ToList();
    }
}