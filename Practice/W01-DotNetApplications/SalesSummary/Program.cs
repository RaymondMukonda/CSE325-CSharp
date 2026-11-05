using System.Globalization;
using System.Text;

var salesDirectory = Path.Combine(AppContext.BaseDirectory, "SalesData");
Console.WriteLine(GenerateSalesSummary(salesDirectory, CultureInfo.GetCultureInfo("en-US")));

static string GenerateSalesSummary(string directory, CultureInfo currencyCulture)
{
    if (!Directory.Exists(directory))
    {
        throw new DirectoryNotFoundException($"Sales data directory was not found: {directory}");
    }

    var report = new StringBuilder();
    var files = Directory.GetFiles(directory, "*.txt").Order(StringComparer.OrdinalIgnoreCase);
    decimal totalSales = 0;
    var fileTotals = new List<(string FileName, decimal Total)>();

    foreach (var file in files)
    {
        decimal fileTotal = 0;
        var lineNumber = 0;

        foreach (var line in File.ReadLines(file))
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (!decimal.TryParse(line, NumberStyles.Currency, CultureInfo.InvariantCulture, out var amount))
            {
                throw new FormatException($"Invalid sales amount in {Path.GetFileName(file)} at line {lineNumber}: '{line}'.");
            }

            fileTotal += amount;
        }

        fileTotals.Add((Path.GetFileName(file), fileTotal));
        totalSales += fileTotal;
    }

    report.AppendLine("Sales Summary");
    report.AppendLine("------------------------------");
    report.AppendLine($"Total Sales: {totalSales.ToString("C", currencyCulture)}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var (fileName, total) in fileTotals)
    {
        report.AppendLine($"  {fileName}: {total.ToString("C", currencyCulture)}");
    }

    return report.ToString();
}
