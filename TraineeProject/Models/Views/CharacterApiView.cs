namespace TraineeProject.Models.Views
{
    public class CharacterApiView
    {
        public CharacterApiView(Character character)
        {
            CharacterName = character.CharacterName;
            WorldServer = character.WorldServer;
        }

        public string CharacterName { get; set; }
        public string WorldServer { get; set; }
    }
}
