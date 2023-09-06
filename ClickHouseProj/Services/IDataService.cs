using ClickHouseProj.Models;

namespace ClickHouseProj.Services;

public interface IDataService
{
    Task CreateTable(string tableName);
    Task DeleteTable(string tableName);
    Task<List<Data>> PutData(string tableName, int dataId, string userName);
    Task RemoveData(string tableName, int dataId);
    Task UpdateData(string tableName, int dataId, string dataName);
    Task<List<Data>> ReadTable(string tableName, string? condition);

    Task<List<Data>> ReadTableWithAndFilter(string tableName, string firstCondition,
        string secondCondition);

    Task<List<Data>> ReadTableWithOrFilter(string tableName,
        string firstCondition, string secondCondition);

    Task<List<Data>> GetItemsListFromTable(string tableName, string? filter = default);
}