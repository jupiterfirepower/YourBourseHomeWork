namespace YB.Todo.Models
{
    public record WebError
    {
        public int ErrorCode { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
    }
}
