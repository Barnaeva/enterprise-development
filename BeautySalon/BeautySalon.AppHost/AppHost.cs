var builder = DistributedApplication.CreateBuilder(args);

const string database = "beauty_salon";

var password = builder.AddParameter("MySqlPassword", true);

var beautySalonDb = builder.AddMySql("mysql", password: password)
    .AddDatabase(database);

builder.AddProject<Projects.BeautySalon_Api>("beautysalon-api")
    .WithReference(beautySalonDb, "MySQL")
    .WaitFor(beautySalonDb);

builder.Build().Run();
