using Microsoft.JSInterop;

namespace Calendar.Pages
{
    public partial class Index

    {
        int year = DateTime.Today.Year;
        int month = DateTime.Today.Month;
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {  

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("createCalendar", month - 1, year);
                
            }            
        }
    }
}
