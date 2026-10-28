namespace BeautySalon.Application.Contracts.Services;

/// <summary>
/// Dto для get запросов к услугам
/// </summary>
/// <param name="Name">Название</param>
/// <param name="Category">Категория</param>
/// <param name="Price">Стоимость</param>
/// <param name="Duration">Длительность</param>
public record ServiceCreateUpdateDto(string Name, string Category, decimal Price, int Duration);
