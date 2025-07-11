using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class UserApp
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<UserApp> _userColl = 
        MongoDbConnection.GetCollection<UserApp>("Users");
    
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
    
    [BsonElement("birthdate")]
    public DateTime BirthDate { get; set; }
    
    [BsonElement("password")]
    public string Password { get; set; }
    
    [BsonElement("is_admin")] 
    public bool IsAdmin { get; set; }
    
    [BsonElement("active")] 
    public bool Active { get; set; }
    
    #endregion

    #region constructors

    

    #endregion

    #region class methods
    
    public static List<UserApp> Get() 
    {
        
        return _userColl.Find(_ => true).ToList();;
    }
    public static List<UserApp> GetAdmin() 
    {
        
        return _userColl.Find( u => u.IsAdmin == true).ToList();;
    }
    public static List<UserApp> GetDriver() 
    {
        
        return _userColl.Find( u => u.IsAdmin == false).ToList();;
    }

    public static UserApp Get(string id)
    {
        
        return _userColl.Find(u => u.Id == id).FirstOrDefault();;
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
            IsAdmin = c.IsAdmin,
            Active = true
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


