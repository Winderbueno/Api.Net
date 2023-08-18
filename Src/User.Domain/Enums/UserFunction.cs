using System.ComponentModel.DataAnnotations;

namespace User.Domain.Enums;

public enum UserFunction
{
    [Display(Name = "Responsable")]
    Manager = 0,
    [Display(Name = "Employé")]
    Employee,
    [Display(Name = "Directeur")]
    Director,
    [Display(Name = "Inspecteur")]
    Inspector,
    [Display(Name = "Charge de mission")]
    ContractOfficer,
    [Display(Name = "Support")]
    Support,
    [Display(Name = "Rédacteur")]
    Editor
}