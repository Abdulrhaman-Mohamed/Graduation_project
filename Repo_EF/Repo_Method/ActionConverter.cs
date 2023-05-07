
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repo_Core.Identity_Models;
namespace Repo_EF.Repo_Method
{
    public class ActionConverter : JsonConverter<ApplicationUser>
    {

        private readonly ApplicationDbContext  _dbContext;
        
        public ActionConverter(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.SaveChanges();
        }

         
        public override ApplicationUser? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // deserialize the JSON data into a Person instance
            var json = JsonSerializer.Deserialize<ApplicationUser>(ref reader, options);
            return json;
        }

        public override void Write(Utf8JsonWriter writer, ApplicationUser value, JsonSerializerOptions options)
        {
            // serialize the Person instance into JSON data
            var json = JsonSerializer.Serialize(value, options);
            writer.WriteStringValue(json);

            // add the Person instance to the database context for tracking
            _dbContext.Set<ApplicationUser>().Add(value);
        }
    }
}