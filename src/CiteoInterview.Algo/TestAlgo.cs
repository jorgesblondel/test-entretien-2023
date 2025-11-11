namespace CiteoInterview.Algo;

public class TestAlgo
{
    public IEnumerable<List<string>> Test1(IEnumerable<string> input)
    {
        return input
            .GroupBy(w => string.Concat(w.OrderBy(c => c)))
            .Select(g => g.ToList());
    }

    public IEnumerable<IEnumerable<int>> Test2(List<List<int>> input)
    {
        if (input is null) throw new ArgumentNullException(nameof(input));

        var presenceByListCount = new Dictionary<int, int>();
        foreach (var group in input)
        {
            foreach (var val in group.Distinct())
            {
                presenceByListCount[val] = presenceByListCount.TryGetValue(val, out var c) ? c + 1 : 1;
            }
        }

        foreach (var group in input)
        {
            yield return group
                .Where(v => presenceByListCount.TryGetValue(v, out var cnt) && cnt == 1)
                .ToList();
        }
    }

    public IEnumerable<IEnumerable<int>> Test3(int[] input, int sum)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        var result = new List<List<int>>();

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = i + 1; j < input.Length; j++)
            {
                for (int k = j + 1; k < input.Length; k++)
                {
                    if (input[i] + input[j] + input[k] == sum)
                    {
                        result.Add(new List<int> { input[i], input[j], input[k] });
                    }
                }
            }
        }
        return result;
    }

    public int Test4(string[] input)
    {
        throw new NotImplementedException();
    }
}