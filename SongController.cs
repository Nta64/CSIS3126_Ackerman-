using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebApplication5
{
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {

        public AppDb Db { get; }

        public PlaylistDb Db2 { get; }

        public SongController(AppDb db, PlaylistDb db2)
        {
            Db = db;

            Db2 = db2; 
        }

        [HttpGet("/AuthenticateUser/{userName}/{password}")]
        public async Task<IActionResult> AuthenticateUserAccount(string userName, string password)
        {
            await Db.Connection.OpenAsync();
            var userquery = new UserQuery(Db);
            var userresult = userquery.UserAutherization(userName, password);
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

        // GET api/blog
        [HttpGet]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var urlquery = new SongPostQuery(Db);
            var urlresult = await urlquery.LatestSongs();
            return new OkObjectResult(urlresult);
        }

        //GET api/blog
        [HttpGet("/singleSong/{id:int}")]
        public async Task<IActionResult> GetSingleSong(int id)
        {
            await Db.Connection.OpenAsync();
            var urlquery = new SongPostQuery(Db);
            var urlresult = await urlquery.SingleUrlByTitle(id);
            return new OkObjectResult(urlresult);
        }


        // POST api/blog
        [HttpPost("/AddSongToPlaylist/{tableName}/{title}")]
        public async Task<IActionResult> PostSong(string tableName, string title)
        {
            await Db2.Connection2.OpenAsync();
            var query = new PlaylistPostQuery(Db2);
            await query.AddSong(tableName, title);
            return new OkObjectResult(query);
        }

        // POST api/blog
        [HttpPost("/CreatePlaylist/{tableName}")]
        public async Task<IActionResult> PostPlaylist(string tableName)
        {
          
            await Db2.Connection2.OpenAsync();
            var query = new PlaylistPostQuery(Db2);
            await query.CreatePlaylist(tableName);
           
            return new OkObjectResult(query);   
        }

        //GET api/blog/5
        [HttpGet("{id:int}")]
       public async Task<IActionResult> GetUrls()
       {
           await Db.Connection.OpenAsync();
           var query = new SongPostQuery(Db);
           var resulttwo = await query.LatestUrls();
           if (resulttwo is null)
               return new NotFoundResult();
           return new OkObjectResult(resulttwo);
       }

        //GET api/blog/5
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

        //GET api/blog/5
        [HttpGet("{title}")]
        public async Task<IActionResult> GetTitles()
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var resulttwo = await query.LatestTitles();
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        //GET api/blog/5
        [HttpGet("/playlistNames")]
        public async Task<IActionResult> GetPlaylist()
        {
            await Db2.Connection2.OpenAsync();
            var query = new PlaylistPostQuery(Db2);
            var resulttwo = await query.LatestPlaylist();
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }

        //GET api/blog/5
        [HttpGet("/playlistUrls/{tableName}")]
        public async Task<IActionResult> GetPlaylistUrl(string tableName)
        {
            await Db2.Connection2.OpenAsync();
            var query = new PlaylistPostQuery(Db2);
            var resulttwo = await query.LatestPlaylistUrl(tableName);
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }



        //GET api/blog/5
        [HttpGet("/playlistTitles/{tableName}")]
        public async Task<IActionResult> GetPlaylistTitle(string tableName)
        {
            await Db2.Connection2.OpenAsync();
            var query = new PlaylistPostQuery(Db2);
            var resulttwo = await query.LatestPlaylistTitle(tableName);
            if (resulttwo is null)
                return new NotFoundResult();
            return new OkObjectResult(resulttwo);
        }


        // PUT api/blog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] Song info)
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            result.Title = info.Title;
            result.Author = info.Author;
            result.Album = info.Album;
            result.Url = info.Url;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE api/blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }

        // DELETE api/blog
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await Db.Connection.OpenAsync();
            var query = new SongPostQuery(Db);
            await query.DeleteAllAsync();
            return new OkResult();
        }

       
    }
}
