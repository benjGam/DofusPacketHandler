using System;

namespace DofusPacketManager.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static Type GetOldestParent(this Type toCheck) {
            Type toReturn = toCheck;
            while (true)
            {
                if (toReturn.BaseType == null || toReturn.BaseType == typeof(object)) return toReturn;
                toReturn = toReturn.BaseType;
            }
        }
    }
}
