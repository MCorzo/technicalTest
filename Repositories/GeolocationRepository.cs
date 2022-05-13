using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Responses;

public interface IGeollocationRepository
{
    IpGeolocation GetIpGeolocation(string ipAddress);
    IEnumerable<IpGeolocation> GetIpsGeolocation(IEnumerable<string> ipAdresses);
}

public class GeollocationRepository : IGeollocationRepository
{
    private IConfiguration _configuration;
    public GeollocationRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IpGeolocation GetIpGeolocation(string ipAddress)
    {
        var result = new IpGeolocation();

        using (var reader = new DatabaseReader(_configuration.GetValue<string>("DatabasePath")))
        {
            CountryResponse? dbResponse;

            result.ipAddress = ipAddress;

            try
            {
                var dbResult = reader.TryCountry(ipAddress, out dbResponse);

                result.located = dbResult;
                result.continent = dbResult ? dbResponse.Continent.Name : "Not located";
                result.countryName = dbResult ? dbResponse.Country.Name : "Not located";
                result.isoCode = dbResult ? dbResponse.Country.IsoCode : string.Empty;
            }
            catch (System.Exception)
            {
                result.located = false;
            }
            finally
            {
                reader.Dispose();
            }
        }

        return result;
    }

    public IEnumerable<IpGeolocation> GetIpsGeolocation(IEnumerable<string> ipAdresses)
    {
        var result = new List<IpGeolocation>();

        ipAdresses.ToList().ForEach(ipAddress => result.Add(GetIpGeolocation(ipAddress)));

        return result;
    }

}