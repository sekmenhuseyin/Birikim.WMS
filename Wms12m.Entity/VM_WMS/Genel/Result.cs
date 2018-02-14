namespace Wms12m.Entity
{
    /// <summary>
    /// genel sonuç
    /// </summary>
    public class Result
    {
        public Result()
        {
            Status = false;
            Id = 0;
            Message = "";
        }

        public Result(bool status)
        {
            Status = status;
            Id = 0;
            Message = "";
        }

        public Result(bool status, string message)
        {
            Status = status;
            Message = message;
            Id = 0;
        }

        public Result(bool status, int id)
        {
            Status = status;
            Id = id;
            Message = "";
        }

        public Result(bool status, int id, string message)
        {
            Status = status;
            Id = id;
            Message = message;
        }

        public Result(bool status, int id, string message, object data)
        {
            Status = status;
            Id = id;
            Message = message;
            Data = data;
        }

        public object Data { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}