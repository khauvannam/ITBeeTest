using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public record OrderDetailRequest([MaxLength(255)] string ProductName, int Quantity, decimal Price);
