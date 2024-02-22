namespace ObserverDesignPattern.Models
{
    public class EmailDto
    {
        public string Name { get; set; } = "Deneme";
        public string From { get; set; } = "no-reply@deneme.edu.tr";

        public string To { get; set; } = "ahmetsarikaya0696@gmail.com";
        public string Subject { get; set; } = "Deneme";
        public string Body { get; set; }
    }
}
