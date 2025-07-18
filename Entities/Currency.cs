public enum Currency
{
    ARS,
    USD,
    EUR
}

public static class CurrencyExtensions
{
    public static string GetSymbol(this Currency currency)
    {
        return currency switch
        {
            Currency.ARS => "$",
            Currency.USD => "US$",
            Currency.EUR => "€",
            _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
        };
    }
}