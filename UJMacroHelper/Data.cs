﻿using System.Collections.Immutable;

using TraitData = System.Collections.Immutable.ImmutableArray<(string Name, System.Collections.Immutable.ImmutableArray<string> Skills, System.Collections.Immutable.ImmutableArray<string> Gifts)>;
using static UJMacroHelper.Skills;

namespace UJMacroHelper;

public static class Data
{
    public static TraitData Species { get; } = [
        ("", [], []),
        ("Alligator", [Endurance, Fighting, Observation], ["Brawling", "Swimming"]),
        ("Anteater", [Athletics, Fighting, Observation], ["Brawling", "Coward"]),
        ("Amradillo", [Athletics, Endurance, Presence], ["Coward", "Jumping"]),
        ("Badger", [Endurance, Fighting, Presence], ["Brawling", "Stealth"]),
        ("Bat", [Athletics, Evasion, Observation], ["Flight", "Stealth"]),
        ("Bear", [Endurance, Fighting, Presence], ["Giant", "Wrestling"]),
        ("Boar", [Endurance, Fighting, Presence], ["Brawling", "Tracking"]),
        ("Cat", [Athletics, Evasion, Observation], ["Acrobat", "Brawling"]),
        ("Cattle", [Athletics, Endurance, Observation], ["Brawling", "Giant"]),
        ("Cheetah", [Athletics, Fighting, Observation], ["Brawling", "Tracking"]),
        ("Coyote", [Athletics, Fighting, Observation], ["Brawling", "Tracking"]),
        ("Crocodile", [Deceit, Endurance, Fighting], ["Brawling", "Swimming"]),
        ("Deer", [Athletics, Evasion, Observation], ["Coward", "Running"]),
        ("Dog", [Athletics, Observation, Tactics], ["Brawling", "Tracking"]),
        ("Donkey", [Endurance, Fighting, Presence], ["Brawling", "Coward"]),
        ("Elephant", [Athletics, Endurance, Presence], ["Brawling", "Giant"]),
        ("Ferret", [Athletics, Evasion, Observation], ["Contortionist", "Coward"]),
        ("Fox", [Athletics, Evasion, Observation], ["Coward", "Danger Sense"]),
        ("Gecko", [Athletics, Deceit, Evasion], ["Climbing", "Coward"]),
        ("Goat", [Endurance, Fighting, Presence], ["Brawling", "Climbing"]),
        ("Horse", [Athletics, Endurance, Tactics], ["Giant", "Running"]),
        ("Jackal", [Athletics, Endurance, Observation], ["Brawling", "Stealth"]),
        ("Lion", [Fighting, Presence, Tactics], ["Brawling", "Stealth"]),
        ("Monkey", [Craft, Observation, Tactics], ["Climbing", "Dexterity"]),
        ("Mouse", [Athletics, Evasion, Observation], ["Contortionist", "Coward"]),
        ("Otter", [Athletics, Evasion, Observation], ["Contortionist", "Swimming"]),
        ("Panther", [Evasion, Fighting, Observation], ["Brawling", "Tracking"]),
        ("Pigeon", [Athletics, Evasion, Observation], ["Coward", "Flight"]),
        ("Porcupine", [Evasion, Observation, Presence], ["Coward", "Quills"]),
        ("Possum", [Athletics, Deceit, Evasion], ["Climbing", "Coward"]),
        ("Rabbit", [Athletics, Evasion, Observation], ["Coward", "Jumping"]),
        ("Raccoon", [Athletics, Evasion, Observation], ["Climbing", "Dexterity"]),
        ("Rat", [Athletics, Evasion, Observation], ["Brawling", "Contortionist"]),
        ("Rhinoceros", [Endurance, Fighting, Observation], ["Brawling", "Giant"]),
        ("Shrew", [Athletics, Fighting, Presence], ["Brawling", "Stealth"]),
        ("Skunk", [Athletics, Evasion, Presence], ["Acrobat", "Spray"]),
        ("Sloth", [Athletics, Deceit, Presence], ["Climbing", "Stealth"]),
        ("Snake", [Athletics, Deceit, Evasion], ["Contortionist", "Wrestling"]),
        ("Sparrow", [Athletics, Evasion, Observation], ["Flight", "Singing"]),
        ("Tiger", [Evasion, Fighting, Observation], ["Brawling", "Climbing"]),
        ("Weasel", [Athletics, Evasion, Observation], ["Brawling", "Contortionist"]),
        ("Wolf", [Athletics, Observation, Tactics], ["Brawling", "Tracking"]),
    ];

