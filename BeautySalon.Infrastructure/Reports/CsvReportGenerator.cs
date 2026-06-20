using System.Text;
using BeautySalon.Application.Reports;

namespace BeautySalon.Infrastructure.Reports;

public class CsvReportGenerator
{
    public byte[] Generate(IEnumerable<AppointmentReportRow> rows)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,UserName,ServiceName,Status,CreatedAt");

        foreach (var row in rows)
        {
            sb.AppendLine($"{row.Id},{Escape(row.UserName)},{Escape(row.ServiceName)},{Escape(row.Status)},{row.CreatedAt:O}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private static string Escape(string value)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }
}