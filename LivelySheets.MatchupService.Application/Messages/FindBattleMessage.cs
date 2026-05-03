namespace LivelySheets.MatchupService.Application.Messages;

public class FindBattleMessage
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}
