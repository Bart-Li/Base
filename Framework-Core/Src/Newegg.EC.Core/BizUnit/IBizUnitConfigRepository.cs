namespace Newegg.EC.Core.BizUnit
{
    /// <summary>
    /// Biz unit config repository.
    /// </summary>
    public interface IBizUnitConfigRepository
    {
        /// <summary>
        /// Gets name.
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
        /// Gets region code.
        /// </summary>
        string RegionCode { get; }
    }
}
