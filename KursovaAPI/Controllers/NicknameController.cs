using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Numerics;

namespace KursovaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NicknameController : ControllerBase
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<User> _usersCollection;
        public NicknameController(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            // Отримайте посилання на колекцію "users" у базі даних
            var database = _mongoClient.GetDatabase("KyrsovaDatabase");
            _usersCollection = database.GetCollection<User>("users");
        }
        [HttpPost]
        [Route("setusers")]
        public async Task<IActionResult> SetPlayer([FromBody] UpdateUserRequest request)
        {
            // Отримайте chatId з запиту
            long chatId = request.ChatId;

            // Отримайте нікнейм з запиту
            string name = request.NewName;

            // Збережіть нікнейм користувача в базі даних
            var existingUser = await _usersCollection.Find(u => u.ChatId == chatId).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                existingUser.Name = name;
                await _usersCollection.ReplaceOneAsync(u => u.Id == existingUser.Id, existingUser);
            }
            else
            {
                var newUser = new User
                {
                    ChatId = chatId,
                    Name = name
                };
                await _usersCollection.InsertOneAsync(newUser);
            }

            // Поверніть успішну відповідь
            return Ok();
        }
        [HttpPut]
        [Route("updateusers")]
        public async Task<IActionResult> UpdatePlayer([FromBody] UpdateUserRequest request)
        {
            // Отримайте chatId з запиту
            long chatId = request.ChatId;

            // Отримайте новий нікнейм з запиту
            string newName = request.NewName;

            // Оновіть нікнейм користувача в базі даних
            var existingUser = await _usersCollection.Find(u => u.ChatId == chatId).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                existingUser.Name = newName;
                await _usersCollection.ReplaceOneAsync(u => u.Id == existingUser.Id, existingUser);
            }

            // Поверніть успішну відповідь
            return Ok();
        }
    }
    public class User
    {
        public ObjectId Id { get; set; }
        public long ChatId { get; set; }
        public string Name { get; set; }
    }
    public class UpdateUserRequest
    {
        public long ChatId { get; set; }
        public string NewName { get; set; }
    }
}
