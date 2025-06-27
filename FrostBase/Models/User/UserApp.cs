using MongoDB.Driver;

public class UserApp
{
    #region statement
    
    //Sql or mongo statements
    private static IMongoCollection<UserApp> _userColl = MongoDbConnection.GetCollection<UserApp>("users");
    
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
    private Truck _truck;

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
        List<UserApp> users =
        [
            new UserApp
            {
                Id = 1001,
                FirtsName = "Andres",
                LastName = "Llamas",
                MiddleName = "Brito",
                Email = "andres.llamas@gmail.com",
                Phone = "6643112313",
                BirthDate = new DateTime(1999, 1, 1),
                Password = "<PASSWORD>",
                Truck = new Truck()
            },
            new UserApp
            {
                Id = 1002,
                FirtsName = "Neyzer",
                LastName = "Popomella",
                MiddleName = "Zapata",
                Email = "neyzer.pompella@gmail.com",
                Phone = "6643123126",
                BirthDate = new DateTime(2005, 5, 5),
                Password = "<PASSWORD>",
                Truck = new Truck()
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
    public static UserApp Get(int id)
    {
        //Test
        UserApp u = new UserApp
        {
            Id = id,
            FirtsName = "Andres",
            LastName = "Llamas",
            MiddleName = "Brito",
            Email = "<EMAIL>",
            Phone = "3123123123",
            BirthDate = new DateTime(1990, 1, 1),
            Password = "<PASSWORD>",
            Truck = new Truck()
        };
        //End test
        return u;
    }

    public static bool Insert(UserApp u)
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


