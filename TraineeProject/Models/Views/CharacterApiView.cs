namespace TraineeProject.Models.Views
{
    public class CharacterApiView
    {
        public CharacterApiView(Character character)
        {
            if (character.Private)
            {
                CharacterName = "Anonymous";
                WorldServer = "Private";
            }
            else
            {
                CharacterName = character.CharacterName;
                WorldServer = character.WorldServer;
            }
        }

        public string CharacterName { get; set; }
        public string WorldServer { get; set; }
    }
}
