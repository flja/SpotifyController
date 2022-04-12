using System;

namespace SpotifyController.Model
{
    public class User
    {
        public string Name { get; set; }
        
        public APIData spotifyAPIData { get; set; }
        public bool IsConnectedToSpotify => spotifyAPIData != null;
    }
}
