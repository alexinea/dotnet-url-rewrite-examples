namespace UrlRewrite.AspNet.HttpMoudle
{
    public static class UrlRewriteExtension
    {
        public static string SubString(this string str, int startIndex, int length = 0)
        {
            if (startIndex < 0)
            {
                return str;
            }
            if (str.Length <= startIndex)
            {
                return string.Empty;
            }
            if (length < 1)
            {
                return str.Substring(startIndex);
            }
            return str.Length < startIndex + length
                ? str.Substring(startIndex)
                : str.Substring(startIndex, length);
        }
    }
}