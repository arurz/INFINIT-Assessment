namespace INFINIT.ServiceLayer.LetterFrequencyCountProvider
{
    public interface ILetterFrequencyCounter
    {
        public Task<Dictionary<char, int>> CountLetterFrequenciesAsync(List<string> fileContents);
    }
}
