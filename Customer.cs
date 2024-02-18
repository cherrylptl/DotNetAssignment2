enum CustomerType
{
    Regular,
    Premium,
    VIP,
}

class Customer
{
    public string CustomerID;
    public string CustomerName;
    public string CustomerPhoneNumber;
    public CustomerType CustomerType;

    public Customer(string id, string name, string phoneNumber, CustomerType customerType)
    {
        CustomerID = id;
        CustomerName = name;
        CustomerPhoneNumber = phoneNumber;
        CustomerType = customerType;
    }
}

class Reservation : Customer
{
    public int ReservationID;
    public string CarType;
    public string AdditionalServices;

    public double TotalPrice;

    public Reservation(int reservationID, Customer customer, string carType, string additionalServices, double totalPrice)
        : base(customer.CustomerID, customer.CustomerName, customer.CustomerPhoneNumber, customer.CustomerType)
    {
        ReservationID = reservationID;
        CarType = carType;
        AdditionalServices = additionalServices;
        TotalPrice = totalPrice;

    }
}
