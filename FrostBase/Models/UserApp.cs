using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class UserApp
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<UserApp> _userColl = 
        MongoDbConnection.GetCollection<UserApp>("users");
    
    #endregion
    
    #region properties

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("first_name")]
    public string FirstName { get; set; }
    
    [BsonElement("last_name")]   
    public string LastName { get; set; }
    
    [BsonElement("middle_name")]
    public string MiddleName { get; set; }
    
    [BsonElement("email")]
    public string Email { get; set; }
    
    [BsonElement("phone")]
    public string Phone { get; set; }
    
    [BsonElement("birth_date")]
    public DateTime BirthDate { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    [BsonElement("IDTruck")] 
    public string IDTruck { get; set; }
    
    #endregion

    #region constructors

    

    #endregion

    #region class methods
    
    /// <summary>
    /// Returns a list of all users
    /// </summary>
    /// <returns></returns>
    public static List<UserApp> Get() 
    {
        //Test
        // List<UserApp> users =
        // [
        //     new UserApp
        //     {
        //         Id = 1001,
        //         FirstName = "Andres",
        //         LastName = "Llamas",
        //         MiddleName = "Brito",
        //         Email = "andres.llamas@gmail.com",
        //         Phone = "6643112313",
        //         BirthDate = new DateTime(1999, 1, 1),
        //         Password = "<PASSWORD>",
        //         Truck = Truck.Get(1003)
        //     },
        //     new UserApp
        //     {
        //         Id = 1002,
        //         FirstName = "Neyzer",
        //         LastName = "Popomella",
        //         MiddleName = "Zapata",
        //         Email = "neyzer.pompella@gmail.com",
        //         Phone = "6643123126",
        //         BirthDate = new DateTime(2005, 5, 5),
        //         Password = "<PASSWORD>",
        //         Truck = Truck.Get(1001)
        //     },
        //];
        //End test
        
        var users = MongoDbConnection.Find<UserApp>("users");
        return users;
    }

    /// <summary>
    /// Returns the user with the specified id
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns></returns>
    public static UserApp Get(string id)
    {
        //Test
        UserApp u = new UserApp
        {
            Id = id,
            FirstName = "Andres",
            LastName = "Llamas",
            MiddleName = "Brito",
            Email = "<EMAIL>",
            Phone = "3123123123",
            BirthDate = new DateTime(1990, 1, 1),
            Password = "<PASSWORD>",
            IDTruck = ObjectId.GenerateNewId().ToString()
        };
        //End test
        return u;
    }

    public static UserApp Insert(CreateUserDto c)
    {
        UserApp u = new UserApp
        {
            Id = ObjectId.GenerateNewId().ToString(),
            FirstName = c.Name,
            LastName = c.LastName,
            MiddleName = c.MiddleName,
            Email = c.Email,
            Phone = c.Phone,
            BirthDate = c.BirthDate,
            Password = c.Password,
            IDTruck = c.IDTruck
        };
        
        return Insert(u);
    }

    public static UserApp Insert(UserApp u)
    {
        try
        {
            _userColl.InsertOne(u);
            return u;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    #endregion
}


