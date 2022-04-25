using System.Text.RegularExpressions;

const string testSentence = "  abc abd  abca AcD   abc ";

Print($"{nameof(testSentence)}: {testSentence}");
Print($"{nameof(NaiveWordCount)}: {NaiveWordCount()}");
Print($"{nameof(CountEveryWhiteSpaceCharacter)}: {CountEveryWhiteSpaceCharacter()}");
Print($"{nameof(CountEveryWhiteSpaceCharacterOldStyle)}: {CountEveryWhiteSpaceCharacterOldStyle()}");
Print($"{nameof(CountWhiteSpacesBlocks)}: {CountWhiteSpacesBlocks()}");
Print($"{nameof(SizeOfLongestWhiteSpacesBlockSize)}: {SizeOfLongestWhiteSpacesBlockSize()}");
Print($"{nameof(SplitIntoWordsOnAnySizedWhiteSpaceBlock)}: {SplitIntoWordsOnAnySizedWhiteSpaceBlock()}");
Print($"{nameof(SplitIntoWordsOnAnySizedWhiteSpaceBlockWithNoEmptyWords)}: {SplitIntoWordsOnAnySizedWhiteSpaceBlockWithNoEmptyWords()}");

int NaiveWordCount()
{
    return testSentence
        .Trim()
        .Split(' ')
        .Length;
}

int CountEveryWhiteSpaceCharacter()
{
    return testSentence.Count(char.IsWhiteSpace);
}

int CountEveryWhiteSpaceCharacterOldStyle()
{
    int count = 0;
    
    for (int i = 0; i < testSentence.Length; i++)
    {
        var s = testSentence.Substring(i, 1);
        
        if (s == " ") count++;
    }
    return count;
}

int CountWhiteSpacesBlocks()
{
    var result = Regex.Matches(testSentence, @"\s{1,}");

    //The { m,n}
    //expression requires the expression immediately prior to it match m to n times, inclusive.Only one limit is required.If the upper limit is missing, it means "m or more repetitions".
    //result contains flag if success, index in string sentence and length

    return result.Count;
}

int SizeOfLongestWhiteSpacesBlockSize()
{
    var result = Regex.Matches(testSentence, @"\s{1,}")
        .Select(r => r.Length)
        .Max();

    return result;
}

string SplitIntoWordsOnAnySizedWhiteSpaceBlock()
{
    var result = Regex.
        Split(testSentence, @"\s{1,}");

    return string.Join('|', result);
}

string SplitIntoWordsOnAnySizedWhiteSpaceBlockWithNoEmptyWords()
{
    var result = Regex.
        Split(testSentence, @"\s{1,}")
        .ToList()
        .Where(s => !string.IsNullOrEmpty(s));
        //.Count();//number of nonempty words separated by empty blocks of spaces of any size

    return string.Join('|', result);
}

void Print(string text)
{
    Console.WriteLine(text);
}

//in operating on strings we may consider using:
//Regex
//Linq
//StringBuilder
//string methods