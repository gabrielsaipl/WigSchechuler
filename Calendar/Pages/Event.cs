using Microsoft.Extensions.Logging;

namespace Calendar.Pages
{
    public class Event
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public string Title { get; set; }
        public string Description { get; set; }
        public Day SelectedDay { get; set; }
        public int EventResource { get; set; }

    }
    
}
