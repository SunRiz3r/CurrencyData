namespace CurrencyData.Models;

// Модель валют
public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double Value { get; set; }
}
