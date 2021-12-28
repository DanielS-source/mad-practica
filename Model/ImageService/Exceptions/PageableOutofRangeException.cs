using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService.Exceptions
{
    [Serializable]
    public class PageableOutofRangeException : Exception
    {
        public PageableOutofRangeException(int pageNumber) : base("Page number was outsie the bounds of the page in: " + pageNumber)
        {
            PageNumber = pageNumber;
        }

        public int PageNumber { get; private set; }
    }
}