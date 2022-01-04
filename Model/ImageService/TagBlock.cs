using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    [Serializable]
    public class TagBlock
    {
        public TagBlock(IList<TagDTO> tagDto, bool hasPrevious, bool hasNext)
        {
            TagDto = tagDto;
            HasPrevious = hasPrevious;
            HasNext = hasNext;
        }

        #region Properties

        public IList<TagDTO> TagDto { get; private set; }
        public bool HasPrevious { get; private set; }
        public bool HasNext { get; private set; }

        #endregion Properties

        public override int GetHashCode()
        {
            unchecked
            {
                int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + TagDto.GetHashCode();
                hash = hash * multiplier + HasPrevious.GetHashCode();
                hash = hash * multiplier + HasNext.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type?

            TagBlock target = (TagBlock)obj;

            return (this.TagDto == target.TagDto)
                && (this.HasPrevious == target.HasPrevious)
                && (this.HasNext == target.HasNext);
        }
    }
}