using Watchman.BusinessLogic.Models.Users;

namespace Watchman.Web.Models
{
    public class PersonalInfo : PersonalInformation
    {
        public PersonalInfo()
        {

        }

        public PersonalInfo(RegisterViewModel model)
        {
            Email = model.Email;
            Phone = model.Phone;
            BirthDay = model.BirthDay;
            FirstName = model.FirstName;
            SecondName = model.SecondName;
            LastName = model.LastName;
        }
    }
}
