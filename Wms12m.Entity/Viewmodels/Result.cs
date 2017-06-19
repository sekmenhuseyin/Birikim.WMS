namespace Wms12m.Entity
{
    /// <summary>
    /// genel sonuç
    /// </summary>
    public class Result
    {
        public Result()
        {
            new Result(false, 0, "", null);
        }
        public Result(bool status)
        {
            new Result(status, 0, "", null);
        }
        public Result(bool status, string message)
        {
            new Result(status, 0, message, null);
        }
        public Result(bool status, int id)
        {
            new Result(status, id, "", null);
        }
        public Result(bool status, int id, string message)
        {
            new Result(status, id, message, null);
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
