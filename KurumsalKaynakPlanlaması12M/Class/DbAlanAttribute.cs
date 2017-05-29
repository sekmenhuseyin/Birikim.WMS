using System;
using System.Data;

/// <summary>
/// Veritabanı tabloların sütun alan bilgileri içerir
/// </summary>

[AttributeUsage(AttributeTargets.Property)]
public class DbAlanAttribute : Attribute
{
    string _alanAdi;
    SqlDbType _dbTye;
    int _maxlength;
    bool _identtiy;
    bool _primary;
    bool _updatable;


    public bool Updatable
    {
        get { return _updatable; }
        set { _updatable = value; }
    }

    public bool Primary
    {
        get { return _primary; }
        set { _primary = value; }
    }

    public int MaxLength
    {
        get { return _maxlength; }
        set { _maxlength = value; }
    }


    public bool Identtiy
    {
        get { return _identtiy; }
        set { _identtiy = value; }
    }

    public SqlDbType DbTye
    {
        get { return _dbTye; }
        set { _dbTye = value; }
    }

  
    public string AlanAdi
    {
        get { return _alanAdi; }
        set { _alanAdi = value; }
    }


    public DbAlanAttribute(string alanAdi, SqlDbType dbType, int maxlength, bool identity, bool primary, bool updatable)
    {
        AlanAdi = alanAdi;
        DbTye = dbType;
        MaxLength = maxlength;
        Identtiy = identity;
        Primary = primary;
        Updatable = updatable;
    }

    public DbAlanAttribute(string alanAdi, SqlDbType dbType, int maxlength, bool identity, bool primary)
        : this(alanAdi, dbType, maxlength, identity, primary, true)
    {

    }

    public DbAlanAttribute(string alanAdi, SqlDbType dbType, int maxlength, bool identity)
        : this(alanAdi, dbType, maxlength, identity, false)
    {

    }

    public DbAlanAttribute(string alanAdi, SqlDbType dbType, int maxlength)
        : this(alanAdi, dbType, maxlength, false)
    {

    }

	public DbAlanAttribute()
	{
	
	}

}