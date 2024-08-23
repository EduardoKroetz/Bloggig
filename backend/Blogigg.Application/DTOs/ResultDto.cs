namespace Bloggig.Application.DTOs;

public class ResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }

    public static ResultDto SuccessResult(object data, string message = "")
    {
        return new ResultDto { Data = data, Message = message, Success = true };
    }

    public static ResultDto BadResult(string error = "")
    {
        return new ResultDto { Data = null, Message = error, Success = false };
    }
}
