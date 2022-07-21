using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeProject.Tests.Integration
{
    internal class UserIdProvider : IUserIdProvider
    {
        public Guid UserId { get; set; }
    }
}
