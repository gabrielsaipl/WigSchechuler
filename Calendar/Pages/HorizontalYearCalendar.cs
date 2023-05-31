using Microsoft.AspNetCore.Components;

namespace Calendar.Pages
{
    public partial class HorizontalYearCalendar
    {
        /* variables and properties */

        // Popup variables for parameters
        private Day DaySelected;
        private Event EventDetail;
        private DateTime DateSelected;
        private int SelectedResource;
        private bool showPopup = false;
        private bool showDetails = false;

        //label for the year in the calendar header
        private string Year => _currentMonth.ToString("yyyy");

        //variable to check the string for the current month
        private string thisMonth => _currentMonth.ToString("MMMM");

        //list of events
        [Parameter]
        public List<Event> Events { get; set; } = new List<Event>();

        //button to switch to the next calendar component
        private bool nextView { get; set; } = false;

        //list of months of the year for the calendar
        public List<string> monthsOfYear { get; set; } = new List<string>();

        //list of days for the calendar
        private List<Day> Days { get; set; }

        //represents the current month
        private DateTime _currentMonth;

        private bool hide { get; set; } = false;

        private int calendarSelected;

        //lista de recursos
        [Parameter]
        public List<ResourceData> ResourceData { get; set; } = new List<ResourceData>
        {
            new ResourceData{Text = "Resource", Id = 1},
            new ResourceData{Text = "Resource", Id = 2},
            new ResourceData{Text = "Resource", Id = 3},
            new ResourceData{Text = "Resource", Id = 4},
            new ResourceData{Text = "Resource", Id = 5},
            new ResourceData{Text = "Resource", Id = 6},
            new ResourceData{Text = "Resource", Id = 7},
            new ResourceData{Text = "Resource", Id = 8},
        };

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
            monthsOfYear = Enumerable.Range(1, 12)
            .Select(month => new DateTime(2000, month, 1).ToString("MMMM"))
            .ToList();
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
                    Class = "day2"
                };
                Days.Add(day);

                currentDay++;
                firstDayOfMonth = firstDayOfMonth.AddDays(1);
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

        private void CreateView(int ca)
        {
            hide = true;
            calendarSelected = ca;
            StateHasChanged();
        }

        // Method to show the popup with the current day
        private void DayClicked(int resourse, int monthNumber)
        {
            SelectedResource = resourse;

            var ClickedDay = new DateTime(_currentMonth.Year, monthNumber, 1);

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
        //method to create a new event (not working)
        private void HandleEventAdded(string eventName, DateTime eventDate, string eventDescription, Day day, int Resource)
        {

            showPopup = false;
            StateHasChanged();
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

        //initializing the events of the calendar
        private void InitializeEvents()
        {

            var event1 = new Event
            {
                Title = "evento1",
                StartDate = new DateTime(2023, 4, 1),
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
                Description = "Isto representa um evento criado automaticamente pelo programa",
                SelectedDay = randomDay,
                EventResource = 2,
            };
            var event3 = new Event
            {
                Title = "evento3",
                StartDate = new DateTime(2023, 5, 17),
                Description = "Descricao do evento 3",
                SelectedDay = new Day { Class = "day2", Date = new DateTime(2023, 5, 17), DayNumber = 17, IsOtherMonth = false },
                EventResource = 7,
            };
            var Evess = new List<Event> { event1, event2, event3 };
            foreach (var evess in Evess)
            {
                Events.Add(evess);
            }

        }
    }
}
