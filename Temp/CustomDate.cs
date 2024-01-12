using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LSS.Temp
{
    public class CustomDate
    {
        private const string DateFormat = "dd--MM-yyyy"; // Define the desired date format

    public DateTime DateValue { get; set; }

    // Constructor to initialize the date value
    public CustomDate(int day, int month, int year)
    {
        DateValue = new DateTime(year, month, day);
    }

    // Method to get the formatted date string
    public string GetFormattedDate()
    {
        return DateValue.ToString(DateFormat);
    }
    }
}