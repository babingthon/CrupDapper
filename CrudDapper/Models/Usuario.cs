﻿namespace CrudDapper.Models;

public class Usuario
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; }
    public string Email { get; set; }
    public string Cargo { get; set; }
    public double Salario { get; set; }
    public string CPF { get; set; }
    public bool Situacao { get; set; }
    public string Senha { get; set; }

}
