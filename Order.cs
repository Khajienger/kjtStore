namespace kjtStore
{
    internal class Order
    {
        public Order(string eMail, int Code, string Title)
        {
            this.email = eMail;
            this.code = Code;
            this.title = Title;
        }

        public string GetEMail()
        {
            return email;
        }

        public int GetCode()
        {
            return code;
        }

        public string GetTitle()
        {
            return title;
        }

        private string email { get; set; }

        private int code { get; set; }

        private string title { get; set; }
    }
}