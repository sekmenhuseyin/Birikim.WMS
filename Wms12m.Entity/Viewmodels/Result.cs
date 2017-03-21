using System.Collections.Generic;

namespace Wms12m.Entity
{
    /// <summary>
    /// genel sonuç
    /// </summary>
    public class Result
    {
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
    /// <summary>
    /// datatables için bir kaç form
    /// </summary>
    public class DataTableParameters
    {
        public List<DataTableColumn> Columns { get; set; }
        public int Draw { get; set; }
        public int Length { get; set; }
        public List<DataOrder> Order { get; set; }
        public Search Search { get; set; }
        public int Start { get; set; }
    }
    public class Search
    {
        public bool Regex { get; set; }
        public string Value { get; set; }
    }
    public class DataTableColumn
    {
        public int Data { get; set; }
        public string Name { get; set; }
        public bool Orderable { get; set; }
        public bool Searchable { get; set; }
        public Search Search { get; set; }
    }
    public class DataOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}
