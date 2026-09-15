using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using Microsoft.Extensions.Configuration;

ISqlDataAccess db = GetConnection();
Authenticate(db);

static void Authenticate(ISqlDataAccess db)
{
    SqlData data = new(db);

    Console.Write("Username: ");
    string? username = Console.ReadLine();

    Console.Write("Password: ");
    string? password = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine();
        Console.WriteLine("Username and password are required.");
        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        return;
    }

    var user = data.Authenticate(username!, password!);

    if (user != null)
    {
        Console.WriteLine();
        Console.WriteLine("Login successful!");
        Console.WriteLine($"Welcome, {user.FirstName} {user.LastName}!");
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("Invalid username or password.");
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}

static ISqlDataAccess GetConnection()
{
    IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    return new SqlDataAccess(config);
}
