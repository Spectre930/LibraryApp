﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp.Models.Models;

public class Employees
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string F_Name { get; set; }

    [Required]
    public string L_Name { get; set; }

    [Required]
    public string Email { get; set; }


    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    [Required]
    public DateTime DOB { get; set; }

    [AllowNull]
    public int Age { get; set; }
    [Required]
    public float PhoneNumber { get; set; }

    public int? RoleId { get; set; }
    public virtual Roles? Role { get; set; } = null!;
}
