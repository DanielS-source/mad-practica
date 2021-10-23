using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.FollowDao
{
    class FollowDao :
        GenericDaoEntityFramework<Follow, Int64>, IFollowDao
    {
    }
}
