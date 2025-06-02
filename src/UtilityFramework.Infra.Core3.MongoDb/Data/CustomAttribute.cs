using System;

namespace UtilityFramework.Infra.Core3.MongoDb.Data
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct | AttributeTargets.All)]
    public class MongoIndex : Attribute { }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ReferenceToAttribute : Attribute
    {
        public Type TargetType { get; }

        public ReferenceToAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }
}