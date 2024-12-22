namespace HeatExchangeApp.Models
{
    public class HomeViewModel: InitData
    {
        public List<Results> Results { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
