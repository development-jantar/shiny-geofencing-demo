using Shiny.Locations;

namespace MauiApp4;

public class MyGeofenceDelegate : IGeofenceDelegate
{
    public Task OnStatusChanged(GeofenceState newState, GeofenceRegion region)
    {
        // handle enter/exit
        return Task.CompletedTask;
    }
}
