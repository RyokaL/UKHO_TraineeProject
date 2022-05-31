namespace TraineeProject.Models.Views
{
    public class CharacterApiView
    {
        public CharacterApiView(Character character, string userId = "")
        {
            Id = character.Id;
            if (character.Private && userId != character.UserId)
            {
                CharacterName = "Anonymous";
                WorldServer = "Private";
            }
            else
            {
                CharacterName = character.CharacterName;
                WorldServer = character.WorldServer;
                Private = character.Private;
            }
        }

        public int Id { get; set; }
        public string CharacterName { get; set; }
        public string WorldServer { get; set; }
        public bool? Private { get; set; }
    }
}
