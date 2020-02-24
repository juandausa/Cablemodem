namespace Infraestructura
{
    public interface IAppSettings
    {
        string JsonStorageFilePath { get; }
        string JsonStorageDirectory { get; }
        string SqlSeedFilePath { get; }
        string SqlSeedDirectory { get; }
    }
}
