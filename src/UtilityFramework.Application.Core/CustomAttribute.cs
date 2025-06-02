using System;
using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Application.Core
{

    [AttributeUsage(AttributeTargets.All)]
    public class Column : Attribute
    {
        public int ColumnIndex { get; set; }

        public Column(int column)
        {
            ColumnIndex = column;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class IsReadOnly : Attribute
    {
        public IsReadOnly() { }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class IsNotNull : Attribute
    {
        public IsNotNull() { }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class DataPropertie : Attribute
    {

        public string PropertieName { get; set; }

        public DataPropertie(string propertieName)
        {
            PropertieName = propertieName;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class DropDownExcel : Attribute
    {
        public Type Options { get; set; }
        public bool AllowBlank { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class DateOfBirthAttribute : RangeAttribute
    {
        public DateOfBirthAttribute()
          : base(typeof(DateTime), new DateTime(DateTime.Now.AddYears(-130).Year, 1, 1).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy")) { }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class IsClass : Attribute
    {
        public IsClass() { }
    }


}
