using System;

/// <summary>
/// Summary description for DbTabloAttribute
/// </summary>

[AttributeUsage(AttributeTargets.Class)]
public class DbTabloAttribute : Attribute
{
    string _dbAdi;
    string _semaAdi;
    string _tabloAdi;
    

    public string DBAdi
    {
        get { return _dbAdi; }
        set { _dbAdi = value; }
    }

    public string SemaAdi
    {
        get { return _semaAdi; }
        set { _semaAdi = value; }
    }
    
    public string TabloAdi
    {
        get { return _tabloAdi; }
        set { _tabloAdi = value; }
    }


	public DbTabloAttribute(string dbAdi, string semaAdi, string tabloAdi)
	{
        DBAdi = dbAdi;
        SemaAdi = semaAdi;
        TabloAdi = tabloAdi;
	}
}