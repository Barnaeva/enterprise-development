using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Appointments;
using BeautySalon.Domain;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Services;

/// <summary>
/// Служба для CRUD операций c записями
/// </summary>
/// <param name="appointmentRepository">Репозиторий записей</param>
public class AppointmentService(IRepository<Appointment, int> appointmentRepository) : IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<AppointmentDto> Create(AppointmentCreateUpdateDto dto)
    {
        var appointment = Mapper.Map(dto);

        var result = await appointmentRepository.Create(appointment);

        return Mapper.Map(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id) => await appointmentRepository.Delete(id);

    /// <inheritdoc/>
    public async Task<AppointmentDto?> Get(int id)
    {
        var appointment = await appointmentRepository.Read(id);

        if (appointment == null)
            return null;

        return Mapper.Map(appointment);
    }

    /// <inheritdoc/>
    public async Task<IList<AppointmentDto>> GetAll() => Mapper.Map(await appointmentRepository.ReadAll());

    /// <inheritdoc/>
    public async Task<AppointmentDto> Update(AppointmentCreateUpdateDto dto, int id)
    {
        var appointment = Mapper.Map(dto);
        appointment.Id = id;

        var result = await appointmentRepository.Update(appointment);
        return Mapper.Map(result);
    }
}
