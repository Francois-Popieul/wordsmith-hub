namespace WordsmithHub.Domain;

public class TranslationLanguage
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; } // ISO 639-1: 2 letters
}
