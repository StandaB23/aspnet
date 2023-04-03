using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection.Emit;

namespace InsuranceCorp.Model;
public class Contract
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string? Name { get; set; }

    [MaxLength(250)]
    public string? Number { get; set; }
    public DateTime Signed { get; set; }
    public bool IsActive { get; set; }
    public override string ToString() => $"{Number} {Name} {IsActive} {Signed.ToString("yyyy-MM-dd")}";

}
