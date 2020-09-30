using System.Collections.Generic;

namespace Application.Interface
{
    public interface IWordFinder
    {
        IEnumerable<string> Find(IEnumerable<string> wordstream);

    }
}
