namespace AzureFunctionDemo.Models
{
    public class CommonResponse<T>
    {
        public int Code { get; set; }
        public string Error { get; set; }
        public string Payload { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
