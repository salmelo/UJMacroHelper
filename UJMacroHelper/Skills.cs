using System.Collections.Immutable;

namespace UJMacroHelper;

public static class Skills
{
    public const string Academics = "Academics";
    public const string Athletics = "Athletics";
    public const string Craft = "Craft";
    public const string Deceit = "Deceit";
    public const string Endurance = "Endurance";
    public const string Evasion = "Evasion";
    public const string Fighting = "Fighting";
    public const string Negotiation = "Negotiation";
    public const string Observation = "Observation";
    public const string Presence = "Presence";
    public const string Questioning = "Questioning";
    public const string Shooting = "Shooting";
    public const string Tactics = "Tactics";
    public const string Transport = "Transport";

    public readonly static ImmutableArray<string> AllSkills = [
        Academics,
        Athletics,
        Craft,
        Deceit,
        Endurance,
        Evasion,
        Fighting,
        Negotiation,
        Observation,
        Presence,
        Questioning,
        Shooting,
        Tactics,
        Transport
    ];
}