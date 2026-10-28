using BeautySalon.Domain.Entities;
using BeautySalon.Application.Contracts.Clients;

namespace BeautySalon.Application;

public static class Mapper
{
    public static ClientDto Map(Client entity) => new(entity.Id, entity.FullName, entity.Gender, entity.Birthday, entity.PhoneNumber);

    public static Client Map(ClientCreateUpdateDto dto) =>
        new() { Id = 0, FullName = dto.FullName, Gender = dto.Gender, Birthday = dto.Birthday, PhoneNumber = dto.PhoneNumber };

    public static IList<ClientDto> Map(IList<Client> entities) => [.. entities.Select(Map)];
}
