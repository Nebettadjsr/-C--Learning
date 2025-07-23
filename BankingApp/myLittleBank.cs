
using System;
using System.Collections.Generic;
using System.Globalization;


/// <summary>
/// Class "BankAccount" holds all Variables, Functions & Cunstroctor associated with all Accounts in the Banking system. 
/// </summary>
/// <param>
/// string userName, float accountBalance, float userPin
/// </param>
public class BankAccount
{
    // Attributes/ Variables of BankAccount class
    public string userName;
    public decimal accountBalance;
    public float userPin;
    
    // Constructor
    // links Object Variables to input Parameter
    public BankAccount(string userName, decimal accountBalance, float userPin)
    {
        this.userName = userName;
        this.userPin = userPin;
        this.accountBalance = accountBalance;
    }
    
    // Methods
    /// <summary>
    /// balanceCheck prints current Account balance to Console
    /// </summary>
    public void balanceCheck()
    {
        Console.WriteLine($"The account balance of {userName} is ${accountBalance:N2}");
    }

    /// <summary>
    /// addBalance allows the user to deposit money into the account. 
    /// Positive Numbers only.
    /// </summary>
    public void addBalance()
    {
        decimal numMoney = 0;

        Console.WriteLine("How much money do you wish to deposit today?");
        string strMoney = Console.ReadLine();
        bool isValid = decimal.TryParse(strMoney, out numMoney);
            
            while (!isValid || numMoney <=0)
            {
                Console.WriteLine("Please only enter positive Numbers. \nHow much money do you wish to deposit today?");
                strMoney = Console.ReadLine();
                isValid = decimal.TryParse(strMoney, NumberStyles.Number, CultureInfo.InvariantCulture, out numMoney);
            }
            accountBalance = accountBalance + numMoney;
            Console.WriteLine($"The new account balance of {userName} is ${accountBalance:N2}");
    }

    /// <summary>
    /// takeBalance - allows user to withdraw money from the account.
    /// validates userIput: only positive Numbers smaller then Account Balance 
    /// </summary>
    public void takeBalance()
    {
        decimal numMoney = 0;
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine("How much money do you wish to withdraw today?");
            string strMoney = Console.ReadLine();
            bool isValid = decimal.TryParse(strMoney, out numMoney);
                
                if (!isValid || numMoney <=0 )
                {
                    Console.WriteLine("Please only enter positive Numbers.");
                    continue;
                }
                
                if (accountBalance < numMoney)
                {
                    Console.WriteLine($"Inssufficient Funds. \nMaximum withdraw Amount is: $ {accountBalance:N2} \n\nHow much money do you wish to withdraw?"); 
                    continue;
                }

             validInput = true;
        }

        accountBalance = accountBalance - numMoney; 
        Console.WriteLine($"The new account balance of {userName} is ${accountBalance:N2}");
    
    }
}
public class BankAccountSystem
{
    /// <summary>
    /// Main Programm Logic - class BankAccountSystem
    /// Creates Users - Demo User "Herbert" and new Users
    /// Logic for selecting, switching, validating Users (PIN check)
    /// Logic for main Menue in [public static void Main]
    /// </summary>
    
    /// <summary>
    /// Dictionary that holds all user accounts.
    /// Key = userName (string), Value = BankAccount object
    /// </summary>
    static Dictionary<string, BankAccount> UserAccounts = new();

    /// <summary>
    /// currentUser initiated
    /// is needed in Arguments when doing function call in Class BankAccount
    /// </summary>
    static string currentUser;

    /// <summary>
    /// CreateUser Logic
    /// getting and validating UserInput for Name, PIN, Balance
    /// Ends with call to balanceCheck to confirm saved Name & Balance
    /// </summary>
    static string CreateUser(){
        
            Console.WriteLine ("What is the Name of the new Account?");
                string newAccountName = Console.ReadLine();
                 while (UserAccounts.ContainsKey(newAccountName))
                    {
                        Console.WriteLine ($"Account named \"{newAccountName}\" already exists! Choose a different name! \nWhat is the Name of the new Account?");
                        newAccountName = Console.ReadLine();
                    }
                Console.WriteLine ("How much money will you deposit in your new account?");
                string newAccountBalance = Console.ReadLine();
                bool isBalanceValid = decimal.TryParse(newAccountBalance, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal intAccountBalance);
                
                while (!isBalanceValid)
                {
                    Console.WriteLine("Please only enter the new Balance in Numbers. \nHow much money will you deposit in your new account?");
                    newAccountBalance = Console.ReadLine();
                    isBalanceValid = decimal.TryParse(newAccountBalance, out intAccountBalance);
                }

                Console.WriteLine($"Please set a PIN for {newAccountName} account: ");
                string currentUserPIN = Console.ReadLine();
                bool isPinValid = float.TryParse(currentUserPIN, out float intCurrentUserPIN);
                
                while (!isPinValid)
                {
                    Console.WriteLine($"Please only enter the PIN in Numbers. \nPlease set a PIN for {newAccountName} account:");
                    currentUserPIN = Console.ReadLine();
                    isPinValid = float.TryParse(currentUserPIN, out intCurrentUserPIN);
                }                        
                UserAccounts.Add(newAccountName, new BankAccount(newAccountName, intAccountBalance, intCurrentUserPIN));
                UserAccounts[newAccountName].balanceCheck();
                return newAccountName;
                
        }
    
