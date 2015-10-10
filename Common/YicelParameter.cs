using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JinTong.Jyrj.Common
{
    public class YicelParameter
    {
        //参数名称
        private string _name = null;
        //数据类型
        private DbType _dbType = DbType.String;

        //SqlServer参数数据类型
        private SqlDbType _sqlDbType = SqlDbType.VarChar;
        //数据库类型
        private DataBaseType _dataBaseType = DataBaseType.SqlServer;
        //参数值
        private object _value = null;
        //参数类型(例如:输入,输出)
        private ParameterDirection _direction = ParameterDirection.Input;
        //参数长度
        private int _size = 0;
        ////参数前缀
        //private string _prefix = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="sqlDbType">SqlServer参数类型</param>
        /// <param name="oracleType">Oracle参数类型</param>
        /// <param name="_direction">参数类型（输入、输出、输入输出)</param>
        /// <param name="value">参数值</param>
        /// <param name="size">参数长度</param>
        /// <param name="dataBaseType">数据库类型</param>
        public YicelParameter(string name, DbType dbType, SqlDbType sqlDbType,
            ParameterDirection direction, object value, int size, DataBaseType dataBaseType)
        {
            _name = name;
            _dbType = dbType;
            _sqlDbType = sqlDbType;
            _direction = direction;
            _value = value;
            _size = size;
            _dataBaseType = dataBaseType;
        }
        /// <summary>
        /// DbType数据类型参数构造函数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">参数值</param>
        public YicelParameter(string name, DbType dbType, object value)
            : this(name, dbType, SqlDbType.VarChar,
            ParameterDirection.Input, value, 1000, DataBaseType.Unknown)
        { }
        /// <summary>
        /// Oracle数据类型参数构造函数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="oType">参数类型</param>
        /// <param name="value">参数值</param>
        public YicelParameter(string name, object value)
            : this(name, DbType.String, SqlDbType.VarChar, ParameterDirection.Input, value, 1000, DataBaseType.Oracle) { }
        /// <summary>
        /// Oracle数据类型参数构造函数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="oType">参数类型</param>
        /// <param name="value">参数值</param>
        /// <param name="length">参数长度</param>
        public YicelParameter(string name, object value, int length)
            : this(name, DbType.String, SqlDbType.VarChar, ParameterDirection.Input, value, length, DataBaseType.Oracle) { }
        /// <summary>
        /// SqlServer数据库类型参数构造函数
        /// </summary>
        /// <param name="namePrefix">参数前缀名</param>
        /// <param name="name">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="value">参数值</param>
        public YicelParameter(string name, System.Data.SqlDbType sqlDbType, object value)
            : this(name, DbType.String, sqlDbType, ParameterDirection.Input, value, 1000, DataBaseType.SqlServer) { }
        /// <summary>
        /// SqlServer数据库类型参数构造函数
        /// </summary>
        /// <param name="namePrefix">参数前缀名</param>
        /// <param name="name">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="value">参数值</param>
        /// <param name="length">参数长度</param>
        public YicelParameter(string name, System.Data.SqlDbType sqlDbType, object value, int length)
            : this(name, DbType.String, sqlDbType, ParameterDirection.Input, value, length, DataBaseType.SqlServer) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="direction">参数方向</param>
        /// <param name="size">数据最大宽度</param>
        public YicelParameter(string name, DbType dbType, ParameterDirection direction, int size)
            : this(name, dbType, SqlDbType.VarChar, direction, null, size, DataBaseType.Unknown) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="oType">数据类型</param>
        /// <param name="direction">参数方向</param>
        /// <param name="size">数据最大宽度</param>
        public YicelParameter(string name, ParameterDirection direction, int size)
            : this(name, DbType.String, SqlDbType.VarChar, direction, null, size, DataBaseType.Oracle) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="sqlDbType">数据类型</param>
        /// <param name="direction">参数方向</param>
        /// <param name="size">数据最大宽度</param>
        public YicelParameter(string name, SqlDbType sqlDbType, ParameterDirection direction, int size)
            : this(name, DbType.String, sqlDbType, direction, null, size, DataBaseType.SqlServer) { }

        /// <summary>
        /// 参数长度(用于输出参数)
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// SqlServer类型
        /// </summary>
        public SqlDbType SqlDbType
        {
            get { return _sqlDbType; }
            set { _sqlDbType = value; }
        }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 参数类型(例如:输入,输出)
        /// </summary>
        public ParameterDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// 参数值(如果是输出参数则数据操作后为输出值)
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DataBaseType
        {
            get { return _dataBaseType; }
            set { _dataBaseType = value; }
        }

    }

    /// <summary>
    /// 数据库类型枚举。
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// 甲骨文公司的Oracle数据库。
        /// </summary>
        Oracle,
        /// <summary>
        /// 微软的SqlServer数据库。
        /// </summary>
        SqlServer,
        /// <summary>
        /// 开源的MySql数据库。
        /// </summary>
        MySql,
        /// <summary>
        /// 未知数据库
        /// </summary>
        Unknown
    }
}
