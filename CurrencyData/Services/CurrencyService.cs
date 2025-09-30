using System.Text.Json;
using CurrencyData.Data;
using CurrencyData.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyData.Services;

public class CurrencyService
{
    private readonly HttpClient _http;
    private readonly AppDbContext _db;

    public CurrencyService(HttpClient http, AppDbContext db)
    {
        _http = http;
        _db = db;
    }

    // Метод получения валют с сайта
    public async Task<List<Currency>> UpdateCurrenciesAsync(string baseCurrency = "EUR")
    {
        var url = $"https://api.frankfurter.app/latest?from={baseCurrency}";
        var response = await _http.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Frankfurter response (all): {json}");

        var data = JsonDocument.Parse(json);
        var rates = data.RootElement.GetProperty("rates");
        var list = new List<Currency>();
        foreach (var rate in rates.EnumerateObject())
        {
            list.Add(new Currency
            {
                Name = rate.Name,
                Value = rate.Value.GetDouble()
            });
        }

        // сохраняем в SQLite
        _db.Currencies.RemoveRange(_db.Currencies);
        await _db.Currencies.AddRangeAsync(list);
        await _db.SaveChangesAsync();

        return list;
    }

    public async Task<List<Currency>> GetCurrenciesFromDbAsync()
    {
        return await _db.Currencies.ToListAsync();
    }
}
