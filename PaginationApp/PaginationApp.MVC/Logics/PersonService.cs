using Newtonsoft.Json;
using Paginations.Models;

namespace Paginations.Logics;

public interface IPersonService
{
    Task<List<Person>> GetPerson();
}

public class PersonService : IPersonService
{
    public async Task<List<Person>> GetPerson()
    {
        return GetData().Result;
    }

    private async Task<List<Person>> GetData()
    {
        var list = new List<Person>();
        var users = JsonConvert.DeserializeObject<List<Person>>(File.ReadAllText("Data/User/User.json"));

        foreach (var user in users)
        {
            list.Add(new Person() { Id = user.Id, Name = user.Name, Email = user.Email, Place = user.Place });
        }
        
        return list;
    }
}
