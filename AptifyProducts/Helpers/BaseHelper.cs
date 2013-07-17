
 namespace AptifyWebApi.Helpers
{
    public static class BaseHelper
    {
        public static bool IsNull<T>(this T obj)
        {
            return Equals(obj, default(T));
        }

        public static bool IsNotNull<T>(this T obj)
        {
            return !obj.IsNull();
        }

        public static T NullableToNon<T>(this T? obj) where T : struct
        {
            return obj.HasValue ? (T) obj : default(T);
        }
    }
}