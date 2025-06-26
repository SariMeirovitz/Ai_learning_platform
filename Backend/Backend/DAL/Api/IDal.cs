using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Api
{
    public interface IDal
    {
        IUser User { get; }
        ICategory Category { get; }
        ISubCategory SubCategory { get; }
        IPrompt Prompt { get; }
    }
}
