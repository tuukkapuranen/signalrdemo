using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace SignalRDemo
{
    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global

    public class MarkerHub : Hub
    {
        private static MarkerModel _theMarker = new MarkerModel { Latitude = 62.25, Longitude = 25.75 };

        public void UpdateModel( MarkerModel clientModel )
        {
            // send the marker state to all other clients except the one sending the update
            _theMarker = clientModel;
            Clients.AllExcept( Context.ConnectionId ).updateMarker( clientModel );
        }

        public void RequestUpdate()
        {
            // send the marker state to the client requesting the update
            Clients.Caller.updateMarker( _theMarker );
        }
    }

    public class MarkerModel
    {
        [JsonProperty( "latitude" )]
        public double Latitude { get; set; }
        [JsonProperty( "longitude" )]
        public double Longitude { get; set; }
    }
}