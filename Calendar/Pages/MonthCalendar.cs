using Microsoft.AspNetCore.Components;

namespace Calendar.Pages
{
    public partial class MonthCalendar
    {
        
        /* variables and properties */

        // Popup variables for parameters
        private Day selectedDay;
        private Event EventDetail;
        private DateTime DateSelected;
        private bool showPopup = false;
        private bool showDetails = false;
        public enum Option
        {
            Month,
            HorizontalMonth,
            HorizontalYear,
            HorizontalFullYear,
            HorizontalWeekYear,
        }

        //label for the month and year in the calendar header
        public string MonthYear => _currentMonth.ToString("MMMM yyyy");

        [Parameter]
        public RenderFragment<bool> Buttons { get; set; }
        //list of events
        [Parameter]
        public List<Event> Events { get; set; } = new List<Event>();

        [Parameter]
        public List<ResourceData> ResourceData { get; set; } = new List<ResourceData>();

        //bool to switch to the next calendar component
        private bool nextView { get; set; } = false;

        //list of weeks for the calendar
        public List<Week> Weeks { get; set; }

        //represents the current month
        private DateTime _currentMonth;

        public bool hide { get; set; } = false;

        private int calendarSelected;

        /* methods */

        protected override void OnInitialized()
        {
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
            Weeks = new List<Week>();
            var firstDayOfMonth = new DateTime(_currentMonth.Year, _currentMonth.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(_currentMonth.Year, _currentMonth.Month);
            var currentDay = 1;

            // Add empty days to first week until start day is reached
            var week = new Week();
            for (int i = 0; i < (int)firstDayOfMonth.DayOfWeek; i++)
            {
                var prevMonthDays = DateTime.DaysInMonth(_currentMonth.AddMonths(-1).Year, _currentMonth.AddMonths(-1).Month);
                var day = new Day
                {
                    DayNumber = prevMonthDays - ((int)firstDayOfMonth.DayOfWeek - i - 1),
                    Date = new DateTime(_currentMonth.AddMonths(-1).Year, _currentMonth.AddMonths(-1).Month, prevMonthDays - ((int)firstDayOfMonth.DayOfWeek - i - 1)),
                    Class = "prev-month",
                    IsOtherMonth = true,
                };
                week.Days.Add(day);
            }

            // Add days of current month
            while (currentDay <= daysInMonth)
            {
                if (week.Days.Count == 7)
                {
                    Weeks.Add(week);
                    week = new Week();
                }
                var day = new Day
                {
                    Date = new DateTime(_currentMonth.Year, _currentMonth.Month, currentDay),
                    DayNumber = currentDay,
                    Class = firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday ? "sunday" : "",
                    IsOtherMonth = false,
                };
                week.Days.Add(day);

                currentDay++;
                firstDayOfMonth = firstDayOfMonth.AddDays(1);
            }
            int nextDays = 1;
            // Add empty days to last week until it has 7 days
            while (week.Days.Count < 7)
            {
                var nextMonthDays = DateTime.DaysInMonth(_currentMonth.AddMonths(1).Year, _currentMonth.AddMonths(1).Month);
                var day = new Day
                {
                    DayNumber = nextDays,
                    Class = "next-month",
                    Date = new DateTime(_currentMonth.AddMonths(1).Year, _currentMonth.AddMonths(1).Month, nextDays),
                    IsOtherMonth = true,
                };
                week.Days.Add(day);
                nextDays++;
            }
            Weeks.Add(week);
        }

        //method to skip to the next month
        private void NextMonth()
        {
            _currentMonth = _currentMonth.AddMonths(1);
            GenerateCalendar();
            showDetails = false;
            showPopup = false;
        }
        //method to skip to the previous month
        private void PreviousMonth()
        {
            _currentMonth = _currentMonth.AddMonths(-1);
            GenerateCalendar();
            showDetails = false;
            showPopup = false;
        }

        // Method to show the popup with the current day
        private void DayClicked(Day day)
        {
            //skips months if the day clicked isn't from the current month
            if (day.Class == "prev-month")
            {
                _currentMonth = _currentMonth.AddMonths(-1);
                GenerateCalendar();
                StateHasChanged();
            }
            else if (day.Class == "next-month")
            {
                _currentMonth = _currentMonth.AddMonths(1);
                GenerateCalendar();
                StateHasChanged();
            }
            else
            {
                DateSelected = new DateTime(_currentMonth.Year, _currentMonth.Month, day.DayNumber);
                selectedDay = day;
                showDetails = false;
                showPopup = true;
                StateHasChanged();
            }
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
                StartDate = new DateTime(2023, 4, 30),
                EndDate = new DateTime(2023, 5, 2),
                Description = "Isto representa um evento criado automaticamente pelo programa",
                SelectedDay = new Day { Class = "day2", Date = new DateTime(2023, 5, 1), DayNumber = 1, IsOtherMonth = false },
                EventResource = 2,
            };

                Events.Add(event1);

        }
    }
}
