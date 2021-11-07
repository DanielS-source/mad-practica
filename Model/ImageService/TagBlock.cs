using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    [Serializable]
    public class TagBlock
    {
        public long TagId { get; private set; }
        public string Name { get; private set; }
        public long Uses { get; private set; }
        public IList<TagBlock> TagsBlock { get; private set; }
        public bool HasPrevious { get; private set; }
        public bool HasNext { get; private set; }

        public TagBlock(long tagId, string name)
        {
            TagId = tagId;
            Name = name;
        }

        public TagBlock(long tagId, string name, long uses)
        {
            TagId = tagId;
            Name = name;
            Uses = uses;
        }

        public TagBlock(IList<TagBlock> tagBlock, bool hasPrevious, bool hasNext)
        {
            TagsBlock = tagBlock;
            HasPrevious = hasPrevious;
            HasNext = hasNext;
        }

    }
}