    public static TraitData Types { get; } = [
        ("", [], []),
        ("Angel", [Endurance, Negotiation, Questioning], ["Noncombatant"]),
        ("Authority", [Observation, Presence, Questioning], ["Legal Authority"]),
        ("Boss", [Negotiation, Presence, Tactics], ["Entourage"]),
        ("Broken", [Endurance, Evasion, Presence], ["Noncombatant"]),
        ("Crooked", [Deceit, Negotiation, Questioning], ["Leadership"]),
        ("Drifter", [Evasion, Observation, Transport], []),
        ("Egghead", [Academics, Craft, Transport], ["Noncombatant"]),
        ("Famous", [Deceit, Presence, Tactics], ["Leadership"]),
        ("Hard-Boiled", [Endurance, Presence, Shooting], []),
        ("Heart-of-Gold", [Observation, Presence, Tactics], ["Bodyguard"]),
        ("Knight", [Endurance, Presence, Tactics], ["Bodyguard"]),
        ("Loser", [Deceit, Evasion, Observation], []),
        ("Lucky", [Athletics, Craft, Evasion], ["Luck"]),
        ("Old", [Academics, Craft, Tactics], ["Leadership"]),
        ("Partner", [Observation, Presence, Tactics], ["Ally"]),
        ("Quiet", [Evasion, Observation, Presence], ["Noncombatant"]),
        ("Rebel", [Fighting, Presence, Shooting], []),
        ("Rich", [Academics, Presence, Transport], ["Wealth"]),
        ("Sultry", [Deceit, Negotiation, Presence], ["Leadership"]),
        ("Young", [Athletics, Evasion, Observation], []),
    ];

