namespace Wms12m.Entity
{
    /// <summary>
    /// genel sonuç
    /// </summary>
    public class Result
    {
        public Result()
        {
            Status = true;
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
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    /// <summary>
    /// json result
    /// </summary>
    public class frmJson
    {
        public string id { get; set; }
        public string value { get; set; }
        public string label { get; set; }
    }
    /// <summary>
    /// json result birim
    /// </summary>
    public class frmBirims
    {
        public string Birim1 { get; set; }
        public string Birim2 { get; set; }
        public string Birim3 { get; set; }
        public string Birim4 { get; set; }
    }
}
