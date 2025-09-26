using System.Reflection.Metadata;

namespace Inventory_Management_System.Models
{
    public class Warehouse : BaseEntity
    {
        //public string Name { get; set; }

        //public string Location { get; set; }
        //public string PhoneNumber { get; set; }

        public string Name { get; private set; }
        public string Location { get; private set; }
        public string PhoneNumber { get; private set; }

        private void SetWarehouseDetails(string name, string location, string phoneNumber)
        {
            Name = name;
            Location = location;
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
        }
        private static void ValidationFields(string name, string location, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name is Required");
            }
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new ArgumentNullException("Location is Required");
            }
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentNullException("Phone number is Required");
            }
        }
        private Warehouse(string name, string location, string phoneNumber)
        {

            ValidationFields(name, location, phoneNumber);
            SetWarehouseDetails(name, location, phoneNumber);
         
        }

        public static Warehouse Create(string name, string location, string phoneNumber)
        {
            return new Warehouse(name, location, phoneNumber);
        }

        public void Update(string name, string location, string phoneNumber)
        {
            SetWarehouseDetails(name, location, phoneNumber);
        }
    }
}
