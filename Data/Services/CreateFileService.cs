using Microsoft.AspNetCore.Components.Forms;

namespace Uploader.Data.Services;

public class CreateFileService
{
    private string StoragePath { get; set; }
    private string DirectoryName { get; set; }
    private long MaxAllowedSize { get; set; }

    private readonly IBrowserFile File;

    public CreateFileService(IBrowserFile file)
    {
        File = file;
        StoragePath = "";
        DirectoryName = "";
    }

    public CreateFileService StoreAt(string path)
    {
        StoragePath = path;
        return this;
    }

    public CreateFileService InDirectory(string directoryName)
    {
        DirectoryName = directoryName;
        return this;
    }

    public CreateFileService LimitStoreTo(long maxAllowedSize)
    {
        MaxAllowedSize = maxAllowedSize;
        return this;
    }

    public async Task<string> Create()
    {
        if (string.IsNullOrEmpty(StoragePath))
            throw new InvalidOperationException(
                $"Erro ao criar arquivo: nenhum caminho da storage de arquivos.");

        string newFileName = RandomizedFileName();
        CreateDirectory();

        await using FileStream fs = new(FilePath(newFileName), FileMode.Create);
        await File.OpenReadStream(MaxAllowedSize)
                  .CopyToAsync(fs);

        return RelativePath(newFileName);
    }
    
    private string RandomizedFileName() => Path.ChangeExtension(
            Path.GetRandomFileName(), 
            Path.GetExtension(File.Name));
    private void CreateDirectory() => Directory.CreateDirectory(
            Path.Combine(StoragePath, DirectoryName));
    private string FilePath(string fileName) =>
        Path.Combine(StoragePath, DirectoryName, fileName);
    private string RelativePath(string fileName) =>
        Path.Combine(DirectoryName, fileName);
}


/**
 * - O arquivo precisa receber um nome aleatório porque caso existam arquivos
 *   c/ nomes repetidos, um sobrescreverá o outro.
 */

