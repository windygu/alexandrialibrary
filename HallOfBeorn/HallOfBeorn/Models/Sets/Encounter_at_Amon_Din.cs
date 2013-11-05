using System;
using System.Collections.Generic;
using HallOfBeorn;
using HallOfBeorn.Models;

namespace HallOfBeorn.Models.Sets
{
    public class EncounteratAmonDin : CardSet
    {
        protected override void Initialize()
        {
            Cards.Add(new Card() {
                Title = "Pippin",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90021",
                IsUnique = true,
                CardType = CardType.Hero,
                Sphere = Sphere.Spirit,
                ThreatCost = 6,
                Willpower = 2,
                Attack = 1,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Hobbit." },
                Text = "If each hero you control has the Hobbit trait, Pippin gains: 'Response: After an enemy engages you, raise your threat by 3 to return it to the staging area. Until the end of the round, that enemy cannot engage you.'",
                Quantity = 1,
                Number = 1
            });
            Cards.Add(new Card() {
                Title = "Denethor",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90022",
                IsUnique = true,
                CardType = CardType.Ally,
                Sphere = Sphere.Leadership,
                ResourceCost = 4,
                Willpower = 3,
                Attack = 1,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Noble." },
                Text = "Denethor gets -1 Willpower for each damaged hero you control.Discard Denethor if his Willpower is 0 or less.",
                Quantity = 3,
                Number = 2
            });
            Cards.Add(new Card() {
                Title = "Lord of Morthond",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90023",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Leadership,
                ResourceCost = 1,
                Traits = new List<string>() { "Title." },
                Keywords = new List<string>() { "Attach to a Gondor or Outlands hero." },
                Text = "If each hero you control has a printed Leadership resource icon, Lord of Morthond gains: 'Response: After you play a Lore, Spirit, or Tactics ally, draw 1 card.'",
                Quantity = 3,
                Number = 3
            });
            Cards.Add(new Card() {
                Title = "Book of Eldacar",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90024",
                IsUnique = true,
                CardType = CardType.Attachment,
                Sphere = Sphere.Tactics,
                ResourceCost = 4,
                Traits = new List<string>() { "Record." },
                Keywords = new List<string>() { "Attach to a Ï hero." },
                Text = "Reduce the cost to play Book of Eldacar by 1 for each hero you control with a printed Tactics resource icon.Action: Discard Book of Eldacar to play any Tactics event card in your discard pile as if it were in your hand. Then, place that event on the bottom of your deck.",
                Quantity = 3,
                Number = 4
            });
            Cards.Add(new Card() {
                Title = "Gondorian Discipline",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90025",
                CardType = CardType.Event,
                Sphere = Sphere.Tactics,
                ResourceCost = 0,
                Traits = new List<string>() { "Gondor." },
                Text = "Response: Cancel up to 2 points of damage just dealt to a Gondor character.",
                Quantity = 3,
                Number = 5
            });
            Cards.Add(new Card() {
                Title = "Minas Tirith Lampwright",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90026",
                CardType = CardType.Ally,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Willpower = 0,
                Attack = 0,
                Defense = 1,
                HitPoints = 1,
                Traits = new List<string>() { "Gondor.", " Craftsman." },
                Text = "Response: After an encounter card with surge is revealed, discard Minas Tirith Lampwright to name enemy, location, or treachery. If the next encounter card revealed is the named type, discard it without resolving its effects.",
                Quantity = 3,
                Number = 6
            });
            Cards.Add(new Card() {
                Title = "Small Target",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90027",
                CardType = CardType.Event,
                Sphere = Sphere.Spirit,
                ResourceCost = 1,
                Text = "Response: After a Hobbit hero you control exhausts to defend an attack, choose another enemy engaged with you and reveal the attacking enemy's shadow card. If that shadow card has no shadow effect, resolve this enemy's attack against the chosen enemy. If that shadow card has a shadow effect, resolve this attack as normal.",
                Quantity = 3,
                Number = 7
            });
            Cards.Add(new Card() {
                Title = "Ithilien Archer",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90028",
                CardType = CardType.Ally,
                Sphere = Sphere.Lore,
                ResourceCost = 3,
                Willpower = 1,
                Attack = 2,
                Defense = 1,
                HitPoints = 2,
                Traits = new List<string>() { "Gondor.", " Ranger." },
                Keywords = new List<string>() { "Ranged." },
                Text = "Response: After Ithilien Archer attacks and damages an enemy, return that enemy to the staging area.",
                Quantity = 3,
                Number = 8
            });
            Cards.Add(new Card() {
                Title = "Ithilien Pit",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90029",
                CardType = CardType.Attachment,
                Sphere = Sphere.Lore,
                ResourceCost = 1,
                Traits = new List<string>() { "Trap." },
                Text = "Play Ithilien Pit into the staging area unattached.If unattached, attach Ithilien Pit to the next eligible enemy that enters the staging area.Any character may choose attached enemy as the target of an attack.",
                Quantity = 3,
                Number = 9
            });
            Cards.Add(new Card() {
                Title = "Hobbit-sense",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a90030",
                CardType = CardType.Event,
                Sphere = Sphere.Neutral,
                ResourceCost = 2,
                Keywords = new List<string>() { "Play only if each of your heroes is a Hobbit." },
                Text = "Combat Action: Enemies engaged with you do not attack this round. You cannot declare attacks this round.",
                Quantity = 3,
                Number = 10
            });
            Cards.Add(new Card() {
                Title = "Savagery of the Orcs - 1A",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93001",
                CardType = CardType.Quest,
                Text = "Setup: Set Ghulat aside, out of play. Put Lord Alcaron into play. Make Burning Farmhouse the active location. Add the Rescued Villagers and Dead Villagers objectives to the staging area. Then, shuffle the encounter deck and reveal 1 encounter card per player and add it to the staging area.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 1,
                Setup = "ttlss",
                Number = 11
            });
            Cards.Add(new Card() {
                Title = "Protect the Villagers - 1A",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93002",
                CardType = CardType.Quest,
                Text = "When Revealed: Add Ghulat to the staging area.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 1,
                Number = 12
            });
            Cards.Add(new Card() {
                Title = "Lord Alcaron",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93070",
                IsUnique = true,
                CardType = CardType.Objective,
                Willpower = 1,
                Attack = 2,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Gondor.", " Noble." },
                Keywords = new List<string>() { "The first player gains control of Lord Alcaron." },
                Text = "Response: After a villager token is discarded, exhaust Lord Alcaron to place that villager token on a location instead.If Lord Alcaron leaves play, the players have lost the game.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 1,
                Number = 13
            });
            Cards.Add(new Card() {
                Title = "Rescued Villagers",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93068",
                CardType = CardType.Objective,
                Text = "If a location leaves play as an explored location, move any villager tokens from that location to Rescued Villagers.At the end of the game, if there are more villager tokens here than damage tokens on Dead Vilagers, the players have won.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 1,
                Number = 14
            });
            Cards.Add(new Card() {
                Title = "Dead Villagers",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93069",
                CardType = CardType.Objective,
                Text = "If a villager token is discarded from a location, objective or quest stage, place a damage token on Dead Villagers.At the end of the game, if there are more damage tokens here than villager tokens on Rescued Villagers, the players have lost.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 1,
                Number = 15
            });
            Cards.Add(new Card() {
                Title = "Ghulat",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93071",
                IsUnique = true,
                CardType = CardType.Enemy,
                EngagementCost = 30,
                Threat = 3,
                Attack = 0,
                Defense = 3,
                HitPoints = 7,
                Traits = new List<string>() { "Orc.", " Uruk." },
                Keywords = new List<string>() { "X is the number of damage tokens on Dead Villagers." },
                Text = "Forced: When Ghulat attacks, place 1 damage token on Dead Villagers.While Ghulat is in play, the game cannot end.",
                EncounterSet = "Encounter at Amon Dîn",
                VictoryPoints = 2,
                Quantity = 1,
                Number = 16
            });
            Cards.Add(new Card() {
                Title = "Marauding Orc",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93072",
                CardType = CardType.Enemy,
                EngagementCost = 25,
                Threat = 2,
                Attack = 4,
                Defense = 1,
                HitPoints = 4,
                Traits = new List<string>() { "Orc." },
                Text = "Forced: After Marauding Orc attacks and destroys a character, place 1 damage token on Dead Villagers.",
                Shadow = "Shadow: If this attack destroys a character, discard 1 villager token from Rescued Villagers.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 4,
                Number = 17
            });
            Cards.Add(new Card() {
                Title = "Orc Ravager",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93073",
                CardType = CardType.Enemy,
                EngagementCost = 35,
                Threat = 2,
                Attack = 3,
                Defense = 1,
                HitPoints = 3,
                Traits = new List<string>() { "Orc." },
                Text = "When Revealed: Discard 1 villager token from the active location. If no villager token is discarded by this effect, Orc Ravager gains surge.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack. (+2 Attack instead if undefended.)",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 3,
                Number = 18
            });
            Cards.Add(new Card() {
                Title = "Craven Eagle",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93074",
                CardType = CardType.Enemy,
                EngagementCost = 40,
                Threat = 3,
                Attack = 5,
                Defense = 2,
                HitPoints = 3,
                Traits = new List<string>() { "Creature.", " Eagle." },
                Text = "When Revealed: Discard the character with the fewest remaining hit points. That character's controller may discard 3 cards at random from his hand to prevent this effect.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 2,
                Number = 19
            });
            Cards.Add(new Card() {
                Title = "Burning Farmhouse",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93075",
                CardType = CardType.Location,
                Threat = 1,
                QuestPoints = 5,
                Traits = new List<string>() { "Gondor." },
                Keywords = new List<string>() { "Villagers 4." },
                Text = "Forced: At the end of the round, discard 1 villager token from Burning Farmhouse.",
                EncounterSet = "Encounter at Amon Dîn",
                VictoryPoints = 1,
                Quantity = 4,
                Number = 20
            });
            Cards.Add(new Card() {
                Title = "Gondorian Hamlet",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93076",
                CardType = CardType.Location,
                Threat = 2,
                QuestPoints = 4,
                Traits = new List<string>() { "Gondor." },
                Keywords = new List<string>() { "Villagers 3." },
                Text = "While Gondorian Hamlet is in the staging area it gains: 'Forced: After a treachery card is revealed from the encounter deck, discard 1 villager token from Gondorian Hamlet.'",
                EncounterSet = "Encounter at Amon Dîn",
                VictoryPoints = 1,
                Quantity = 4,
                Number = 21
            });
            Cards.Add(new Card() {
                Title = "Secluded Farmhouse",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93077",
                CardType = CardType.Location,
                Threat = 3,
                QuestPoints = 3,
                Traits = new List<string>() { "Gondor." },
                Keywords = new List<string>() { "Villagers 2." },
                Text = "Travel: Reveal the top card of the encounter deck and add it to the staging area to travel here.",
                EncounterSet = "Encounter at Amon Dîn",
                VictoryPoints = 1,
                Quantity = 3,
                Number = 22
            });
            Cards.Add(new Card() {
                Title = "Burnt Homestead",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93078",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Surge." },
                Text = "When Revealed: Raise each player's threat by the number of damage tokens on Dead Villagers.",
                Shadow = "Shadow: Defending player raises his threat by 1 for each damage token on Dead Villagers.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 2,
                Number = 23
            });
            Cards.Add(new Card() {
                Title = "Trapped Inside",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93079",
                CardType = CardType.Treachery,
                Keywords = new List<string>() { "Doomed 2." },
                Text = "When Revealed: Discard 1 villager token from the active location for each player in the game. If no villager tokens were removed by this effect, Trapped Inside gains surge.",
                Shadow = "Shadow: If this attack destroys a character, discard 1 villager token from Rescued Villagers.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 2,
                Number = 24
            });
            Cards.Add(new Card() {
                Title = "Panicked!",
                Id = "fd89bdbf-7475-4f3e-96fc-8f5315a93080",
                CardType = CardType.Treachery,
                Text = "When Revealed: Each player must take a villager token from Rescued Villagers and place it on a location in the staging area, if able. If no villager tokens were placed on a location by this effect, Panicked! gains surge.",
                Shadow = "Shadow: Attacking enemy gets +1 Attack for each damage token on Dead Villagers.",
                EncounterSet = "Encounter at Amon Dîn",
                Quantity = 2,
                Number = 25
            });
        }
    }
}
