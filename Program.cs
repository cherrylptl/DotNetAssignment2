namespace DotNetAssignment2;

class CarRental
{
    string[] Services = {
        "Economy",
        "Standard",
        "Luxury",
        };
    double[] ServicesPrice = {
        29.99,
        49.99,
        79.99,
        };
    string[] AdditionalServices = {
        "GPS Navigation",
        "Child Car Seat",
        "Chauffeur Service",
        };
    double[] AdditionalServicesPrice = {
        9.99,
        14.99,
        99.99,
        };

    List<Reservation> reservations = new List<Reservation>();

    static void Main(string[] args)
    {

        CarRental carRental = new CarRental();
        carRental.Initialize();
        carRental.WelComeMessage();
        carRental.DisplayOptions(carRental);
    }

    public void WelComeMessage()
    {
        Console.WriteLine("\n----------------------------------------------");
        Console.WriteLine("---------  WelCome to Car Rental   -----------");
        Console.WriteLine("----------------------------------------------");
    }

    public void DisplayOptions(CarRental carRental)
    {
        while (true)
        {
            Console.WriteLine("\nChoose an option below:");
            Console.WriteLine("1. Create a reservation");
            Console.WriteLine("2. List all reservations");
            Console.WriteLine("3. Clear all reservationsk");
            Console.WriteLine("4. Exit the program");

            if (int.TryParse(Console.ReadLine(), out int userOption) && userOption >= 5)
            {
                Console.WriteLine("Invalid input. Please enter valid option.");
                continue;
            }
            switch (userOption)
            {
                case 1:
                    carRental.CreateReservation(carRental);
                    break;
                case 2:
                    carRental.AllReservations();
                    break;
                case 3:
                    carRental.ClearAllReservations();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input , Try again\n");
                    break;
            }

        }

    }
    public void CreateReservation(CarRental carRental)
    {
        string? customerID = null;
        string? name = null;
        string? phoneNumber = null;
        int? selectedCustomerType = null;
        CustomerType customerType = CustomerType.Regular;

        while (true)
        {

            Console.WriteLine("Enter customer information:");
            if (customerID == null)
            {
                Console.WriteLine("Customer ID (6-digit alphanumeric):");
                customerID = Console.ReadLine();
                if (string.IsNullOrEmpty(customerID) || customerID.Length != 6 || !customerID.All(char.IsLetterOrDigit))
                {
                    Console.WriteLine("Invalid Customer ID. Should be 6-digit alphanumeric.");
                    customerID = null;
                    continue;
                }
            }

            if (name == null)
            {
                Console.WriteLine("Name:");
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty.");
                    name = null;
                    continue;
                }
            }


            while (phoneNumber == null)
            {
                Console.WriteLine("Phone Number (10 digits):");
                phoneNumber = Console.ReadLine();
                if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit))
                {
                    Console.WriteLine("Invalid phone number. Please enter a valid 10-digit number.");
                    phoneNumber = null;
                    continue;
                }
            }

            while (selectedCustomerType == null)
            {
                Console.WriteLine("Customer Type (0 for Regular, 1 for Premium, 2 for VIP):");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int parsedValue) || parsedValue < 0 || parsedValue > 2)
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                selectedCustomerType = parsedValue;
            }

            customerType = selectedCustomerType == 0 ? CustomerType.Regular :
                selectedCustomerType == 1 ? CustomerType.Premium : CustomerType.VIP;

            //Create New Customer
            Customer customer = new Customer(customerID, name, phoneNumber, customerType);

            int carTypeOption = carRental.ChooseCarType();

            bool isAdditionalServiceRequested = false;

            do
            {
                Console.WriteLine("Do you want to include additional services? (Yes/No)");
                string userInput = Console.ReadLine() ?? "".ToLower();

                if (userInput == "yes")
                {
                    isAdditionalServiceRequested = true;
                    break;
                }
                else if (userInput == "no")
                {
                    isAdditionalServiceRequested = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            } while (true);

            int additionalServicesIndex = -1;
            if (isAdditionalServiceRequested)
            {
                additionalServicesIndex = ChooseAdditionalService();
            }

            //Calculate Total Price
            double totalPrice = carRental.CalculateTotalPrice(ServicesPrice[carTypeOption], additionalServicesIndex == -1 ? 0.0 : AdditionalServicesPrice[additionalServicesIndex]);

            //Create New Reservation and Add Customer
            reservations.Add(new Reservation(reservations.Count + 1, customer, Services[carTypeOption], additionalServicesIndex == -1 ? "No Additional Service" : AdditionalServices[additionalServicesIndex], totalPrice));

            Console.WriteLine("Reservation created successfully!");
            break;
        }
    }

    public double CalculateTotalPrice(double servicesAmount, double additionalServicesAmount)
    {
        return servicesAmount + additionalServicesAmount;
    }

    public int ChooseAdditionalService()
    {
        int additionalServiceIndex;
        bool isValidInput;


        Console.WriteLine("Choose the additional service:");
        for (int i = 0; i < AdditionalServices.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {AdditionalServices[i]} - ${AdditionalServicesPrice[i]}/day");
        }


        do
        {
            isValidInput = int.TryParse(Console.ReadLine(), out additionalServiceIndex);

            if (!isValidInput || !new[] { 1, 2, 3 }.Contains(additionalServiceIndex))
            {
                Console.WriteLine("Please enter a valid option (1,2,3):");
            }

        } while (!isValidInput || !new[] { 1, 2, 3 }.Contains(additionalServiceIndex));


        return additionalServiceIndex - 1;
    }

    public int ChooseCarType()
    {
        int CarTypeIndex;
        bool isValidCarType;

        Console.WriteLine("Choose the number corresponding to the car type :");
        for (int i = 0; i < Services.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Services[i]} - ${ServicesPrice[i]}/day");
        }

        do
        {
            isValidCarType = int.TryParse(Console.ReadLine(), out CarTypeIndex);

            if (!isValidCarType || !new[] { 1, 2, 3 }.Contains(CarTypeIndex))
            {
                Console.WriteLine("Please enter a valid option (1,2,3):");
            }
        } while (!isValidCarType || !new[] { 1, 2, 3 }.Contains(CarTypeIndex));

        return CarTypeIndex - 1;
    }

    public void AllReservations()
    {

        Console.WriteLine("----------- All Reservations ------------\n");
        if (reservations.Count == 0)
        {
            Console.WriteLine("No Reservation Found!\n");
        }
        else
        {
            foreach (var reservation in reservations)
            {
                Console.WriteLine("Reservation ID: " + reservation.ReservationID);
                Console.WriteLine("Customer ID: " + "XXX" + reservation.CustomerID.Substring(3));
                Console.WriteLine("Name: " + reservation.CustomerName);
                Console.WriteLine("Phone Number: " + reservation.CustomerPhoneNumber);
                Console.WriteLine("Customer Type: " + reservation.CustomerType);
                Console.WriteLine("Car Type: " + reservation.CarType.Split('-').First().Trim());
                Console.WriteLine("Additional Services: " + reservation.AdditionalServices.Split('-').First().Trim());
                Console.WriteLine("Total Price: $" + reservation.TotalPrice);
                Console.WriteLine("----------------------------------------------");
            }
        }
    }

    public void ClearAllReservations()
    {
        reservations.Clear();
        Console.WriteLine("All reservations cleared successfully.\n");
    }

    public void Initialize()
    {
        //Create Customer
        Customer customer1 = new Customer("CHE123", "Cherryl Patel", "5197601596", CustomerType.VIP);
        Customer customer2 = new Customer("HAR110", "Harit Thoriya", "5191232882", CustomerType.Premium);
        Customer customer3 = new Customer("RAJ001", "Raj Patel", "5191237777", CustomerType.Regular);

        //Create Reservation
        reservations.Add(new Reservation(1, customer1, "Luxury", "GPS Navigation", 20.00));
        reservations.Add(new Reservation(2, customer2, "Premium", "Chauffeur Service", 20.00));
        reservations.Add(new Reservation(3, customer3, "Economy", "GPS Navigation", 30.00));
    }

}
