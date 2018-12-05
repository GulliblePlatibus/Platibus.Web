namespace Platibus.Web.DataServices.Models.User
{
    public class UserNameAndSalary
    {
        public string name { get; set; }
        public double salary { get; set; }

        public UserNameAndSalary(string _name, double _salary)
        {
            name = _name;
            salary = _salary;
        }
    }
}