using System.ComponentModel;
using System.Runtime.Serialization;

namespace UtilityFramework.Services.Core3.Models
{
    public enum TypeSituation
    {
        [EnumMember(Value = "OK")]
        [Description("OK")]
        OK,
        [EnumMember(Value = "RECEBIDA")]
        [Description("RECEBIDA")]
        Received,
        [EnumMember(Value = "ENVIADA")]
        [Description("ENVIADA")]
        Sended,
        [EnumMember(Value = "ERRO")]
        [Description("ERRO")]
        Error,
        [EnumMember(Value = "FILA")]
        [Description("FILA")]
        Queue,
        [EnumMember(Value = "CANCELADA")]
        [Description("CANCELADA")]
        Canceled,
        [EnumMember(Value = "BLACK LIST")]
        [Description("BLACK LIST")]
        BlackList
    }
}