namespace GrpcServer.API.ServicesExposer;

public class ProtoFileExposer
{
    private readonly string _protosPath;

    public ProtoFileExposer(IWebHostEnvironment hostEnvironment) =>
          _protosPath = Path.Combine(hostEnvironment.ContentRootPath, "Protos");

    public Dictionary<string, IEnumerable<string>> Protos()
    {
        var result = new Dictionary<string, IEnumerable<string>>();
        if (Directory.Exists(_protosPath))
        {
            foreach (var _ in Directory.GetDirectories(_protosPath).Select(Path.GetFileName))
            {
                var protoFiles = Directory
                    .GetFiles(Path.Combine(_protosPath, _))
                    .Select(Path.GetFileName);

                if (protoFiles?.Any() == true) result.Add(_, protoFiles);
            }
        }
        return result;
    }

    public async Task<string> View(int version, string protoName)
    {
        var path = ProtoFilePath(version, protoName);
        var result = File.Exists(path) ? await File.ReadAllTextAsync(path) : string.Empty;
        return result;
    }

    public string DownloadPath(int version, string protoName)
    {
        var path = ProtoFilePath(version, protoName);
        return File.Exists(path) ? path : string.Empty;
    }

    private string ProtoFilePath(int version, string protoName) =>
        Path.Combine(_protosPath, $"v{version}", protoName);
}


