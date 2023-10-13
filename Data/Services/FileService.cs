using Microsoft.AspNetCore.Components.Forms;

namespace Uploader.Data.Services;

public class FileService
{
    public async Task Create(IBrowserFile file, string userName)
    {
        try
        {
            await Task.Run(() => new CreateFileService(file)
                    .StoreAt($"../_temp/{userName}")
                    .LimitStoreTo(1024 * 1024 / 2)
                    .Create());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}