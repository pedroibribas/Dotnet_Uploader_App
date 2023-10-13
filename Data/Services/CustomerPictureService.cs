using Uploader.Data.Models.Dtos;
using Uploader.Data.Models.Entities;

namespace Uploader.Data.Services;

public class CustomerPictureService
{
    public async Task<CreateUserNewFileDto> ChangePicture(CreateUserNewFileDto dto)
    {
        if (dto.File == null)
            throw new InvalidOperationException($"Nenhum arquivo enviado");

        try
        {
            string fileCreatedRelativePath = await Task.Run(() =>
            {
                return new CreateFileService(dto.File).StoreAt($"../_temp/")
                                                  .InDirectory($"{dto.UserId}")
                                                  .LimitStoreTo(1024 * 1024 / 2)
                                                  .Create();
            });
                    
            await Task.Run(() =>
            {
                Customer customer = new();
                customer.FilePath = fileCreatedRelativePath;
            });

            return new CreateUserNewFileDto()
            {
                Response = new Response()
                {
                    Status = ResponseStatus.SUCCESS
                }
            };
        }
        catch (Exception ex)
        {
            return new CreateUserNewFileDto()
            {
                Response = new Response()
                {
                    Status = ResponseStatus.ERROR,
                    Message = ex.Message
                }
            };
        }
    }
}