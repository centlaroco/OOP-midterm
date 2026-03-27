using System;

namespace HotelD2
{
    // Abstract base class 
    abstract class Room
    {
        // private fields (encapsulation)
        private int roomNumber;
        private double price;
        private bool nakaBooked; // tracks booking status

        // public properties (getters and setters)
        public int RoomNumber{
            get{return roomNumber;}
            set{roomNumber= value;}
        }

        public double Price{
            get{return price;}
            set{price= value;}
        }

        public bool NakaBooked{
            get{return nakaBooked;}
            set{nakaBooked= value;}
        }

        // automated properties of user infos
        public string Name{get; set;}
        public int Age{get; set;}
        public string Address{get; set;}

        // Constructor to initialize room details
        public Room(int RoomNumber, double Price){
            roomNumber=RoomNumber;
            price=Price;
            nakaBooked=false; // default:  if the availability is not booked
        }

        // Static constructor (runs once when program starts)
        static Room(){
            Console.WriteLine("\n===WELCOME TO HOTEL 2D===");
        }

        // Method to book a room
        public void BookARoom()
        {
            if(!nakaBooked){
                nakaBooked=true;

                // Display booking details
                Console.WriteLine("\n\t===Booking Details===");
                Console.WriteLine($"Name: {Name}");
                Console.WriteLine($"Age: {Age}");
                Console.WriteLine($"Address: {Address}");
                Console.WriteLine($"Room Number: {roomNumber}");
                Console.WriteLine($"Room Type: {DisplayRoomType()}");
                Console.WriteLine($"Room {roomNumber} booked successfully.");
                Console.WriteLine("============================");
            }
            else{
                Console.WriteLine($"Room {roomNumber} is already Booked.");
            }
        }

        // Method to cancel booking
        public void CancelBooking()
        {
            if(nakaBooked)
            {
                nakaBooked=false;
                Console.WriteLine($"Cancelled Book: {roomNumber}");
            }
            else{
                Console.WriteLine($"Room {roomNumber} is not booked.");
            }
        }

        // Abstract method (implemented by derived classes) defines the room type
        public abstract string DisplayRoomType();

        // Virtual method it could be overriden
        public virtual void DisplayHotelInfo()
        {
            Console.WriteLine($"{DisplayRoomType()} | Room: {roomNumber} | Price: {price} | Available: {!nakaBooked} ");
        }
    }

    // Different room types (inheritance + polymorphism)
    class SkwaaMoor: Room
    {
        public SkwaaMoor(int RoomNumber, double Price): base(RoomNumber, Price){}

        public override string DisplayRoomType()
        {
            return "Skwaa Room";
        }
    }

    class ChakaMoor: Room
    {
        public ChakaMoor(int RoomNumber, double Price): base(RoomNumber,Price){}

        public override string DisplayRoomType()
        {
            return "Chaka Room";
        }
    }

    class NormalMoor: Room
    {
        public NormalMoor(int RoomNumber, double Price): base(RoomNumber,Price){}

        public override string DisplayRoomType()
        {
            return "Normal Room";
        }
    }

    class LayshoMoor: Room
    {
        public LayshoMoor(int RoomNumber, double Price): base(RoomNumber,Price){}

        public override string DisplayRoomType()
        {
            return "Laysho Room";
        }
    }

    class DeluxeMoor: Room
    {
        public DeluxeMoor(int RoomNumber, double Price): base(RoomNumber,Price){}

        public override string DisplayRoomType()
        {
            return "Deluxe Room";
        }
    }

    // Abstract Payment class (demonstrates abstraction)
    abstract class Payment
    {
        public abstract void Pay(double amount);
    }

    // cash payment implementation 
    class CashPayment: Payment
    {
        public override void Pay(double amount)
        {
            Console.WriteLine($"Received {amount} in cash");
            Console.WriteLine("Thanks for paying in cash");
        }
    }

    // credit payment implementation
    class CreditPayment: Payment
    {
        public override void Pay(double amount)
        {
            Console.WriteLine($"Received {amount} in credit");
            Console.WriteLine("Thanks for paying in Credit");
        }
    }

