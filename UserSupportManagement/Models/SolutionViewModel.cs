using System.Collections.Generic;
using static UserSupportManagement.Constants.Permissions;

namespace UserSupportManagement.Models
{
    public class SolutionViewModel
    {
        public IList<Problem> Problems { get; set; }
        public IList<Solution> Solutions { get; set; }
    }
}
