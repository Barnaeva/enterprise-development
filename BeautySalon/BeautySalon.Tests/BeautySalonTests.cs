namespace BeautySalon.Tests;

/// <summary>
/// Unit тесты
/// </summary>
public class BeautySalonTests
{
    /// <summary>
    /// Проверяет информацию о всех мастерах, стаж работы которых не меньше 5 лет
    /// </summary>
    [Fact]
    public void GetMastersWithExperienceAtLeastFiveYears()
    {
        var result = TestData.Masters
            .Where(master => master.WorkExperience >= 5)
            .OrderBy(master => master.FullName)
            .Select(master => master.FullName)
            .ToList();

        var expected = new List<string>
        {
            "Волкова Ольга Максимовна",
            "Иванова Анна Сергеевна",
            "Кузнецов Дмитрий Олегович",
            "Лебедев Артем Сергеевич",
            "Петрова Мария Андреевна",
            "Соколова Елена Викторовна",
            "Федорова Виктория Ильинична"
        };

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет информацию о всех окошках выбранного мастера
    /// </summary>
    [Fact]
    public void GetFreeSlotsForSelectedMaster()
    {
        var selectedMaster = TestData.Masters.First();

        var appointments = TestData.Appointments
            .Where(appointment => appointment.Master.Id == selectedMaster.Id)
            .Where(appointment =>
                appointment.StartTime.Date == new DateTime(2026, 9, 25))
            .OrderBy(appointment => appointment.StartTime)
            .ToList();

        var result = appointments
            .Select((appointment, index) => new
            {
                Current = appointment,
                Next = index + 1 < appointments.Count
                    ? appointments[index + 1]
                    : null
            })
            .Where(item => item.Next != null)
            .Select(item => new
            {
                Start = item.Current.StartTime.AddMinutes(
                    item.Current.Service.Duration),
                End = item.Next!.StartTime
            })
            .Where(item => item.Start < item.End)
            .Select(item => $"{item.Start:HH:mm}-{item.End:HH:mm}")
            .ToList();

        var expected = new List<string>
        {
            "10:00-11:00",
            "12:30-14:00"
        };

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверет топ 5 наиболее популярных услуг
    /// </summary>
    [Fact]
    public void GetTopFivePopularServices()
    {
        var result = TestData.Appointments
            .GroupBy(appointment => appointment.Service)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key.Name)
            .Take(5)
            .Select(group => group.Key.Name)
            .ToList();

        var expected = new List<string>
        {
            "Стрижка",
            "Маникюр",
            "Окрашивание",
            "Педикюр",
            "Укладка"
        };

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет информацию о количестве повторных записей клиентов за последний месяц
    /// </summary>
    [Fact]
    public void GetNumberOfRepeatClientAppointmentsForLastMonth()
    {
        var currentDate = new DateTime(2026, 9, 25);
        var monthStart = currentDate.AddMonths(-1);

        var result = TestData.Appointments
            .Where(appointment =>
                appointment.StartTime.Date >= monthStart.Date &&
                appointment.StartTime.Date <= currentDate.Date)
            .GroupBy(appointment => appointment.Client)
            .Where(group => group.Count() > 1)
            .Sum(group => group.Count() - 1);

        const int expected = 8;

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет информацию о клиентах, записанных к нескольким мастерам, упорядочить по дате рождения
    /// </summary>
    [Fact]
    public void GetClientsRegisteredWithSeveralMasters()
    {
        var result = TestData.Appointments
            .GroupBy(appointment => appointment.Client)
            .Where(group =>
                group.Select(appointment => appointment.Master.Id)
                    .Distinct()
                    .Count() > 1)
            .OrderBy(group => group.Key.Birthday)
            .Select(group => group.Key.FullName)
            .ToList();

        var expected = new List<string>
        {
            "Иванов Сергей Петрович",
            "Смирнова Марина Алексеевна",
            "Кузнецова Ольга Дмитриевна"
        };

        Assert.Equal(expected, result);
    }
}