    public static TraitData Careers { get; } = [
        ("", [], []),
        ("Actor", [Deceit, Observation, Presence], ["Performance", "Team Player"]),
        ("Agitator", [Academics, Presence, Tactics], ["Guts", "Leadership"]),
        ("Artisan", [Academics, Craft, Negotiation], ["Craft", "Specialty	Team Player"]),
        ("Athlete", [Athletics, Endurance, Fighting], ["Team Player", "Wrestling"]),
        ("Biker", [Endurance, Observation, Transport], ["Motorcycling", "Streetwise"]),
        ("Bootlegger", [Deceit, Evasion, Transport], ["Chemistry", "Streetwise"]),
        ("Brute", [Endurance, Fighting, Presence], ["Boxing", "Streetwise"]),
        ("Bureaucrat", [Academics, Negotiation, Observation], ["Bribery", "Research"]),
        ("Burglar", [Athletics, Craft, Evasion], ["Sabotage", "Streetwise"]),
        ("Celebrity", [Deceit, Negotiation, Presence], ["Entourage", "Leadership"]),
        ("Clergy", [Academics, Negotiation, Questioning], ["Diplomacy", "Leadership"]),
        ("Con Artist", [Deceit, Negotiation, Questioning], ["Disguise",	"Fast-Talk"]),
        ("Daredevil", [Athletics, Endurance, Transport], ["Guts", "Performance"]),
        ("Detective", [Deceit, Observation, Questioning], ["Gossip", "Streetwise"]),
        ("Dilettante", [Academics, Observation, Questioning], ["High Society", "Wealth"]),
        ("Doctor", [Academics, Observation, Questioning], ["Medicine", "Research"]),
        ("Explorer", [Academics, Athletics, Endurance], ["Geography", "Survival"]),
        ("Farmer", [Craft, Endurance, Observation], ["Survival", "Team Player"]),
        ("Firefighter", [Athletics, Endurance, Observation], ["Firefighting", "Team Player"]),
        ("Gambler", [Deceit, Observation, Questioning], ["Gossip", "Streetwise"]),
        ("Gangster", [Presence, Shooting, Tactics], ["Bullet Conservation", "Streetwise"]),
        ("Hoodlum", [Fighting, Presence, Tactics], ["Guts", "Streetwise"]),
        ("Hooker", [Deceit, Negotiation, Presence], ["Streetwise", "Team Player"]),
        ("Laborer", [Craft, Endurance, Observation], ["Carousing", "Team Player"]),
        ("Libertine", [Deceit, Presence, Tactics], ["Carousing", "High Society"]),
        ("Magician", [Deceit, Observation, Presence], ["Performance", "Sleight of Hand"]),
        ("Masked Vigilante", [Athletics, Evasion, Fighting], ["Disguise", "Guts"]),
        ("Mobster", [Negotiation, Presence, Shooting], ["Bullet Conservation", "Streetwise"]),
        ("Motorist", [Evasion, Observation, Transport], ["Driving", "Team Player"]),
        ("Musician", [Academics, Observation, Presence], ["Gossip", "Performance"]),
        ("Nurse", [Academics, Observation, Questioning], ["Medicine", "Team Player"]),
        ("Outlaw", [Evasion, Fighting, Shooting], ["Stealth", "Streetwise"]),
        ("Patrol", [Endurance, Presence, Tactics], ["Boxing", "Bullet Conservation"]),
        ("Politician", [Academics, Deceit, Negotiation], ["Bribery", "Diplomacy"]),
        ("Prize Fighter", [Endurance, Fighting, Presence], ["Boxing", "Guts"]),
        ("Professor", [Academics, Observation, Questioning], ["Geography", "Research"]),
        ("Reporter", [Observation, Questioning, Transport], ["Gossip", "Research"]),
        ("Safecracker", [Craft, Evasion, Observation], ["Demolitions", "Sabotage"]),
        ("Salesperson", [Deceit, Negotiation, Observation], ["Diplomacy", "Team Player"]),
        ("Servant", [Craft, Observation, Transport], ["Gossip", "Team Player"]),
        ("Singer", [Deceit, Observation, Presence], ["Singing", "Team Player"]),
        ("Soldier", [Fighting, Shooting, Tactics], ["Bullet", "Conservation	Team Player"]),
        ("Spy", [Deceit, Evasion, Observation], ["Disguise", "Gossip"]),
        ("Thief", [Athletics, Evasion, Observation], ["Sabotage", "Sleight of Hand"]),
        ("Torpedo", [Evasion, Observation, Shooting], ["Sneak Attack", "Streetwise"]),
        ("Tycoon", [Academics, Negotiation, Questioning], ["Diplomacy", "High Society"]),
        ("Vagrant", [Endurance, Negotiation, Observation], ["Streetwise", "Survival"]),
        ("Writer", [Academics, Observation, Questioning], ["Gossip", "Research"]),
    ];

    public static ImmutableHashSet<string> ConditionalD12s { get; } = [
        "Bribery",
        "Carousing",
        "Chemistry",
        "Climbing",
        "Coward",
        "Craft Specialty",
        "Demolitions",
        "Diplomacy",
        "Disguise",
        "Driving",
        "Fast-Talk",
        "Firefighting",
        "Geography",
        "Gossip",
        "Guts",
        "High Society",
        "Leadership",
        "Medicine",
        "Motorcycling",
        "Noncombatant",
        "Performance",
        "Personality",
        "Romance",
        "Running",
        "Research",
        "Sabotage",
        "Singing",
        "Stealth",
        "Streetwise",
        "Survival",
        "Swimming",
        "Team Player",
        "Tracking",
    ];
}
