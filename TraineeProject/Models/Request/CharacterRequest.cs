namespace TraineeProject.Models.Request
{
    public class CharacterRequest
    {
        public string CharacterName { get; set; }
        public string WorldServer { get; set; }
        public string? UserId { get; set; }
        public bool? Private { get; set; }

        public static Character convertToDbModel(CharacterRequest character)
        {
            return new Character
            {
                CharacterName = character.CharacterName,
                WorldServer = character.WorldServer,
                UserId = character.UserId,
                Private = character.Private ?? false
            };
        }
    }
}
