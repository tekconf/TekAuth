using System.Collections.Generic;

namespace Tekconf.DTO
{
    public class Speaker
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
        public List<Session> Sessions { get; set; }
    }
}