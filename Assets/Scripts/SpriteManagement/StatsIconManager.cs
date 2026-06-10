public class StatsIconManager : IconManager
{
    void Start()
    {
        GameManager.Instance.statsIconManager = this;
    }
}