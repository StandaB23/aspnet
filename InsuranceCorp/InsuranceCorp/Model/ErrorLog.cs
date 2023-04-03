using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InsuranceCorp.Model;

[Index(nameof(TimeStamp))]
public class ErrorLog
{
    public long Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public string? Message { get; set; }
    public LogLevel Level { get; set; }
}
