namespace Infrastructure.Common.Email;

public class EmailOptions
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public bool UseDefaultCredentials { get; set; }
}