using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebApplication5
{
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        public AppDb Db { get; }
        public SongController(AppDb db)
        {
            Db = db;
        }

        //Click plus symbol next to line Number to open regions labled User, Song, Playlist related queries

        //Each query is put into an http request, the purposes of each request is explained within the api url route, and further explained within the queries referenced
        #region User Related Queries 

        [HttpGet("/AuthenticateUser/{UserName}/{Password}")]
        public async Task<IActionResult> AuthenticateUserAccount(string UserName, string Password)
        {
            await Db.Connection.OpenAsync();
            var userquery = new UserQuery(Db);
            var userresult = userquery.UserAutherization(UserName, Password);
            return new OkObjectResult(userresult);
        }
       
        [HttpPost("/CreateUser/{userName}/{password}/{dateTime}")]
        public async Task<IActionResult> CreateUserAccount(string userName, string password, string dateTime)
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            await query.RegisterUserAsync(userName, password, dateTime);
            return new OkObjectResult(query);
        }

        [HttpGet("/getUserId/{UserName}")]
        public async Task<IActionResult> GetUserID(string UserName)
        {
            await Db.Connection.OpenAsync();
            var query = new UserQuery(Db);
            var resulttwo = await query.CurrentUserId(UserName);
            return new OkObjectResult(resulttwo);
        }
        #endregion

        #region Song Related Queries
        [HttpGet("/SongTitles")]
        public async Task<IActionResult> GetSongs()
        {
            await Db.Connection.OpenAsync();
            var urlquery = new SongPostQuery(Db);
            var urlresult = await urlquery.LatestTitles();
            return new OkObjectResult(urlresult);
        }

        [HttpGet("/GetTitles")]
        public async Task<IActionResult> GetTitles()
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var resulttwo = await query.LatestTitles();
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/GetArtists")]
        public async Task<IActionResult> GetAuthors()
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var resulttwo = await query.LatestArtists();
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/SongUrls")]
        public async Task<IActionResult> GetSongUrls()
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var resulttwo = await query.LatestSongUrls();
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/GetSongAlbumImages")]
        public async Task<IActionResult> GetImages()
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var resulttwo = await query.LatestImages();
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/GetSongIds")]
        public async Task<IActionResult> GetSongIds()
        {
            await Db.Connection.OpenAsync();
            var urlquery = new SongPostQuery(Db);
            var urlresult = await urlquery.LatestSongIds();
            return new OkObjectResult(urlresult);
        }

        [HttpGet("/GetPlaylistSongIds/{PlaylistID:int}")]
        public async Task<IActionResult> GetPlaylistSongIds(int PlaylistID)
        {
            await Db.Connection.OpenAsync();
            var urlquery = new SongPostQuery(Db);
            var urlresult = await urlquery.LatestPlaylistSongIds(PlaylistID);
            return new OkObjectResult(urlresult);
        }

        [HttpGet("/GetCurrentSong/{SongId:int}")]
        public async Task<IActionResult> GetCurrentSong(int SongId)
        {
            await Db.Connection.OpenAsync();
            var urlquery = new SongPostQuery(Db);
            var urlresult = await urlquery.LatestSong(SongId);
            return new OkObjectResult(urlresult);
        }

        [HttpGet("/GetCurrentAlbum/{SongId:int}")]
        public async Task<IActionResult> GetCurrentAlbum(int SongId)
        {
            await Db.Connection.OpenAsync();
            var urlquery = new SongPostQuery(Db);
            var urlresult = await urlquery.LatestAlbum(SongId);
            return new OkObjectResult(urlresult);
        }
        #endregion

        #region Playlist Related Queries

        [HttpPost("/AddSongToPlaylist/{SongID}/{PlaylistID}")]
        public async Task<IActionResult> PostSong(int SongID, int PlaylistID)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            await query.AddSong(SongID, PlaylistID);
            return new OkObjectResult(query);
        }

        [HttpDelete("/RemoveSongFromPlaylist/{SongID}/{PlaylistID}")]
        public async Task<IActionResult> RemoveSong(int SongID, int PlaylistID)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            await query.RemoveSong(SongID, PlaylistID);
            return new OkObjectResult(query);
        }

        [HttpPost("/CreatePlaylist/{UserId}/{PlaylistName}")]
        public async Task<IActionResult> PostPlaylist(int UserID, string PlaylistName)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            await query.CreatePlaylist(UserID, PlaylistName);
            return new OkObjectResult(query);
        }

        [HttpDelete("/DeletePlaylist/{PlaylistId}")]
        public async Task<IActionResult> DeletePlaylist(int PlaylistId)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            await query.DeletePlaylist(PlaylistId);
            return new OkObjectResult(query);
        }

        [HttpGet("/PlaylistSongUrls/{PlaylistID:int}")]
        public async Task<IActionResult> GetPlaylistSongUrls(int PlaylistID)
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var resulttwo = await query.LatestPlaylistSongUrls(PlaylistID);
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/GetPlaylistAlbumImages/{PlaylistID:int}")]
        public async Task<IActionResult> GetPlaylistSongAlbumImages(int PlaylistID)
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var resulttwo = await query.LatestPlaylistSongAlbumImages(PlaylistID);
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/playlistNames/{UserId:int}")]
        public async Task<IActionResult> GetPlaylist(int UserId)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            var resulttwo = await query.LatestPlaylist(UserId);
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/getPlaylistId/{UserID:int}/{PlaylistName}")]
        public async Task<IActionResult> GetPlaylistID(int UserID, string PlaylistName)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            var resulttwo = await query.CurrentPlaylistId(UserID, PlaylistName);
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/getAllPlaylistId/{UserID:int}")]
        public async Task<IActionResult> GetAllPlaylistID(int UserID)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            var resulttwo = await query.LatestPlaylistsIds(UserID);
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/playlistTitles/{PlaylistID:int}")]
        public async Task<IActionResult> GetPlaylistTitle(int PlaylistID)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            var resulttwo = await query.LatestPlaylistTitle(PlaylistID);
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        [HttpGet("/GetPlaylistArtists/{PlaylistId}")]
        public async Task<IActionResult> GetPlaylistArtists(int PlaylistId)
        {
            await Db.Connection.OpenAsync();
            var query = new PlaylistPostQuery(Db);
            var resulttwo = await query.CurrentPlaylistArtists(PlaylistId);
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }
        #endregion
    }
}
