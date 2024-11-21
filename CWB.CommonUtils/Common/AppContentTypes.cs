namespace CWB.CommonUtils.Common
{
    public static class AppContentTypes
    {
        public const string ContentType = "application/json";

        public static string[] AdditionalContentType()
        {
            return new string[] { "text/json", "application/xml", "application/*+json" };
        }


    }
}
