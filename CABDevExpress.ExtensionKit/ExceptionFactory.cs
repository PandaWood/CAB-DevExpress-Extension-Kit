using System;

namespace CABDevExpress
{
    internal static class ExceptionFactory
    {
        public static Exception CreateInvalidAdapterElementType(Type elementType, Type factoryType)
        {
            return new ArgumentException(
                string.Format("The specified uielement type '{0}' is not a valid for the '{1}'.",
                              elementType.FullName, factoryType.Name)
                );
        }

        public static Exception CreateInvalidElementHost(Type host)
        {
            return new ArgumentException(host.Name + " cannot host uielements.");
        }
    }
}