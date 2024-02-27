using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UJMacroHelper;

public static class MacroWriter
{
    public const string DefaultTemplate = "&{template:default} {{name=@{character_name}}}";

    public static ImmutableArray<string> Traits { get; } = ["Mind", "Body", "Speed", "Will"];
    public static ImmutableArray<(string name, string label)> SkillDice { get; } = [
        ("Species", "@{SpeciesName}"),
        ("Type", "@{TypeName}"),
        ("Career", "@{CareerName}"),
        ("Expert", "Expert")
        ];
    public static ImmutableArray<string> Skills { get; } = ["Academics", "Athletics", "Craft", "Deceit", "Endurance", "Evasion", "Fighting", "Negotiation", "Observation", "Presence", "Questioning", "Shooting", "Tactics", "Transport"];

    public static string GetBasicMacroCode(int rollCount = 1, IEnumerable<(string label, string value)>? bonuses = null, bool nestify = true)
    {
        if (rollCount < 1) { return ""; }
        
        var macro = GetBasicMacroStringBuilder(rollCount, bonuses);
        return nestify ? macro.ToString().NestifyMacro() : macro.ToString();
    }

    public static StringBuilder GetBasicMacroStringBuilder(int rollCount = 1, IEnumerable<(string label, string value)>? bonuses = null)
    {
        if (rollCount < 1) { return new(); }
        bonuses ??= [];

        var macro = new StringBuilder(DefaultTemplate);

        void NestQuery(string label, string value, int maxNest, int nestLevel)
        {
            macro.Append('|');
            macro.Append(label);
            macro.Append(',');
            macro.Append(value);
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
                macro.Append('|');
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

            NestQuery("Assist", $"{{{{Assist={GetRepeatCountQuery("Assist d8s", "[[1d8]]", 4)}}}}}", maxNest, nestLevel);
            NestQuery("Assist (Team Player)", $"{{{{Assist (TP)={GetRepeatCountQuery("Assist d12s", "[[1d12]]", 4)}}}}}", maxNest, nestLevel);

            foreach (var (label, value) in bonuses!)
            {
                NestQuery(label, value, maxNest, nestLevel);
            }

            macro.Append('}');
        }

        for (var i = 0; i < rollCount; i++)
        {
            NestMacro(0, 0, i);
        }
        return macro;
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
        foreach (var (die, label) in SkillDice)
        {
            sb.Append($$$$"""
                      [[{{@{{{{{skillValue}}}}{{{{die}}}}}}[{{{{label}}}}],0d0}kh1]] 
                      """);
        }
        sb.Append("}}");
        return sb.ToString();
    }

    public static string NestifyMacro(this ReadOnlySpan<char> code)
    {
        var sb = new StringBuilder();
        var callDepth = 0;

        bool lastWasCall = false;
        bool lastWasQuery = false;

        var opens = new Stack<int>();
        var queries = new Stack<int>();

        for (int i = 0; i < code.Length; i++)
        {
            var c = code[i];
            string add = $"{c}";

            int lastOpen;
            int lastQuery;

            switch (c)
            {
                case '@' or '%' or '^':
                    lastWasCall = true;
                    break;
                case '?':
                    lastWasQuery = true;
                    break;
                case '{':
                    opens.Push(i);
                    if (lastWasCall)
                    {
                        callDepth++;
                    }
                    if (lastWasQuery)
                    {
                        queries.Push(i);
                    }
                    goto default;
                case '|' or ',':

                    if (!opens.TryPeek(out lastOpen)) { lastOpen = -1; }
                    if (!queries.TryPeek(out lastQuery)) { lastQuery =  -1; }

                    if (lastOpen > lastQuery)
                    {
                        add = Nestify(c, queries.Count);
                    }
                    else
                    {
                        add = Nestify(c, queries.Count - 1);
                    }
                    goto default;
                case '}':
                    if (opens.TryPop(out lastOpen)) { }
                    if (queries.TryPeek(out lastQuery)) { }

                    if (callDepth > 0)
                    {
                        callDepth--;
                        goto default;
                    }

                    if (lastQuery >= lastOpen)
                    {
                        _ = queries.Pop();
                    }
                    add = Nestify(c, queries.Count);
                    goto default;
                default:
                    lastWasCall = false;
                    lastWasQuery = false;
                    break;
            }
            sb.Append(add);
        }
        return sb.ToString();
    }

    public static string NestifyMacro(this string code) => NestifyMacro((ReadOnlySpan<char>)code);

    public static string GetRepeatCountQuery(string query, string code, int maxCount)
    {
        var sb = new StringBuilder($"?{{{query}");

        for (int i = 0; i < maxCount; i++)
        {
            sb.Append($"|{i + 1},");
            sb.Append(string.Join(" ", Enumerable.Repeat(code, i + 1)));
        }
        sb.Append('}');

        return sb.ToString();
    }

    public static string UseSelected(this string code) => code.Replace("@{", "@{selected|").Replace("character_name","token_name");
}
