public class SensorReport
{
    public List<ReportHistory> HistoryItems {get;}

    public SensorReport(List<ReportHistory> historyItems)
    {
        HistoryItems = historyItems;
    }

    public long SumOfExtrapolatedValues(bool isBackwards)
    {
        return HistoryItems.Sum(x => x.ExtrapolateNextValue(isBackwards));
    }

    public void Print()
    {
        foreach(var item in HistoryItems)
        {
            item.Print();
        }
    }
}