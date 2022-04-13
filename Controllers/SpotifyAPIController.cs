using System.Collections.Generic;
using System.Threading.Tasks;
using SpotifyController.Data;
using SpotifyController.Model;
using SpotifyController.Model.SpotifyAPI;
using SpotifyController.Services;
using Microsoft.AspNetCore.Mvc;
using SpotifyController.Exceptions;

namespace LogenV2React.Controllers
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class SpotifyAPIController : ControllerBase
    {

        private SpotifyAPIService _spotifyAPIService;

        public SpotifyAPIController(SpotifyAPIService spotifyAPIService)
        {
            _spotifyAPIService = spotifyAPIService;
        }

        [HttpGet]
        public IActionResult Authorize()
        {
            return Redirect(_spotifyAPIService.AuthorizeUser());
        }

        [HttpGet]
        public async Task<IActionResult> AccessToken()
        {
            User user = UserRepo.TestUser;

            try
            {
                (bool succes, string tokenContent) = await _spotifyAPIService.GetToken(user);
                if (!succes)
                    return NotFound($"Could not get Token for user - Token response: {tokenContent}");
            }
            catch (SpotifyNotConnectedException)
            {
                return Redirect("/API/SpotifyAPI/Authorize");
            }

            return Redirect("/");
        }
        
        [HttpPost]
        public async Task<IActionResult> QueueTrack([FromQuery] string trackId)
        {
            if (trackId == null)
                return NotFound("No trackId supplied!");

            User user = UserRepo.TestUser;
            bool succesfullyAdded;
            string responseContent;

            try
            {
                (succesfullyAdded, responseContent) = await _spotifyAPIService.QueueTrack(user, trackId);
            }
            catch (SpotifyNotConnectedException)
            {
                return Unauthorized("Not Connected to spotify!");
            }

            if (!succesfullyAdded)
                return NotFound("Song could not be added to queue!");

            return Ok("Song succesfully added to queue!");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPlaylists()
        {
            User user = UserRepo.TestUser;
            
            bool succesfullyAdded;
            Playlists playlists;
            string responseContent;

            try
            {
                (succesfullyAdded, playlists, responseContent) = await _spotifyAPIService.GetCurrentUsersPlaylists(user);
                //(succesfullyAdded, playlists, responseContent) = await _spotifyAPIService.GetCurrentUsersPlaylists(user);
            }
            catch (SpotifyNotConnectedException)
            {
                return Unauthorized(new {Message = "Not Connected to spotify!"});
            }

            if (!succesfullyAdded)
                return NotFound("Playlists could not be found!");

            return Ok(playlists);
            //return Ok(responseContent);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaylist([FromQuery] string playlistId)
        {
            if (playlistId == null)
                return NotFound("No playlistId supplied!");

            User user = UserRepo.TestUser;
            
            bool succesfullyAdded;
            Playlist playlist;
            string responseContent;

            try
            {
                (succesfullyAdded, playlist, responseContent) = await _spotifyAPIService.GetPlaylist(user, playlistId);
            }
            catch (SpotifyNotConnectedException)
            {
                return Unauthorized(new {Message = "Not Connected to spotify!"});
            }

            if (!succesfullyAdded)
                return NotFound("Playlist could not be found!");

            return Ok(playlist);
        }

        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (query == null)
                return NotFound("No search query found");

            User user = UserRepo.TestUser;

            bool succesfullyAdded;
            Search searchResult;
            string responseContent;

            try
            {
                (succesfullyAdded, searchResult, responseContent) = await _spotifyAPIService.Search(user, query);
            }
            catch (SpotifyNotConnectedException)
            {
                return Unauthorized(new { Message = "Not Connected to spotify!" });
            }

            if (!succesfullyAdded)
                return NotFound("Search failed!");

            return Ok(searchResult);
            return Ok(responseContent);
        }

    }
}
