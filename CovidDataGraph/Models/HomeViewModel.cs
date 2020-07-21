namespace CovidDataGraph.Models
{
    public class HomeViewModel
    {
        public HomeViewModel(string location)
        {
            Location = location;
        }
        public string Location { get; private set; }
    }
}
