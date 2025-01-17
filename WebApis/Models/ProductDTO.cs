﻿using System.ComponentModel.DataAnnotations;

public class ProductDTO
{
    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O preço do produto é obrigatório")]
    [Range(0.01, 1000000, ErrorMessage = "O preço deve estar entre 0.01 e 1.000.000")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "O ID da categoria é obrigatório")]
    public int CategoryId { get; set; }
}
