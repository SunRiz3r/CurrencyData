namespace CurrencyData.Models;

// Модель для валют из сайта
public class FrankfurterResponse
{
    public string Base { get; set; } = "";
    public string Date { get; set; } = "";
    public Dictionary<string, double> Rates { get; set; } = new();
}
