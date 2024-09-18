namespace aws_secrets_manager.Model
{
    public class BaseResponse
    {
        public int Code { get; set; } = 200;
        public string Status { get; set; } = "Success";
        public string Data { get; set; }
    }
}