    static void RemoveUser(){
        Console.WriteLine($"Please confirm you wish to delete user Account {currentUser} by typing \"y\" to continue and \"n\" to abort.");        
        string conf = Console.ReadLine();
        
        switch (conf)
        {
            case "y":
            case "Y":
                Console.WriteLine($"To proceed deletion of user Account {currentUser} please provide PIN:");
                bool pinOK = CheckPin();

                if (!pinOK)
                {
                    Console.WriteLine("Wrong PIN! Deletion aborted!. Exiting...");
                    return;
                }
               
                Console.WriteLine($"User {currentUser} succsessfully deleted.");
                UserAccounts.Remove("Herbert");
                return;
                

                break;
            default:
                Console.WriteLine("Account deletion abort. Retun to Main Menu.");
                return;

        }
    }
   
    /// <summary>
    /// switchUser allows to toggle between multiple accounts.
    /// if input invalid (due to typo of capital letters or similar) offers user to create new account or try again or exit
    /// creating a new user in this loop will result in imidiate CheckPin after creation - not ideal but acceptable for this project
    /// </summary>
    static string switchUser() 
    {
        while (true) // <--- Loop until valid user selected or account created
        {
            Console.WriteLine("What is the name of the Account you wish to access?");
            if (UserAccounts.ContainsKey("Herbert"))
                {
                    Console.WriteLine("Default user \"Herbert\" - PIN 1234");
                } 
            string userinput = Console.ReadLine();

            if (UserAccounts.ContainsKey(userinput))
            {
                return userinput; 
            }
            else
            {
                Console.WriteLine($"No account with name: {userinput}");
                Console.WriteLine("Would you like to: \n(c)reate new account \n(t)ry again \n(e)xit");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "t":
                    case "T":
                        
                        break;

                    case "c":
                    case "C":
                        string newUser = CreateUser();
                        return newUser; 

                    case "e":
                    case "E":
                        Console.WriteLine("Thank you for using my little Bank!");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose c, t, or e.");
                        break;
                }
            }
        }
    }
      
    /// <summary>
    /// CheckPin validates & compares user PIN
    /// after 3 wrong PIN numbers the program exits
    /// entering letters will not count towards CountPinTry
    /// </summary>
    static bool CheckPin()
    {
        int CountPinTry = 3;

        while (CountPinTry > 0)
        {
            Console.WriteLine($"What is the PIN of {currentUser} account?");
            string currentUserPIN = Console.ReadLine();

            bool isPinValid = float.TryParse(currentUserPIN, out float intCurrentUserPIN);

            if (!isPinValid)
            {
                Console.WriteLine($"Wrong PIN! {CountPinTry} attempts left.");
                Console.WriteLine("Please enter numbers only!");
                continue;
            }

            if (intCurrentUserPIN == UserAccounts[currentUser].userPin)
            {
                return true; 
            }

            CountPinTry--;
            Console.WriteLine($"Wrong PIN! {CountPinTry} attempts left.");
        }

        // After loop: user failed all attempts
        return false;
    }


       
    /// <summary>
    /// Main Program Menue
    /// Creating Demo User Herbert, PIN 1234, AccountBalance $9876.50
    /// </summary>
    public static void Main(string[] args)
    {   
        UserAccounts.Add("Herbert", new BankAccount("Herbert", 9876.5m, 1234));
                
        Console.WriteLine ("Welcome to my little Bank!");
                currentUser = switchUser();
                bool pinOK = CheckPin();

                if (!pinOK)
                {
                    Console.WriteLine("Wrong PIN. Exiting...");
                    return;
                }

                while (true)
                {
                Console.WriteLine ("Would you like to: \n(w)ithdraw Money? \n(c)heck your account balance? \n(d)eposit Money? \n(m)ake new Account \n(s)witch user \n(r)emove account \n(e)xit programm");
                string input = Console.ReadLine();
                
                switch (input)
                {
                    case "w":
                    case "W":
                        UserAccounts[currentUser].takeBalance();
                        break;
                    case "c":
                    case "C":
                        UserAccounts[currentUser].balanceCheck();
                        break;
                    case "d":
                    case "D":
                        UserAccounts[currentUser].addBalance();
                        break;
                    case "r":
                    case "R":
                        RemoveUser();
                        currentUser = switchUser();
                        if (!CheckPin())
                        {
                            Console.WriteLine("Wrong PIN. Exiting...");
                            return;
                        }
                        break;
                    case "e":
                    case "E":
                        Console.WriteLine("Thank you for using my little Bank!");
                        return;
                    case "m":
                    case "M":
                        CreateUser();
                         break;                        
                    case "s":
                    case "S":
                        currentUser = switchUser();
                        if (!CheckPin())
                        {
                            Console.WriteLine("Wrong PIN. Exiting...");
                            return;
                        }
                        break;
                }    
                }
            
           

        
    }
}
