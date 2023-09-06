using System.Data;
using Cassandra;
using ClickHouseProj.Models;

namespace ClickHouseProj.Services;

public class CassandraService : IDataService
{
    private readonly Cassandra.ISession _session;

    private const string KeyspaceName = "data";

    public CassandraService()
    {
        var cluster = Cluster
            .Builder()
            .WithPort(9042)
            .AddContactPoint("localhost")
            .Build();
        _session = cluster.Connect();

        _session.CreateKeyspaceIfNotExists(KeyspaceName);
        _session.ChangeKeyspace(KeyspaceName);
    }

    public async Task CreateTable(string tableName) =>
        await ExecuteCommand(
            $"CREATE TABLE IF NOT EXISTS {KeyspaceName}.{tableName} (data_id int PRIMARY KEY, name text)");

    public async Task DeleteTable(string tableName) =>
        await ExecuteCommand($"DROP TABLE {KeyspaceName}.{tableName}");

    public async Task<List<Data>> PutData(string tableName, int dataId, string userName)
    {
        await ExecuteCommand(
            $"INSERT INTO {KeyspaceName}.{tableName} (data_id, name) VALUES\n({dataId}, '{userName}')");
        return await GetItemsListFromTable(tableName, $"data_id = {dataId}");
    }

    public async Task RemoveData(string tableName, int dataId) =>
        await ExecuteCommand($"DELETE FROM {KeyspaceName}.{tableName} WHERE data_id = {dataId}");

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
        var sql = $"SELECT * FROM {tableName}" + (filter is not null ? $" WHERE {filter}" : null) + " ALLOW FILTERING";
        Console.WriteLine(sql);

        var res = await _session.ExecuteAsync(new SimpleStatement(sql));

        var result = new List<Data>();

        // Iterate through the RowSet
        foreach (var row in res)
        {
            var id = row.GetValue<int>("data_id");
            var name = row.GetValue<string>("name");
            result.Add(new Data(id, name));
        }

        return result;
    }

    private async Task ExecuteCommand(string sqlQuery)
    {
        Console.WriteLine(sqlQuery);
        await _session.ExecuteAsync(new SimpleStatement(sqlQuery));
    }
}