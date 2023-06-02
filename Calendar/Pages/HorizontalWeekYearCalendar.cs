using Microsoft.AspNetCore.Components;

namespace Calendar.Pages
{
    public partial class HorizontalWeekYearCalendar
    {


        /* variables and properties */

        // Popup variables for parameters
        private Day selectedDay;
        private Event EventDetail;
        private int SelectedResource;
        private DateTime DateSelected;
        private bool showPopup = false;
        private bool showDetails = false;
        private Event eve;

        public enum Option
        {
            Month,
            HorizontalMonth,
            HorizontalYear,
            HorizontalFullYear,
            HorizontalWeekYear,
        }

        //label for the month and year in the calendar header
        private string Year => _currentMonth.ToString("yyyy");

        //list of events
        [Parameter]
        public List<Event> Events { get; set; } = new List<Event>();

        //bool to switch to the next calendar component
        private bool nextView { get; set; } = false;

        //list of weeks for the calendar
        public List<Weeks> Month { get; set; }

        //represents the current month
        private DateTime _currentMonth;

        //list of months of the year for the calendar
        public List<string> monthsOfYear { get; set; } = new List<string>();

        private bool hide { get; set; } = false;

        private int calendarSelected;

        //variable to check the string for the current month
        private string thisMonth => _currentMonth.ToString("MMMM");

        //lista de recursos
        [Parameter]
        public List<ResourceData> ResourceData { get; set; } = new List<ResourceData>();

        /* methods */

        protected override void OnInitialized()
        {

            //initializing variables for the calendar the events and the calendar
            monthsOfYear = Enumerable.Range(1, 12)
            .Select(month => new DateTime(2000, month, 1).ToString("MMMM"))
            .ToList();

            //create empty event to prevent nullreferenceexpetion
            EventDetail = new Event
            {
                Title = "",
                EventResource = 0,
                Description = "",
                StartDate = new DateTime(2022, 4, 1),
            };

            //initializing variables for the calendar the events and the calendar
            DateSelected = DateTime.Today;
            _currentMonth = DateTime.Today;
            //InitializeEvents();
            GenerateCalendar();

        }

        //method to create the calendar (days and weeks)
        private void GenerateCalendar()
        {
            //prevent popups from appearing
            showDetails = false;
            showPopup = false;
            //initialize variables

            Month = new List<Weeks>();
            for (int month = 1; month <= monthsOfYear.Count; month++)
            {
                
                DateTime firstDayOfMonth = new DateTime(int.Parse(Year), month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                var WeekList = new Weeks();
                for (int week = 1; week <= 5; week++)
                {
                    
                    DateTime firstDayOfWeek = firstDayOfMonth.AddDays((week - 1) * 7);
                    DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

                    if (firstDayOfWeek.Month == month || lastDayOfWeek.Month == month)
                    {
                        Week w = new Week
                        {
                            WeekNumber = week
                        };
                        WeekList.WeeksList.Add(w);
                    }
                    
                }
                Month.Add(WeekList);
            }


        }

        //method to skip to the next year
        private void NextYear()
        {
            _currentMonth = _currentMonth.AddYears(1);
            GenerateCalendar();
            showDetails = false;
            showPopup = false;
        }
        //method to skip to the previous year
        private void PreviousYear()
        {
            _currentMonth = _currentMonth.AddYears(-1);
            GenerateCalendar();
            showDetails = false;
            showPopup = false;
        }

        // Method to show the popup with the current day
        private void DayClicked(int resourse, int monthNumber, int dayNumber, int weekNumber)
        {
            SelectedResource = resourse;

            var ClickedDay = new DateTime(_currentMonth.Year, monthNumber, dayNumber);

            DateSelected = ClickedDay;
            showDetails = false;
            showPopup = true;
        }
        [Parameter]
        public Action<Event> EventClick { get; set; }
        //method to show the details of the event clicked
        private void EventDetails(Event ev)
        {
            //EventDetail = ev;
            //showPopup = false;
            //showDetails = true;
            //StateHasChanged();
            EventClick.Invoke(ev);
        }

        private void CreateView(int ca)
        {
            hide = true;
            calendarSelected = ca;
            StateHasChanged();
        }

        //method to create a new event (not working)
        private void HandleEventAdded(string eventName, DateTime eventStartDate, DateTime eventEndDate, string eventDescription, Day day, int Resource)
        {
            var event1 = new Event
            {
                Title = eventName,
                StartDate = eventStartDate,
                EndDate = eventEndDate,
                Description = eventDescription,
                SelectedDay = day,
                EventResource = Resource,
            };
            Events.Add(event1);
            showPopup = false;
            StateHasChanged();
        }

        //initializing the events of the calendar
        private void InitializeEvents()
        {

            var event1 = new Event
            {
                Title = "Exemplo de Evento",
                StartDate = new DateTime(2023, DateTime.Now.Month, DateTime.Now.Day),
                Description = "Isto representa um evento criado automaticamente pelo programa",
                SelectedDay = new Day { Class = "day2", Date = new DateTime(2023, DateTime.Now.Month, DateTime.Now.Day), DayNumber = DateTime.Now.Day, IsOtherMonth = false },
                EventResource = 2,
            };

            Events.Add(event1);

        }
    }
}
