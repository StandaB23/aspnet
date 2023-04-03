using System.ComponentModel.DataAnnotations;

namespace InsuranceCorp.Model;
public class Address
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string? Street { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(20)]
    public string? ZipCode { get; set; }

    public override string ToString() => $"{Street} {City} {ZipCode}";
}
