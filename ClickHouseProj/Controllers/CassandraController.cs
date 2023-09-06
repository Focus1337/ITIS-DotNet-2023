using System.Diagnostics;
using ClickHouseProj.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClickHouseProj.Controllers;

[ApiController]
[Route("[controller]")]
public class CassandraController : ControllerBase
{
    private readonly IDataService _dataService;

    public CassandraController(IEnumerable<IDataService> dataService)
    {
        _dataService = dataService.FirstOrDefault(x => x.GetType() == typeof(CassandraService))!;
    }

    [HttpPost("CreateTable")]
    public async Task<IActionResult> CreateTable(string tableName)
    {
        await _dataService.CreateTable(tableName);
        return Ok();
    }

    [HttpPost("DeleteTable")]
    public async Task<IActionResult> DeleteTable(string tableName)
    {
        await _dataService.DeleteTable(tableName);
        return Ok();
    }

    [HttpPost("PutData")]
    public async Task<IActionResult> PutData(string tableName, int dataId, string dataName) =>
        Ok(await _dataService.PutData(tableName, dataId, dataName));

    [HttpDelete("RemoveData")]
    public async Task<IActionResult> RemoveData(string tableName, int dataId)
    {
        await _dataService.RemoveData(tableName, dataId);
        return Ok();
    }

    [HttpPost("UpdateData")]
    public async Task<IActionResult> UpdateData(string tableName, int dataId, string dataName)
    {
        await _dataService.UpdateData(tableName, dataId, dataName);
        return Ok();
    }

    [HttpGet("ReadTable")]
    public async Task<IActionResult> ReadTable(string tableName, string? condition) =>
        Ok(await _dataService.ReadTable(tableName, condition));

    [HttpGet("ReadTableWithAndFilter")]
    public async Task<IActionResult> ReadTableWithAndFilter(string tableName, string firstCondition,
        string secondCondition) =>
        Ok(await _dataService.ReadTableWithAndFilter(tableName, firstCondition, secondCondition));

    [HttpGet("ReadTableWithOrFilter")]
    public async Task<IActionResult> ReadTableWithOrFilter(string tableName,
        string firstCondition, string secondCondition) =>
        Ok(await _dataService.ReadTableWithOrFilter(tableName, firstCondition, secondCondition));


    private const string TEST_TABLE = "test_table_1241325423";

    [HttpGet("test-insert")]
    public async Task<IActionResult> TestInsert()
    {
        try
        {
            await DeleteTable(TEST_TABLE);
        }
        catch
        {
            // ignored
        }

        await CreateTable(TEST_TABLE);

        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            await PutData(TEST_TABLE, i, $"data{i}");
        }

        var elapsed = sw.Elapsed;
        await DeleteTable(TEST_TABLE);

        return Ok(elapsed);
    }

    [HttpGet("test-update")]
    public async Task<IActionResult> TestUpdate()
    {
        try
        {
            await DeleteTable(TEST_TABLE);
        }
        catch
        {
            // ignored
        }

        await CreateTable(TEST_TABLE);
        await PutData(TEST_TABLE, 1, $"data{1}");

        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            await PutData(TEST_TABLE, 1, $"data{i}");
        }

        var elapsed = sw.Elapsed;
        await DeleteTable(TEST_TABLE);

        return Ok(elapsed);
    }

    [HttpGet("test-search")]
    public async Task<IActionResult> TestSearch()
    {
        try
        {
            await DeleteTable(TEST_TABLE);
        }
        catch
        {
            // ignored
        }

        await CreateTable(TEST_TABLE);
        await PutData(TEST_TABLE, 1, $"data{1}");

        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            await ReadTable(TEST_TABLE, "data_id > 0");
        }

        var elapsed = sw.Elapsed;
        await DeleteTable(TEST_TABLE);

        return Ok(elapsed);
    }

    [HttpGet("test-delete")]
    public async Task<IActionResult> TestDelete()
    {
        try
        {
            await DeleteTable(TEST_TABLE);
        }
        catch
        {
            // ignored
        }

        await CreateTable(TEST_TABLE);
        for (int i = 0; i < 100; i++)
        {
            await PutData(TEST_TABLE, i, $"data{i}");
        }

        var sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            await RemoveData(TEST_TABLE, i);
        }

        var elapsed = sw.Elapsed;
        await DeleteTable(TEST_TABLE);

        return Ok(elapsed);
    }
}