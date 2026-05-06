using Shiny;
using Shiny.Locations;
using System.Diagnostics;

namespace MauiApp4
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        readonly IGeofenceManager geofenceManager;

        public MainPage(IGeofenceManager injectedGeofenceManager)
        {
            geofenceManager = injectedGeofenceManager;

            InitializeComponent();
        }

        private async void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Geofence region started {count} time";
            else
                CounterBtn.Text = $"Geofence region started {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);

            // Prevent - A region with the identifier 'RegionArrival' already exists
            List<GeofenceRegion> geofenceRegions = [.. geofenceManager.GetMonitorRegions()];
            if (geofenceRegions is not null && !geofenceRegions.Any(x => x.Identifier == "RegionArrival"))
            {
                // Region arrival
                try
                {
                    // Random
                    GeofenceRegion geofenceRegionArrival = new("RegionArrival", new Position(30.30, 30.56), Distance.FromMeters(10))
                    {
                        SingleUse = false,
                        NotifyOnEntry = true,
                        NotifyOnExit = false,
                    };


                    // Add to monitor
                    await geofenceManager.StartMonitoring(geofenceRegionArrival);
                    Debug.WriteLine("RegionArrival monitor started");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Unable to add Region Arrival due to: "+ex.Message);
                }
            }
            else
            {
                Debug.WriteLine("RegionArrival monitor is already running");
            }
        }
    }
}
