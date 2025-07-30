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
            if(string.IsNullOrEmpty(u.Id))
                u.Id = ObjectId.GenerateNewId().ToString();
            _userColl.InsertOne(u);
            return u;
        }
        catch (Exception e)
        {
            Console.WriteLine("user insert: "+e);
            throw new Exception("Error inserting user: "+e.Message);
        }
    }

    public static UserApp Update(UpdateUserDto u)
    {
        var user = new UserApp()
        {
            Id = u.Id,
            FirstName = u.Name.FirstName,
            MiddleName = u.Name.MiddleName,
            LastName = u.Name.LastName,
            Email = u.Email,
            Phone = u.Phone,
            BirthDate = u.BirthDate.Value.ToDateTime(TimeOnly.MinValue),
        };
        
        return Update(user);
    }
    public static UserApp Update(UserApp updatedUser)
    {
        try
        {
            var filter = Builders<UserApp>.Filter.Eq(u => u.Id, updatedUser.Id);
            var update = Builders<UserApp>.Update
                .Set(u => u.FirstName, updatedUser.FirstName)
                .Set(u => u.MiddleName, updatedUser.MiddleName)
                .Set(u => u.LastName, updatedUser.LastName)
                .Set(u => u.Email, updatedUser.Email)
                .Set(u => u.Phone, updatedUser.Phone)
                .Set(u => u.BirthDate, updatedUser.BirthDate)
                .Set(u => u.Active, updatedUser.Active);
            

            var options = new FindOneAndUpdateOptions<UserApp>
            {
                ReturnDocument = ReturnDocument.After
            };

            return _userColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine("user insert: "+e);
            throw new Exception("Error updating user: " + e.Message);
        }
    }
    
    public static UserApp Login(string email, string password)
    {
        var user = _userColl.Find(u => u.Email == email && u.Password == password && u.Active).FirstOrDefault();
        if (user != null) return user;
        
        throw new Exception("Email or Password incorrect");
    }
    
    public static UserApp Delete(string id)
    {
        try
        {
            var filter = Builders<UserApp>.Filter.Eq(u => u.Id, id);
            var update = Builders<UserApp>.Update.Set(u => u.Active, false);

            var options = new FindOneAndUpdateOptions<UserApp>
            {
                ReturnDocument = ReturnDocument.After
            };

            return _userColl.FindOneAndUpdate(filter, update, options);
        }
        catch (Exception e)
        {
            Console.WriteLine("user insert: "+e);
            throw new Exception("Error deleting (deactivating) user: " + e.Message);
        }
    }
    
    #endregion
}


