using System;

namespace CWB.CommonUtils.Common
{
    public static class CWBAppUtils
    {
        public static string EncodeLong(long Id)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Id.ToString());
            var base64String = Convert.ToBase64String(plainTextBytes);
            return Uri.EscapeDataString(base64String);
        }
        public static long DecodeString(string encodedString)
        {
            var decodedString = Uri.UnescapeDataString(encodedString);
            var plantTextbytes = Convert.FromBase64String(decodedString);
            return long.Parse(System.Text.Encoding.UTF8.GetString(plantTextbytes));
        }
    }
}
