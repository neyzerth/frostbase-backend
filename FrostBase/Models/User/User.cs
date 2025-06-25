public class User
{
    #region statement
    
    //Sql or mongo statements
    
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
    private string _truckId;

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
        get => _password;
        set => _password = value;
    }

    public string TruckId
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
                FirtsName = "Andres",
                LastName = "Llamas",
                MiddleName = "Brito",
                Email = "<EMAIL>",
                Phone = "6643112313",
                BirthDate = new DateTime(1999, 1, 1),
                Password = "<PASSWORD>",
                TruckId = "1"
            },
            new User
            {
                FirtsName = "Neyzer",
                LastName = "Pompomella",
                MiddleName = "Zapata",
                Email = "<EMAIL>",
                Phone = "6643123126",
                BirthDate = new DateTime(2005, 5, 5),
                Password = "<PASSWORD>",
                TruckId = "2"
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
            FirtsName = "Andres",
            LastName = "Llamas",
            MiddleName = "Brito",
            Email = "<EMAIL>",
            Phone = "3123123123",
            BirthDate = new DateTime(1990, 1, 1),
            Password = "<PASSWORD>",
            TruckId = "1"
        };
        //End test
        return u;

    }

    #endregion
}


