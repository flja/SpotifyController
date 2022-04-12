using System;

namespace SpotifyController.Model
{
    public class APIData
    {
        public APIData(string code, string status)
        {
            Code = code;
            Status = status;
        }

        public string Code { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Status { get; set; }

        public DateTime AccessTokenExpiration { get; set; }
    }
}