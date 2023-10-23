namespace GrpcServer.API.Extentions;

using ServicesExposer;

internal static class ProtoExposerExtention
{
    internal static void ProtoExposer(this WebApplication source) =>
         source
        .View()
        .Download()
        .ProtoVersions();

    private static WebApplication View(this WebApplication source)
    {
        source.MapGet("/protos/view/v{version:int}/{protoName}",
            async (ProtoFileExposer exposer, int version, string protoName) =>
        {
            var result = default(IResult);
            var protoFileText = await exposer.View(version, protoName);

            if (string.IsNullOrWhiteSpace(protoFileText)) result = Results.NotFound();

            result = Results.Text(protoFileText);
            return result;
        });
        return source;
    }

    private static WebApplication Download(this WebApplication source)
    {
        source.MapGet("/protos/download/v{version:int}/{protoName}",
            (ProtoFileExposer exposer, int version, string protoName) =>
        {
            var result = default(IResult);
            var path = exposer.DownloadPath(version, protoName);

            if (!File.Exists(path)) result = Results.NotFound();

            result = Results.File(path);
            return result;
        });
        return source;
    }

    private static WebApplication ProtoVersions(this WebApplication source)
    {
        source.MapGet("/protos", (ProtoFileExposer exposer) =>
        {
            var result = default(IResult);
            var protos = exposer.Protos();

            if (protos is null || !protos.Any()) result = Results.NoContent();

            result = Results.Json(protos);
            return result;
        });
        return source;
    }
}
