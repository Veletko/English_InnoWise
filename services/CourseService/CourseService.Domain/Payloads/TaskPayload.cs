namespace CourseService.Domain.Payloads;

using System.Text.Json.Serialization;

[JsonDerivedType(typeof(MultipleChoicePayload), typeDiscriminator: "multiple_choice")]
[JsonDerivedType(typeof(FillInBlankPayload), typeDiscriminator: "fill_in_blank")]
[JsonDerivedType(typeof(MatchPairsPayload), typeDiscriminator: "match_pairs")]
public abstract class TaskPayload
{
}

public class MultipleChoicePayload : TaskPayload
{
    public required string Question { get; set; }
    public required List<string> Options { get; set; }
    public required List<int> CorrectOptionIndexes { get; set; }
}

public class FillInBlankPayload : TaskPayload
{
    public required string TextTemplate { get; set; }
    public required Dictionary<int, List<string>> CorrectAnswers { get; set; }
}

public class MatchPairsPayload : TaskPayload
{
    public required List<MatchingPair> Pairs { get; set; }
}

public class MatchingPair
{
    public required string Id { get; set; }
    public required string Left { get; set; }
    public required string Right { get; set; }
}
