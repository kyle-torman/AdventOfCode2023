public class CategoryMapper
{
    public required string Name {get; init;}
    private List<CategoryMap> _categoryMaps = new List<CategoryMap>();

    public void AddCategoryMap(CategoryMap categoryMap)
    {
        _categoryMaps.Add(categoryMap);
    }

    public List<Range> MapSourceCategoryRangeToDestinationCategoryRange(List<Range> sourceRange)
    {
        var unmappedRanges = new List<Range>();
        unmappedRanges.AddRange(sourceRange);
        
        var mappedRanges = new List<Range>();
        foreach(var categoryMap in _categoryMaps)
        {
            var workingRanges = unmappedRanges.ToArray();
            unmappedRanges.Clear();
            foreach(var unmappedRange in workingRanges)
            {
                var categoryRanges = categoryMap.MapSourceCategoryRangeToDestinationCategoryRange(unmappedRange);
                mappedRanges.AddRange(categoryRanges.Where(cr => cr.DestinationRange is not null).Select(cr => cr.DestinationRange)!);
                unmappedRanges.AddRange(categoryRanges.Where(cr => cr.DestinationRange is null).Select(cr => cr.SourceRange));
            }
        }

        mappedRanges.AddRange(unmappedRanges);
        return mappedRanges;
    }

    public long MapSourceCategoryToDestinationCategory(long sourceCategory)
    {
        foreach(var categoryMap in _categoryMaps)
        {
            var destinationCategory = categoryMap.MapSourceCategoryToDestinationCategory(sourceCategory);
            if(destinationCategory is not null)
            {
                return destinationCategory.Value;
            }
        }

        return sourceCategory;
    }

    public void Print()
    {
        Console.WriteLine(Name);
        foreach(var categoryMap in _categoryMaps)
        {
            Console.WriteLine($"{categoryMap.DestinationRangeStart} {categoryMap.SourceRangeStart} {categoryMap.RangeLength}");
        }
    }
}