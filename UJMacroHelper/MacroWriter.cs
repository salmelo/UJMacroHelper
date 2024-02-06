using System.Collections.Immutable;
using System.Text;

namespace UJMacroHelper;

public static class MacroWriter
{
    public const string DefaultTemplate = "&{template:default} {{name=@{character_name}}}";

    public static ImmutableArray<string> Traits { get; } = ["Mind", "Body", "Speed", "Will"];
    public static ImmutableArray<string> SkillDice { get; } = ["Species", "Type", "Career", "Expert"];
    public static ImmutableArray<string> Skills { get; } = ["Academics", "Athletics", "Craft", "Deceit", "Endurance", "Evasion", "Fighting", "Negotiation", "Observation", "Presence", "Questioning", "Shooting", "Tactics", "Transport"];

    public static string GetBasicMacroCode(int rollCount = 1, IEnumerable<(string label, string value)>? bonuses = null)
    {
        if (rollCount < 1) { return ""; }
        bonuses ??= [];

        var macro = new StringBuilder(DefaultTemplate);

        void NestQuery(string label, string value, int maxNest, int nestLevel)
        {
            macro.Append(Nestify('|', nestLevel));
            macro.Append(label);
            macro.Append(Nestify(',', nestLevel));
            macro.Append(Nestify(value, nestLevel + 1));
            if (nestLevel < maxNest)
            {
                NestMacro(maxNest, nestLevel + 1);
            }
        }

        void NestMacro(int maxNest, int nestLevel = 0, int rollNumber = 0)
        {
            macro.Append($"?{{Roll{rollNumber + 1}");
            if (rollNumber > 0)
            {
                macro.Append(Nestify('|', nestLevel));
            }

            foreach (var trait in Traits)
            {
                NestQuery(trait, GetTraitCode(trait), maxNest, nestLevel);
            }

            foreach (var skill in Skills)
            {
                var code = GetSkillCode(skill);
                NestQuery(skill, code, maxNest, nestLevel);
            }

            NestQuery("Assist", $"{{{{Assist {rollNumber + 1}=[[1d8]]}}}}", maxNest, nestLevel);
            NestQuery("Assist (Team Player)", $"{{{{Assist {rollNumber + 1}=[[1d12]]}}}}", maxNest, nestLevel);

            foreach (var (label, value) in bonuses!)
            {
                NestQuery(label, value, maxNest, nestLevel);
            }

            macro.Append(Nestify('}', nestLevel));
        }

        for (var i = 0; i < rollCount; i++)
        {
            NestMacro(0, 0, i);
        }

        return macro.ToString();

    }

    public static string GetTraitCode(string trait) => $$$$"""
                  {{{{{{trait}}}}=[[@{{{{{trait}}}}}]]}}
                  """;
    public static string Nestify(char character, int nestLevel) => nestLevel switch
    {
        <= 0 => new([character]),
        1 => character switch
        {
            '|' => "&#124;",
            '}' => "&#125;",
            ',' => "&#44;",
            _ => new([character]),
        },
        >= 2 => Nestify(character, nestLevel - 1).Replace("&", "&amp;")
    };

    public static string Nestify(this string query, int nestLevel)
    {
        if (nestLevel <= 0) return query;

        var sb = new StringBuilder(query);
        int callDepth = 0;
        for (int i = 0; i < sb.Length; i++)
        {
            var c = sb[i];
            if (c is '@' or '%' or '^')
            {
                callDepth++;
            }
            else if (c is '|' or '}' or ',')
            {
                if (c is '}' && callDepth > 0)
                {
                    callDepth--;
                    continue;
                }
                var rep = Nestify(c, nestLevel);
                sb.Remove(i, 1);
                sb.Insert(i, rep);
                i += rep.Length - 1;
            }
        }

        return sb.ToString();
    }

    public static string GetSkillCode(string skill)
    {
        var sb = new StringBuilder($$$$"""
                    {{{{{{skill}}}}=
                    """);
        var skillValue = skill is "Academics" or "Athletics"
          ? skill.Substring(0, skill.Length - 1) : skill;
        foreach (var die in SkillDice)
        {
            sb.Append($$$$"""
                      [[{{@{{{{{skillValue}}}}{{{{die}}}}}}[{{{{die}}}}],0d0}kh1]] 
                      """);
        }
        sb.Append("}}");
        return sb.ToString();
    }

    public static string NestifyMacro(this ReadOnlySpan<char> code)
    {
        var sb = new StringBuilder();
        var callDepth = 0;
        var queryDepth = 0;
        var openCount = 0;
        bool lastCall = false;
        bool lastQuery = false;

        foreach (var c in code)
        {
            string add = $"{c}";
            switch (c)
            {
                case '@' or '%' or '^':
                    lastCall = true;
                    break;
                case '?':
                    lastQuery = true;
                    break;
                case '{':
                    openCount++;
                    if (lastCall)
                    {
                        callDepth++;
                    }
                    if (lastQuery)
                    {
                        queryDepth++;
                    }
                    goto default;
                case '|' or ',':
                    add = Nestify(c, queryDepth - 1);
                    goto default;
                case '}':
                    openCount = Math.Max(openCount - 1, 0);
                    if (callDepth > 0)
                    {
                        callDepth--;
                        goto default;
                    }
                    
                    if (queryDepth > openCount)
                    {
                        queryDepth--;
                    }
                    add = Nestify(c, queryDepth);
                    goto default;
                default:
                    lastCall = false;
                    lastQuery = false;
                    break;
            }
            sb.Append(add);
        }
        return sb.ToString();
    }

    public static string NestifyMacro(this string code) => NestifyMacro((ReadOnlySpan<char>)code);
}
