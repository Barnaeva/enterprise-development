using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Application.Contracts.Masters;
using BeautySalon.Application.Contracts.Services;

namespace BeautySalon.Application.Contracts;

public interface IAnalyticService
{
    public Task<IList<MasterDto>> GetMastersWithExperienceAtLeastFiveYears();

    public Task<IList<DateTime>> GetFreeSlotsForSelectedMaster();

    public Task<IList<ServiceDto>> GetTopFivePopularServices();

    public Task<int> GetNumberOfRepeatClientAppointmentsForLastMonth();

    public Task<IList<ClientDto>> GetClientsRegisteredWithSeveralMasters();
}
