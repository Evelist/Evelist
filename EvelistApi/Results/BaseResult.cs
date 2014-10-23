namespace EvelistApi.Results
{
    public class BaseResult
    {
        public string message { get; set; }
        public bool IsSuccessed { get { return message == "OK"; } }
    }
}
