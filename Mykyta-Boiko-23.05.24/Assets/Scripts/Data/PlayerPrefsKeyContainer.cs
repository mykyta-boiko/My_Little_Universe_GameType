namespace Data
{
    public static class PlayerPrefsKeyContainer
    {
        private const string WOOD = "WOOD";
        private const string STONE = "STONE";
        private const string CRYSTAL = "CRYSTAL";
        private const string LUMBER = "LUMBER";
        private const string BRICK = "BRICK";
        private const string LAST_BUILT_ISLAND_NAME = "LAST_BUILT_ISLAND_NAME";

        public static string GetWoodKey() => WOOD;
        public static string GetStoneKey() => STONE;
        public static string GetCrystalKey() => CRYSTAL;
        public static string GetLumberKey() => LUMBER;
        public static string GetBrickKey() => BRICK;

        public static string GetLastBuiltIslandName() => LAST_BUILT_ISLAND_NAME;
    }
}