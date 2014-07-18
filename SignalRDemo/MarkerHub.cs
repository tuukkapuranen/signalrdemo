using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace SignalRDemo
{
    // ReSharper disable UnusedMember.Global
    // ReSharper disable ClassNeverInstantiated.Global

    public class MarkerHub : Hub
    {
        private static readonly Dictionary<int, MarkerModel> markers = new Dictionary<int, MarkerModel>();

        static MarkerHub()
        {
            markers.Add( 7, new MarkerModel { Id = 7, Latitude = 62.25, Longitude = 25.75 } );
        }

        public void UpdateModel( MarkerModel clientModel )
        {
            // create new marker if needed
            if ( markers.ContainsKey( clientModel.Id ) )
                markers[clientModel.Id] = clientModel;
            else
                markers.Add( clientModel.Id, clientModel );

            // send the marker state to all other clients except the one sending the update
            Clients.All.updateMarker( markers.Values.ToArray() );
        }

        public void Delete( MarkerModel clientModel )
        {
            markers.Remove( clientModel.Id );
            // send the marker state to the client requesting the update
            Clients.All.updateMarker( markers.Values.ToArray() );
        }

        public void RequestUpdate()
        {
            // send the marker state to the client requesting the update
            Clients.Caller.updateMarker( markers.Values.ToArray() );
        }
    }

    public class MarkerModel
    {
        [JsonProperty( "latitude" )]
        public double Latitude { get; set; }
        [JsonProperty( "longitude" )]
        public double Longitude { get; set; }
        [JsonProperty( "id" )]
        public int Id { get; set; }
    }
}