using System.Web;

namespace Newegg.EC.Core.RestClient.Impl
{
    public class Parameter
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public ParameterType Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0}={1}", this.Name, HttpUtility.UrlEncode(this.Value.ToString()));
        }
    }
}
