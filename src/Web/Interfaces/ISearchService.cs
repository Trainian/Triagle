namespace Web.Interfaces
{
    public interface ISearchService
    {
        Task<IDictionary<string, string>> GetElementsByString(string fstring);
    }
}
