using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOTEL_MANAGER_SYSTEM
{
    public class Room
    {
        private int RoomNumber;
        private string RoomType;
        private int BedCount;
        private float Price;
        private bool IsBooked;

        public Room() { }
        public Room(int roomNumber, string roomType, int bedCount, float price, bool isBooked)
        {
            RoomNumber = roomNumber;
            RoomType = roomType;
            BedCount = bedCount;
            Price = price;
            IsBooked = isBooked;
        }

        public int roomnumber { get => RoomNumber; set => RoomNumber = value; }
        public string roomtype { get => RoomType; set => RoomType = value; }
        public int bedcount { get => BedCount; set => BedCount = value; }
        public float price { get => Price; set => Price = value; }
        public bool isBooked { get => IsBooked; set => IsBooked = value; }

        public string PrintDetails()
        {
            return "RoomNumber: " + roomnumber + "\nRoomType: " + roomtype + "\nBedCount: " + bedcount + "\nPrice: " + price + "\nIsBooked: " + isBooked;
        }
    }
}