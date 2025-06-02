using System;

namespace UtilityFramework.Infra.Core.MongoDb.Data
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct | AttributeTargets.All)]
    public class MongoIndex : Attribute { }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ReferenceToAttribute(Type targetType) : Attribute
    {
        public Type TargetType { get; } = targetType;
    }
}