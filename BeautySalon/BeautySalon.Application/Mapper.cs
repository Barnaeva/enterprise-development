using BeautySalon.Domain.Entities;
using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Application.Contracts.Masters;

namespace BeautySalon.Application;

public static class Mapper
{
    public static ClientDto Map(Client entity) => new(entity.Id, entity.FullName, entity.Gender, entity.Birthday, entity.PhoneNumber);

    public static Client Map(ClientCreateUpdateDto dto) =>
        new() { Id = 0, FullName = dto.FullName, Gender = dto.Gender, Birthday = dto.Birthday, PhoneNumber = dto.PhoneNumber };

    public static IList<ClientDto> Map(IList<Client> entities) => [.. entities.Select(Map)];

    public static MasterDto Map(Master entity) =>
        new(entity.Id, entity.FullName, entity.Gender, entity.PassportNumber, entity.Specialization, entity.WorkExperience);

    public static Master Map(MasterCreateUpdateDto dto) =>
        new() { Id = 0, FullName = dto.FullName, Gender = dto.Gender, PassportNumber = dto.PassportNumber, Specialization = dto.Specialization, WorkExperience = dto.WorkExperience};

    public static IList<MasterDto> Map(IList<Master> entities) => [.. entities.Select(Map)];
}
