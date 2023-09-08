using System.Collections.Generic;

namespace Assessment
{
    public interface IWordsProvider
    {
        List<string> GetWordList();
    }

    public class WordsProvider : IWordsProvider
    {
        public List<string> GetWordList()
        {
            return new List<string>();
        }
    }
}
