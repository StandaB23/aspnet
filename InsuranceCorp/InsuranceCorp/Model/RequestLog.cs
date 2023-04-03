using System.ComponentModel.DataAnnotations;

namespace InsuranceCorp.Model;


/// <summary>
/// Logovani prichozich requestu
/// </summary>
public class RequestLog
{
    public int Id { get; set; }
    public DateTime Date { set; get; }

    [MaxLength(500)]
    public string? Url { get; set; }

    [MaxLength(100)]
    public string? IP { get; set; }

    [MaxLength(500)]
    public string? Referer { get; set; }

    [MaxLength(500)]
    public string? UserAgent { get; set; }

    [MaxLength(500)]
    public string? User { get; set; }

}
