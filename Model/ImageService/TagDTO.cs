using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    [Serializable]
    public class TagDTO
    {
        public TagDTO(long tagId, string name)
        {
            TagId = tagId;
            Name = name;
        }

        public TagDTO(long tagId, string name, int uses)
        {
            TagId = tagId;
            Name = name;
            Uses = uses;
        }

        #region Properties

        public long TagId { get; private set; }
        public string Name { get; private set; }
        public int Uses { get; private set; }

        #endregion Properties

        public override int GetHashCode()
        {
            unchecked
            {
                int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + TagId.GetHashCode();
                hash = hash * multiplier + (Name == null ? 0 : Name.GetHashCode());
                hash = hash * multiplier + Uses.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type?

            TagDTO target = (TagDTO)obj;

            return (this.TagId == target.TagId)
                && (this.Name == target.Name)
                && (this.Uses == target.Uses);
        }

    }
}
