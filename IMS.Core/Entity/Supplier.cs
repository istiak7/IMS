namespace Inventory_Management_System.Models
{
    public class Supplier : BaseEntity
    {
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string ? Address { get; private set; }

        private void ValidationField(string name, string phone, string email, string address)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name is Required");
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentNullException("Location is Required");
            }
            if (string.IsNullOrEmpty(email)){
                throw new ArgumentNullException("Email is Required");
            }
            if (string.IsNullOrEmpty(address)){

                throw new ArgumentNullException("Phone number is Required");  
            }
            
            
        }

        private void SetSupplierDetails(string name, string phone, string email, string address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            CreatedAt = DateTime.UtcNow;
        }
        private void UpdateSupplierDetails(string name, string phone, string email, string address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        private Supplier(string name, string phone, string email, string address)
        {
            ValidationField(name, phone, email, address);
            SetSupplierDetails(name, phone, email, address);
        }

        public static Supplier Create(string name, string phone, string email, string address){
            return new Supplier(name, phone, email, address);
        }
        public void Update(string name, string phone, string email, string address)
        {
            UpdateSupplierDetails(name, phone, email, address);
        }
    }
}
