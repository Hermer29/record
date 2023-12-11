namespace Record.Model;

public class FeedbackRequests
{
    private static List<(int hours, int minutes)> _requests = new()
    {
        (13, 00),
        (13, 02),
        (14, 30)
    };

    public void AddTime(int hours, int minutes)
    {
        _requests.Add((hours, minutes));
    }

    public IEnumerable<(int hours, int minutes)> GetTimes() => _requests;
}