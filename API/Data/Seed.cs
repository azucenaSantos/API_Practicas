
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)//al ser static podemos acceder a el a través de la clase sin necesidad de crear una instancia de ella.
        {
            //Comprobar si hay usuarios en la bd
            if (await context.Users.AnyAsync()) return; //Si ya tiene, sale del metodo

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            //Hacemos que el json funcione sea cual sea la nomenclatura (pascal o camel) de los nombres de atributos del json
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData); //Desserializamos

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")); //en este ejemplo tendremos la misma contraseña para cada usuario
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);//Añadimos al entity framework
            }
            await context.SaveChangesAsync(); //Guardamos datos en la base de datos

        }

    }
}