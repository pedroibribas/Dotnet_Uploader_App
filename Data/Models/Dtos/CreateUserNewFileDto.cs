using Microsoft.AspNetCore.Components.Forms;

namespace Uploader.Data.Models.Dtos;

public class CreateUserNewFileDto
{
    public string? UserId { get; set; }
    public IBrowserFile? File { get; set; }
    public Response? Response { get; set; }
}

public class Response
{
    public ResponseStatus? Status { get; set; }
    public string? Message { get; set; }
}

public enum ResponseStatus
{
    SUCCESS, ERROR
}