namespace WebTimer.Models.ViewModels
{
    public class HomeViewModel
    {
        public int Status { get; set; }
        public Dictionary<string, TimeSpan> TimesByCategories { get; set; }
        public Record? OpenRecord { get; set; }

        public HomeViewModel() 
        {
            TimesByCategories = new Dictionary<string, TimeSpan>();
        }
    }
}
