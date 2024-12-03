namespace BankingSolution.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string? StatusMessage { get; set; }

        public object? Result { get; set; }
    }
}
