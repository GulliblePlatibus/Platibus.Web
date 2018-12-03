namespace Platibus.Web.DataServices.Models.User
{
    public class UserNameAndSalary
    {
        public string name { get; set; }
        public int salary { get; set; }

        public UserNameAndSalary(string _name, int _salary)
        {
            name = _name;
            salary = _salary;
        }
    }
}