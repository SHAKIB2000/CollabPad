namespace CollabPad.Web.Models
{
    public enum ResponseTypes
    {
        Success,
        Error
    }
    public class ResponseModel
    {
        public string? Message { get; set; }
        public ResponseTypes ResponseType { get; set;}
    }
}
