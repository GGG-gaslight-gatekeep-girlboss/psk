namespace CoffeeShop.BusinessLogic.OrderManagement;

public static class Constants
{
    //TODO: Make an endpoint to change shop working hours
    public static readonly TimeOnly Open = new(8, 0, 0);   // 08:00 AM
    public static readonly TimeOnly Close = new(20, 0, 0); // 08:00 PM
}