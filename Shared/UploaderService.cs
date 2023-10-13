using Uploader.Data.Models.Dtos;
using Uploader.Data.Services;

namespace Uploader.Shared;

public class UploaderService
{
    private readonly CustomerPictureService _fileService = new();

    public async Task<CreateUserNewFileDto> CreateFile(CreateUserNewFileDto dto)
    {
        return await _fileService.ChangePicture(dto);
    }
}