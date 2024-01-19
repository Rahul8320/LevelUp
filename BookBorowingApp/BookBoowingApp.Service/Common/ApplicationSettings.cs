namespace BookBoowingApp.Service.Common;

public class AppSettings
{
    private string _supportedGenreString = string.Empty;

    public List<string> SupportedGenres { get; init; }

    public string SupportedGenreString
    {
        get
        {
            return _supportedGenreString;
        }
        init
        {
            _supportedGenreString = value;
            SupportedGenres = [];

            if (value != null)
            {
                SupportedGenres = [.. _supportedGenreString.Split(',')];
            }
        }
    }

    public AppSettings()
    {
        SupportedGenres = [];
    }
}
