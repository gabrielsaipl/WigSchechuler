using Microsoft.AspNetCore.Components;

namespace Calendar.Pages
{
    public partial class HorizontalMonthCalendar
    {
        /* variables and properties */

        // Popup variables for parameters
        private Day DaySelected;
        private Event EventDetail;
        private DateTime DateSelected;
        private ResourceData SelectedResource;
        private bool showPopup = false;
        private bool showDetails = false;

        //label for the month and year in the calendar header
        private string MonthYear => _currentMonth.ToString("MMMM yyyy");

        //list of events
        [Parameter]
        public List<Event> Events { get; set; } = new List<Event>();

        //button to switch to the next calendar component
        private bool nextView { get; set; } = false;
        //list of days for the calendar
        private List<Day> Days { get; set; }
        //represents the current month
        private DateTime _currentMonth;

        private bool hide { get; set; } = false;

        private int calendarSelected;

        //lista de recursos
        [Parameter]
        public List<ResourceData> ResourceData { get; set; } = new List<ResourceData>();

        /* methods */

        protected override void OnInitialized()
        {
            //create empty event to prevent nullreferenceexpetion
            EventDetail = new Event
            {
                Title = "",
                EventResource = 0,
                Description = "",
                StartDate = DateTime.Now,
            };

            //initializing variables for the calendar the events and the calendar
            _currentMonth = DateTime.Today;
            //InitializeEvents();
            GenerateCalendar();
        }

        

        //method to create the calendar (days)
        private void GenerateCalendar()
        {
            showPopup = false;
            showDetails = false;
            Days = new List<Day>();
            var firstDayOfMonth = new DateTime(_currentMonth.Year, _currentMonth.Month, 1);
            var daysInMonth = DateTime.DaysInMonth(_currentMonth.Year, _currentMonth.Month);
            var currentDay = 1;
            // Add days of current month
            while (currentDay <= daysInMonth)
            {
                var day = new Day
                {
                    Date = new DateTime(_currentMonth.Year, _currentMonth.Month, currentDay),
                    DayNumber = currentDay,
                    Class = "day"
                };
                Days.Add(day);

                currentDay++;
                firstDayOfMonth = firstDayOfMonth.AddDays(1);
            }
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
        private void DayClicked(ResourceData resourse, int dayNumber)
        {
            SelectedResource = resourse;

            var ClickedDay = new DateTime(_currentMonth.Year, _currentMonth.Month, dayNumber);

            foreach (var day in Days)
            {
                if (day.Date == ClickedDay)
                {
                    DaySelected = day;
                }
            }
            DateSelected = ClickedDay;
            showDetails = false;
            showPopup = true;
            StateHasChanged();
        }

        private void CreateView(int ca)
        {
            calendarSelected = ca;
            hide = true;
            StateHasChanged();
        }

        //method to create a new event
        private void HandleEventAdded(string eventName, DateTime eventStartDate, DateTime eventEndDate, string eventDescription, Day day, ResourceData Resource)
        {
            var event1 = new Event
            {
                Title = eventName,
                StartDate = eventStartDate,
                EndDate = eventEndDate,
                Description = eventDescription,
                SelectedDay = day,
                EventResource = Resource.Id,
            };
            eventListService.EventList.Add(event1);
            if (eventListService.EventList.Any())
            {
                foreach (var eve in eventListService.EventList)
                {
                    if (Events.Contains(eve))
                    {

                    }
                    else
                    {
                        Events.Add(eve);
                    }

                }
            }
            showPopup = false;
            StateHasChanged();
        }
        [Parameter]
        public Action<Event> EventClick { get; set; }
        //method to show the details of the event clicked
        private void EventDetails(Event ev)
        {
            //EventDetail = ev;
            showPopup = false;
            //showDetails = true;
            //StateHasChanged();
            EventClick.Invoke(ev);
        }
        //initializing the events of the calendar
        private void InitializeEvents()
        {

            var event1 = new Event
            {
                Title = "evento1",
                StartDate = new DateTime(2023, 4, 1),
                EndDate = new DateTime(2023, 4, 16),
                Description = "Descricao do evento 1",
                SelectedDay = new Day { Class = "day2", Date = new DateTime(2023, 4, 1), DayNumber = 1, IsOtherMonth = false },
                EventResource = 1,
            };
            Day randomDay = new Day
            {
                Date = new DateTime(_currentMonth.Year, 4, 3),
                DayNumber = 3,
                Class = "day  ",
            };
            var event2 = new Event
            {
                Title = "Exemplo de Evento",
                StartDate = new DateTime(2023, 4, 3),
                EndDate = new DateTime(2023, 4, 9),
                Description = "Isto representa um evento criado automaticamente pelo programa",
                SelectedDay = randomDay,
                EventResource = 2,
            };
            var event3 = new Event
            {
                Title = "Exemplo de Evento",
                StartDate = new DateTime(2023, 4, 4),
                EndDate = new DateTime(2023, 4, 14),
                Description = "Isto representa um evento criado automaticamente pelo programa",
                SelectedDay = new Day { Class = "day2", Date = new DateTime(2023, 4, 4), DayNumber = 4, IsOtherMonth = false },
                EventResource = 2,
            };
            var event4 = new Event
            {
                Title = "evento3",
                StartDate = new DateTime(2023, 5, 17),
                EndDate = new DateTime(2023, 5, 23),
                Description = "Descricao do evento 3",
                SelectedDay = new Day { Class = "day2", Date = new DateTime(2023, 5, 17), DayNumber = 17, IsOtherMonth = false },
                EventResource = 7,
            };
            var Evess = new List<Event> { event1, event2, event3, event4 };
            foreach (var evess in Evess)
            {
                Events.Add(evess);
            }

        }
    }
}
