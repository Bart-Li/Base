namespace Newegg.EC.Core.RestClient
{
    /// <summary>
    /// Method Type.
    /// </summary>
    public enum Method
    {
        GET,
        POST,
        PUT,
        DELETE,
        HEAD,
        OPTIONS,
        PATCH,
    }

    /// <summary>
    /// Parameter type.
    /// </summary>
    public enum ParameterType
    {
        Post,
        Cookie,
        HttpHeader,
        QueryString,        
    }

    /// <summary>
    /// Data formats
    /// </summary>
    public enum DataFormat
    {
        Json,
        Xml
    }

    /// <summary>
    /// Content type.
    /// </summary>
    public class ContentType
    {
        public const string Json = "application/json";
        public const string Xml = "application/xml";
        public const string Jsv = "application/jsv";
        public const string Form = "application/x-www-form-urlencoded";
    }
}
