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
        private const string CRAFTING_RESOURCE = "CRAFTING_RESOURCE";

        private const string CHARACTER_PISITION_X = "CHARACTER_PISITION_X";
        private const string CHARACTER_PISITION_Y = "CHARACTER_PISITION_Y";
        private const string CHARACTER_PISITION_Z = "CHARACTER_PISITION_Z";

        public static string GetWoodKey() => WOOD;
        public static string GetStoneKey() => STONE;
        public static string GetCrystalKey() => CRYSTAL;
        public static string GetLumberKey() => LUMBER;
        public static string GetBrickKey() => BRICK;
        public static string GetCraftingResourceKey() => CRAFTING_RESOURCE;
        public static string GetLastBuiltIslandName() => LAST_BUILT_ISLAND_NAME;

        public static string GetCharacterPositionX() => CHARACTER_PISITION_X;
        public static string GetCharacterPositionY() => CHARACTER_PISITION_Y;
        public static string GetCharacterPositionZ() => CHARACTER_PISITION_Z;

    }
}