    //main program
    class Program
    {
        static void Main()
        {
            // create an array of Room objects
            Room[] rooms=new Room[5];

            // initialize rooms with different types and prices
            rooms[0]= new SkwaaMoor(101,500);
            rooms[1]= new ChakaMoor(103,1000);
            rooms[2]= new NormalMoor(201,2500);
            rooms[3]= new LayshoMoor(302,5500);
            rooms[4]= new DeluxeMoor(403,11000);

            int pili=0; // user choice

            // Main menu while loop
            while(pili!=5)
            {
                Console.WriteLine("\n++Book a Hotel++");
                Console.WriteLine("\t(1) Display Rooms");
                Console.WriteLine("\t(2) Book a Room");
                Console.WriteLine("\t(3) Cancel Booking");
                Console.WriteLine("\t(4) User Info");
                Console.WriteLine("\t(5) Exit");

                Console.Write("Enter Number: ");
                pili=Convert.ToInt32(Console.ReadLine());

                // display all rooms
                if(pili==1)
                {
                    for(int i=0;i<rooms.Length;i++)
                    {
                        rooms[i].DisplayHotelInfo();
                    }
                }

                // Book a room
                else if(pili==2)
                {
                    Console.WriteLine("\n***BOOK A ROOM***");
                    Console.Write("Enter Room Number: ");
                    int roomNum=Convert.ToInt32(Console.ReadLine());

                    bool foundRoom=false;

                    // Search for the room
                    for(int j=0;j<rooms.Length;j++)
                    {
                        if(rooms[j].RoomNumber==roomNum){

                            // Collect user info name age and address
                            Console.Write("Enter your name: ");
                            string name=Console.ReadLine();

                            Console.Write("Enter your age: ");
                            int age=Convert.ToInt32(Console.ReadLine());

                            // Age validation
                            if(age<18)
                            {
                                Console.WriteLine("You are still a minor");
                                break;
                            }

                            Console.Write("Enter your address: ");
                            string address=Console.ReadLine();

                            // Payment selection
                            Console.WriteLine("Payment Method");
                            Console.WriteLine("\t(1)Cash");
                            Console.WriteLine("\t(2)Credit");
                            Console.Write("Choose payment: ");
                            int payChoice=Convert.ToInt32(Console.ReadLine());

                            Payment payment=null;
                            string paymentType= "";

                            // polymorphism in action
                            if(payChoice==1){
                                payment= new CashPayment();
                                paymentType="Cash";
                            }
                            else if(payChoice==2){
                                payment= new CreditPayment();
                                paymentType="Credit";
                            }
                            else{
                                Console.WriteLine("Invalid payment choice");
                            }

                            // Assign user info to the room
                            rooms[j].Name=name;
                            rooms[j].Age=age;
                            rooms[j].Address=address;

                            // Book the room
                            rooms[j].BookARoom();

                            // Process payment
                            if(payment!=null){
                                Console.WriteLine($"Payment: {paymentType}");
                                payment.Pay(rooms[j].Price);
                            }

                            foundRoom=true;
                            break;
                        }
                    }

                    // If room not found
                    if(!foundRoom){
                        Console.WriteLine("Room not found. Please try again");
                    }
                }

                // Cancel booking
                else if(pili==3)
                {
                    Console.WriteLine("Enter Room Number to Cancel: ");
                    int cancelRoom=Convert.ToInt32(Console.ReadLine());

                    bool found=false;

                    for(int x=0;x<rooms.Length;x++)
                    {
                        if(rooms[x].RoomNumber==cancelRoom)
                        {
                            rooms[x].CancelBooking();
                            found=true;
                            break;
                        }
                    }

                    if(!found)
                    {
                        Console.WriteLine("Room not found");
                    }
                }

                // Display all booked users
                else if(pili==4)
                {
                    Console.WriteLine("\n===USER INFORMATION===");

                    bool hasBooked=false;

                    for(int a=0;a<rooms.Length;a++)
                    {
                        if(rooms[a].NakaBooked)
                        {
                            Console.WriteLine("\n------------------------");
                            Console.WriteLine($"Name: {rooms[a].Name}");
                            Console.WriteLine($"Age: {rooms[a].Age}");
                            Console.WriteLine($"Address: {rooms[a].Address}");
                            Console.WriteLine($"Room Number: {rooms[a].RoomNumber}");
                            Console.WriteLine($"Room Type: {rooms[a].DisplayRoomType()}");
                            hasBooked = true;
                        }
                    }

                    if(!hasBooked)
                    {
                        Console.WriteLine("No users have booked yet");
                    }
                }

                // Exit program
                else if(pili==5)
                {
                    Console.WriteLine("Bye thanks for Booking a Room");
                }

                // Invalid input
                else{
                    Console.WriteLine("Invalid");
                }
            }
        }
    }
}