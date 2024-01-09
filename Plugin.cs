using RankedUtils.EloViewer;
using SML;

namespace RankedUtils;

[Mod.SalemMod]
public class Main
{
    public static void Start()
    {
        System.Console.WriteLine("[RankedUtils] RankedSnobs");
        State.Init();
    }
}

[Mod.SalemMenuItem]
public class SalemMenuButtons
{
    public static Mod.SalemMenuButton menuButton1 = new()
    {
        Label = "General Stats",
        Icon = State.sprite,
        OnClick = () => Buttons.ShowStatsMessage("General Stats", State.userStatistics.getGeneralStatisticsString())
    };
    public static Mod.SalemMenuButton menuButton2 = new()
    {
        Label = "Ranked Stats",
        Icon = State.sprite,
        OnClick = () => Buttons.ShowStatsMessage("Ranked Stats", State.userStatistics.getRankedInformationString())
    };
}