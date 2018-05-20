namespace Newegg.EC.Core.BizUnit
{
    public interface IBizUnit
    {
        /// <summary>
        /// Gets biz unit name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets country code.
        /// </summary>
        string CountryCode { get; }

        /// <summary>
        /// Gets company code.
        /// </summary>
        int CompanyCode { get; }

        /// <summary>
        /// Gets language code.
        /// </summary>
        string LanguageCode { get; }

        /// <summary>
        /// Gets country code.
        /// </summary>
        string RegionCode { get; }        
    }
}
