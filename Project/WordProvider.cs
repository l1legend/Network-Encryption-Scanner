// The Assessment namespace holds interfaces and implementations related to word list providers.
namespace Assessment
{
    // IWordsProvider is an interface that any class can implement 
    // to provide a list of words.
    public interface IWordsProvider
    {
        List<string> GetWordList();
    }

    // Words class is an implementation of the IWordsProvider interface.
    // Currently, it returns an empty list of words.
    public class Words : IWordsProvider
    {
        public List<string> GetWordList()
        {
            return new List<string>();
        }
    }
}
