using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection.Emit;
using System.Runtime.Loader;

namespace InsuranceCorp.Model;

public class Person
{
    public int Id { get; set; }

    [MaxLength(250)]
    [Display(Name = "Jméno")]
    public string? FirstName { get; set; }
    [MaxLength(250)]

    public string? LastName { get; set; }

    [MaxLength(525)]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    [Display(Name = "Rodné číslo")]
    [MaxLength(20)]
    public string? BirthNumber { get; set; }
    public Address? Address { get; set; }
    public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
    public override string ToString() => $"{FirstName} {LastName} {Email} {DateOfBirth.ToString("yyyy")} ({Contracts?.Count()}) {Address?.City}";

}
