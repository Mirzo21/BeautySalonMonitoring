namespace BeautySalon.Application.Reports;

public record AppointmentReportRow(
    int Id,
    string UserName,
    string ServiceName,
    string Status,
    DateTime CreatedAt
);