using Microsoft.AspNetCore.Components.Forms;

namespace Uploader.Shared;

public class FileHandler
{

    private string StoragePath { get; set; }
    private long MaxAllowedSize { get; set; }

    private readonly IBrowserFile File;

    public FileHandler(IBrowserFile file)
    {
        File = file;
        StoragePath = "";
    }

    public FileHandler StoreAt(string path)
    {
        StoragePath = path;
        return this;
    }

    public FileHandler LimitStoreTo(long maxAllowedSize)
    {
        MaxAllowedSize = maxAllowedSize;
        return this;
    }

    public async void Create()
    {
        if (string.IsNullOrEmpty(StoragePath))
            throw new InvalidOperationException(
                $"Erro ao criar arquivo: nenhum caminho da storage de arquivos.");

        Directory.CreateDirectory(StoragePath);
        string filePath = FilePath(StoragePath);

        await using FileStream fs = new(filePath, FileMode.Create);
        await File.OpenReadStream(MaxAllowedSize)
                    .CopyToAsync(fs);
    }
    
    private string RandomizedFileName() => Path.ChangeExtension(
            Path.GetRandomFileName(), 
            Path.GetExtension(File.Name));

    private string FilePath(string directoryPath) =>
        Path.Combine(directoryPath, RandomizedFileName());
}


/**
 * - O arquivo precisa receber um nome aleatório porque caso existam arquivos
 *   c/ nomes repetidos, um sobrescreverá o outro.
 */

