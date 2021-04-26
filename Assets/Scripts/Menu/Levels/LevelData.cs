[System.Serializable]
public class LevelData
{
    public string Name;

    public string Part1 => "Complete Part1";
    public string Part2 => "Complete Part2";
    public string Part3 => "Complete Part3";

    public bool IsPart1Complete = false;
    public bool IsPart2Complete = false;
    public bool IsPart3Complete = false;
}
