namespace kjtStore
{
    internal class Client
    {
        private string firstName { get; set; }
        private string secondName { get; set; }
        private string patronymicName { get; set; }
        private string phone { get; set; }
        private string email { get; set; }

        public Client(string FirstName, string SecondName, string PatronymicName, string Phone, string eMail)
        {
            this.firstName = FirstName;
            this.secondName = SecondName;
            this.patronymicName = PatronymicName;
            this.phone = Phone;
            this.email = eMail;
        }

        public string GetFirstName()
        {
            return firstName;
        }

        public string GetSecondName()
        {
            return secondName;
        }

        public string GetPatronymicName()
        {
            return patronymicName;
        }

        public string GetPhone()
        {
            return phone;
        }

        public string GetEMail()
        {
            return email;
        }
    }
}