using MongoDB.Driver;
namespace FrostBase.Models.User;

public class User
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<User> _userColl = MongoDbConnection.GetCollection<User>("users");
    
    #endregion
    
    #region attributes

    private int _id;
    private string _firtsName;
    private string _lastName;
    private string _middleName;
    private string _email;
    private string _phone;
    private DateTime _birthDate;
    private string _password;
    //TODO - use truck model
    //private Truck _truck
    private int _truckId;

    #endregion

    #region properties

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string FirtsName
    {
        get => _firtsName;
        set => _firtsName = value;
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    public string MiddleName
    {
        get => _middleName;
        set => _middleName = value;
    }

    public string Email
    {
        get => _email;
        set => _email = value;
    }

    public string Phone
    {
        get => _phone;
        set => _phone = value;
    }

    public DateTime BirthDate
    {
        get => _birthDate;
        set => _birthDate = value;
    }

    public string Password
    {
        set => _password = value;
    }

    //public Truck Truck
    public int TruckId
    {
        get => _truckId;
        set => _truckId = value;
    }

    #endregion

    #region constructors

    

    #endregion

    #region class methods
    
    /// <summary>
    /// Returns a list of all users
    /// </summary>
    /// <returns></returns>
    public static List<User> Get() 
    {
        //Test
        List<User> users =
        [
            new User
            {
                Id = 1001,
                FirtsName = "Andres",
                LastName = "Llamas",
                MiddleName = "Brito",
                Email = "andres.llamas@gmail.com",
                Phone = "6643112313",
                BirthDate = new DateTime(1999, 1, 1),
                Password = "<PASSWORD>",
                TruckId = 1
            },
            new User
            {
                Id = 1002,
                FirtsName = "Neyzer",
                LastName = "Popomella",
                MiddleName = "Zapata",
                Email = "neyzer.pompella@gmail.com",
                Phone = "6643123126",
                BirthDate = new DateTime(2005, 5, 5),
                Password = "<PASSWORD>",
                TruckId = 2
            },
        ];
        //End test
        
        return users;
    }

    /// <summary>
    /// Returns the user with the specified id
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns></returns>
    public static User Get(int id)
    {
        //Test
        User u = new User
        {
            Id = id,
            FirtsName = "Andres",
            LastName = "Llamas",
            MiddleName = "Brito",
            Email = "<EMAIL>",
            Phone = "3123123123",
            BirthDate = new DateTime(1990, 1, 1),
            Password = "<PASSWORD>",
            TruckId = 1
        };
        //End test
        return u;

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }

    public static bool Insert(User u)
    {
        try
        {
            _userColl.InsertOne(u);
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


