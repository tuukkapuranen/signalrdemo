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
            _theMarker = clientModel;
            Clients.AllExcept( Context.ConnectionId ).updateMarker( clientModel );
        }

        public void RequestUpdate()
        {
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