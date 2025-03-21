namespace Domain.ProjectSettings;

public static class ConnString
{
    public static string SqlServer(
        string database = "StoreDatabase",
        string server = "127.0.0.1,1433"
    )
    {
        return $"Server={server};Initial Catalog={database};User ID=sa;Password=Nam09189921;TrustServerCertificate=True;Encrypt=False";
    }
}
