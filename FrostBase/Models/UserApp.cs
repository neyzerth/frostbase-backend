using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

public class UserApp
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<CreateUserDto> _userColl = 
        MongoDbConnection.GetCollection<CreateUserDto>("users");
    
    #endregion
    
    #region attributes

    private int _id;
    private string _firstName;
    private string _lastName;
    private string _middleName;
    private string _email;
    private string _phone;
    private DateTime _birthDate;
    private string _password;
    private Truck _truck;

    #endregion

    #region properties

    [BsonId]
    public int Id
    {
        get => _id;
        set => _id = value;
    }
    
    [BsonElement("first_name")]
    public string FirstName
    {
        get => _firstName;
        set => _firstName = value;
    }

    [BsonElement("last_name")]   
    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    [BsonElement("middle_name")]
    public string MiddleName
    {
        get => _middleName;
        set => _middleName = value;
    }

    [BsonElement("email")]
    public string Email
    {
        get => _email;
        set => _email = value;
    }

    [BsonElement("phone")]
    public string Phone
    {
        get => _phone;
        set => _phone = value;
    }

    [BsonElement("birth_date")]
    public DateTime BirthDate
    {
        get => _birthDate;
        set => _birthDate = value;
    }

    [BsonElement("password")]
    public string Password
    {
        set => _password = value;
    }

    [BsonElement("IDTruck")] 
    public int IDTruck { get => _truck.Id; set => _truck.Id = value; }

    [BsonIgnore]
    public Truck Truck
    {
        get => _truck;
        set => _truck = value;
    }

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
    public static UserApp Get(int id)
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
            Truck = Truck.Get(1001)
        };
        //End test
        return u;
    }

    public static UserApp createUser(CreateUserDto c)
    {
        UserApp u = new UserApp
        {
            Id = c.Id
        };
        
        return u;
    }

    public static bool Insert(CreateUserDto c)
    {
        try
        {
            _userColl.InsertOne(c);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    #endregion
}


