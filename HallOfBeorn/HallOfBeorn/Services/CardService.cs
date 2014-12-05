﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HallOfBeorn.Models;
using HallOfBeorn.Models.Decks.HallOfBeorn;
using HallOfBeorn.Models.Decks.TalesFromTheCards;
using HallOfBeorn.Models.Products;

namespace HallOfBeorn.Services
{
    public class CardService
    {
        public CardService()
        {
            LoadProducts();
            LoadDecks();
            //LoadRelationships();
            LoadScenarioCards();
            LoadCategories();

            /*
            Func<Card, bool> isAutoComplete = (card) =>
                {
                    if (card.IsUnique)
                        return true;

                    return (card.CardType == CardType.Hero || card.CardType == CardType.Ally || card.CardType == CardType.Attachment || card.CardType == CardType.Event || card.CardType == CardType.Objective || card.CardType == CardType.Objective_Ally);
                };

            Func<Card, string> getTitle = (card) =>
                {
                    return (!string.IsNullOrEmpty(card.NormalizedTitle)) ?
                        card.NormalizedTitle
                        : card.Title;
                };

            var autocomplete = string.Join(",", new HashSet<string>(cards.Values.Where(x => isAutoComplete(x)).Select(y => string.Format("\"{0}\"", getTitle(y))).ToList()));
            */
        }

        private readonly List<Product> products = new List<Product>();
        private readonly List<ProductGroup> productGroups = new List<ProductGroup>();
        private readonly List<CardSet> sets = new List<CardSet>();
        private readonly List<string> setNames = new List<string>();
        private readonly Dictionary<string, string> encounterSetNames = new Dictionary<string, string>();
        private readonly Dictionary<string, Card> cards = new Dictionary<string, Card>();
        private readonly Dictionary<string, string> keywords = new Dictionary<string, string>();
        private readonly Dictionary<string, string> traits = new Dictionary<string, string>();
        private readonly Dictionary<string, Deck> decks = new Dictionary<string, Deck>();
        private readonly Dictionary<string, Scenario> scenarios = new Dictionary<string, Scenario>();
        private readonly Dictionary<string, Category> categories = new Dictionary<string, Category>();
        private readonly Dictionary<byte, string> victoryPointValues = new Dictionary<byte, string>();

        const int MAX_RESULTS = 128;

        private void AddProduct(Product product)
        {
            products.Add(product);

            foreach (var cardSet in product.CardSets)
            {
                AddSet(product, cardSet);
            }
        }

        private void AddSet(Product product, CardSet cardSet)
        {
            sets.Add(cardSet);

            if (!string.IsNullOrEmpty(cardSet.Cycle) && !setNames.Contains(cardSet.Cycle.ToUpper()))
                setNames.Add(cardSet.Cycle.ToUpper());

            setNames.Add(cardSet.Name);

            var campaignMap = new Dictionary<string, Card>();

            foreach (var card in cardSet.Cards)
            {
                if (!string.IsNullOrEmpty(card.ScenarioTitle))
                {
                    var escapedTitle = card.ScenarioTitle.ToUrlSafeString();

                    if (card.CardType == CardType.Quest)
                    {
                        if (!scenarios.ContainsKey(escapedTitle))
                        {
                            var scenarioTitle = card.ScenarioTitle;
                            var scenarioNumber = card.ScenarioNumber;

                            var cycle = !string.IsNullOrEmpty(card.CardSet.Cycle) ? card.CardSet.Cycle : card.CardSet.Name;
                            
                            if (cycle == "NIGHTMARE")
                            {
                                var encounterSet = card.EncounterSet.Replace(" Nightmare", string.Empty);
                                scenarioTitle = scenarioTitle.Replace(" Nightmare", string.Empty);
                                var original = cards.Values.Where(x => x.CardType == CardType.Quest && x.EncounterSet == encounterSet && x.CardSet.Cycle != "NIGHTMARE").FirstOrDefault();
                                if (original != null)
                                {
                                    cycle = !string.IsNullOrEmpty(original.CardSet.Cycle) ? original.CardSet.Cycle : original.CardSet.Name;
                                }
                            }

                            var scenario = new Scenario { Title = scenarioTitle, GroupName = cycle, Number = scenarioNumber, RulesUrl = product.RulesUrl, ProductName = product.Name };
                            scenario.AddQuestCard(card);

                            scenarios.Add(escapedTitle, scenario);
                        }
                        else
                        {
                            scenarios[escapedTitle].AddQuestCard(card);
                        }
                    }
                    else if (card.CardType == CardType.Campaign)
                    {
                        campaignMap.Add(escapedTitle, card);
                    }
                }

                if (!string.IsNullOrEmpty(card.EncounterSet))
                {
                    if (!encounterSetNames.ContainsKey(card.EncounterSet))
                    {
                        encounterSetNames.Add(card.EncounterSet, cardSet.Name);
                    }
                }

                cards.Add(card.Id, card);

                foreach (var keyword in card.Keywords)
                {
                    var keywordKey = keyword.Trim();
                    var keywordValue = keywordKey.Replace("~", string.Empty);
                    if (!keywords.ContainsKey(keywordKey))
                        keywords.Add(keywordKey, keywordValue);
                }

                foreach (var trait in card.Traits)
                {
                    var traitKey = trait.Replace(".", string.Empty).Trim();
                    if (!traits.ContainsKey(traitKey))
                        traits.Add(traitKey, trait.Trim());
                }

                if (card.VictoryPoints > 0 && !victoryPointValues.ContainsKey(card.VictoryPoints))
                {
                    victoryPointValues.Add(card.VictoryPoints, string.Format("Victory {0}.", card.VictoryPoints));
                }
            }

            if (campaignMap.Count > 0)
            {
                foreach (var campaignItem in campaignMap)
                {
                    if (scenarios.ContainsKey(campaignItem.Key))
                    {
                        scenarios[campaignItem.Key].SetCampaignCard(campaignItem.Value);
                    }
                }
            }
        }

        private void AddDeck(Deck deck)
        {
            if (deck == null || decks.ContainsKey(deck.Name))
                return;

            decks.Add(deck.Name, deck);

            if (string.IsNullOrEmpty(deck.DeckList))
                return;

            foreach (var line in deck.DeckList.Split(new string [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var tokens = line.Split(' ').ToList();
                if (tokens == null || tokens.Count == 0)
                    continue;

                if (tokens.Last().StartsWith("x"))
                {
                    tokens.Remove(tokens.Last());
                }

                var setName = string.Empty;

                var last = tokens.LastOrDefault();
                if (last != null && last.Contains('(') && last.Contains(')'))
                {
                    setName = last.Trim().TrimStart('(').TrimEnd(')');
                    tokens.Remove(last);
                }

                if (tokens.Count == 0)
                    continue;

                var title = string.Join(" ", tokens).ToLower();

                var card = cards.Values.Where(x => 
                    (
                        (string.Equals(x.Title.ToLower(), title) || (!string.IsNullOrEmpty(x.NormalizedTitle) && string.Equals(x.NormalizedTitle.ToLower(), title)))
                        && ( string.IsNullOrEmpty(setName) || ( string.Equals(x.CardSet.Abbreviation, setName) || string.Equals(x.CardSet.Name, setName) ) )
                        && ( x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Attachment || x.CardType == CardType.Event || x.CardType == CardType.Treasure || x.CardType == CardType.Boon )
                    )
                ).FirstOrDefault();

                if (card != null && !card.Decks.ContainsKey(deck.Name))
                {
                    card.Decks.Add(deck.Name, deck);
                }
            }
        }

        private bool IsCategorizable(Card card)
        {
            if (string.IsNullOrEmpty(card.Text))
                return false;

            switch (card.CardType)
            {
                case CardType.Hero:
                    return true;
                case CardType.Ally:
                    return true;
                case CardType.Attachment:
                    return true;
                case CardType.Event:
                    return true;
                default:
                    return false;
            }
        }

        private Func<Card, Category> CreateCategoryFilter(string pattern, Category category)
        {
            return CreateCategoryFilter(pattern, category, null);
        }

        private Func<Card, Category> CreateCategoryFilter(string pattern, Category category, string negation)
        {
            Func<Card, Category> filter = (card) =>
                {
                    foreach (var line in card.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (line.MatchesPattern(pattern) && (string.IsNullOrEmpty(negation) || !line.ToLower().Contains(negation.ToLower())))
                            return category;
                    }

                    return Category.None;
                };

            return filter;
        }

        private void LoadCategories()
        {
            var filters = new List<Func<Card, Category>>
            {
                CreateCategoryFilter(@"add[\s]{1}[\d]{1}[\s]{1}resource", Category.Resource_Acceleration),
                CreateCategoryFilter(@"move[\s]{1}.*[\s]{1}resource|Pay 1 resource from a hero's resource pool to add 1 resource|add 1 resource to a Gondor or Noble|give attached hero a (Leadership|Tactics|Spirit|Lore)|gains a (Leadership|Tactics|Spirit|Lore)", Category.Resource_Smoothing),
                CreateCategoryFilter(@"(ally|allies){1,}.*into[\s]play|put into play the revealed card for no cost", Category.Mustering),
                CreateCategoryFilter(@"\+[\d]*[\s]Attack", Category.Attack_Bonus),
                CreateCategoryFilter(@"\+[\d]*[\s]Defense", Category.Defense_Bonus),
                CreateCategoryFilter(@"\+[\d]*[\s]Willpower|add.*Willpower", Category.Willpower_Bonus),
                CreateCategoryFilter(@"\+[\d]*[\s]Hit[\s]Point", Category.Hit_Point_Bonus),
                CreateCategoryFilter(@"(draw|draws)[\s][\w]*[\s]card|look at the top 2 cards of your deck. Add 1 to your hand|take it into your hand|a card and add it to your hand", Category.Card_Draw),
                CreateCategoryFilter(@"search[\s].*your[\s]deck", Category.Card_Search),
                CreateCategoryFilter(@"(look|looks)[\s]at[\s].*[\s]deck|the top card of your deck faceup|exchange a card in your hand with the top card of your deck", Category.Player_Scrying, "encounter deck"),
                CreateCategoryFilter(@"(look|looks)[\s]at[\s].*encounter[\s]deck", Category.Encounter_Scrying),
                CreateCategoryFilter("(enemy|enemies).*cannot attack", Category.Combat_Control),
                CreateCategoryFilter(@"heal[\s].*damage", Category.Healing),
                CreateCategoryFilter(@"discard.*Condition[\s]attachment", Category.Condition_Control),
                CreateCategoryFilter(@"place[\s].*progress|switch the active location|location enters play", Category.Location_Control),
                CreateCategoryFilter("ready.*(character|hero|ally|allies|him|her|them)", Category.Readying, "While Dain Ironfoot is ready"),
                CreateCategoryFilter(@"(return.*discard[\s]pile.*hand|shuffle.*discard[\s]pile.*back)", Category.Recursion, "encounter discard pile"),
                CreateCategoryFilter(@"deal[\s]([\d]|X)*[\s]damage|Deal damage to the attacking enemy|Excess damage dealt by this attack is assigned", Category.Direct_Damage),
                CreateCategoryFilter(@"(look at|revealed|enters play|top of the).*encounter[\s]deck", Category.Encounter_Control),
                CreateCategoryFilter(@"cancel.*shadow|shadow[\s]cards", Category.Shadow_Control), 
                CreateCategoryFilter(@"(reduce|reduces|lower).*(his|your|player).*threat", Category.Threat_Control, "your threat is lower"),
                CreateCategoryFilter(@"((enemy|enemies).*staging[\s]area.*attack|attacker.*against.*enemy not engaged with you|Any character may choose attached enemy as the target of an attack)", Category.Staging_Area_Attack),
                CreateCategoryFilter("(choose (an enemy|a location).*(staging area|not engaged with you))|add.*each enemy's engagement cost|each enemy.*gets.*engagement cost", Category.Staging_Area_Control),
                CreateCategoryFilter(@"after[\s].*[\s](enters|enters or leaves)[\s]play", Category.Enters_Play),
                CreateCategoryFilter(@"after[\s].*[\s]leaves[\s]play|return (it|him|Keen-eyed Took) to your hand", Category.Leaves_Play, "After attached location leaves play"),
                CreateCategoryFilter(@"(after[\s]you[\s]play[\s].*[\s]from[\s]your[\s]hand|after you play)", Category.Played_From_Hand),
                CreateCategoryFilter(@"attach 1 attachment card|an attachment of cost 3 or less and put it into play|you may attach that card facedown|to play Weapon and Armor attachments on|put into play the revealed card for no cost", Category.Equipping)
            };

            foreach (var card in cards.Values.Where(x => IsCategorizable(x)))
            {
                foreach (var filter in filters)
                {
                    var category = filter(card);
                    if (category == Category.None)
                        continue;
                    
                    card.Categories.Add(category);

                    var categoryKey = category.ToString();
                    if (!categories.ContainsKey(categoryKey))
                    {
                        categories.Add(categoryKey, category);
                    }
                }
            }
        }

        private void LoadScenarioCards()
        {
            foreach (var card in cards.Values.Where(x => !string.IsNullOrEmpty(x.ScenarioTitle)))
            {
                var escapedTitle = card.ScenarioTitle.ToUrlSafeString();

                if (card.CardType == CardType.Location || card.CardType == CardType.Enemy || card.CardType == CardType.Treachery || card.CardType == CardType.Objective || card.CardType == CardType.Objective_Ally)
                {
                    if (scenarios.ContainsKey(escapedTitle) && !card.ScenarioTitle.EndsWith(" Nightmare"))
                    {
                        scenarios[escapedTitle].AddScenarioCard(card);
                    }
                    else
                    {
                        if (card.ScenarioTitle.EndsWith(" Nightmare"))
                        {
                            var originalTitle = card.ScenarioTitle.Replace(" Nightmare", string.Empty).ToUrlSafeString();

                            if (scenarios.ContainsKey(originalTitle))
                            {
                                scenarios[originalTitle].AddScenarioCard(card);
                            }
                        }
                    }

                    foreach (var otherScenario in scenarios.Values.Where(x => x.IncludesEncounterSet(card.ScenarioTitle)).ToList())
                    {
                        otherScenario.AddScenarioCard(card);
                    }
                }
            }

            //load nightmare setup cards
            foreach (var setupCard in cards.Values.Where(x => x.CardType == CardType.Nightmare_Setup))
            {
                if (setupCard.UpdateScenarioCards != null)
                {
                    setupCard.UpdateScenarioCards(ScenarioGroups());
                }
            }
        }

        private void LoadProducts()
        {
            productGroups.Add(new ShadowsOfMirkwoodProductGroup());
            productGroups.Add(new TheDwarrowdelfProductGroup());
            productGroups.Add(new AgainstTheShadowProductGroup());
            productGroups.Add(new TheRingMakerProductGroup());

            productGroups.Add(new TheHobbitSagaProductGroup());
            productGroups.Add(new TheLordOfTheRingsSagaProductGroup());

            productGroups.Add(new GenConDeckProductGroup());
            productGroups.Add(new NightmareDeckProductGroup());

            productGroups.Add(new CustomProductGroup());

            foreach (var productGroup in productGroups)
            {
                if (productGroup.MainProduct != null)
                {
                    AddProduct(productGroup.MainProduct);
                }

                foreach (var product in productGroup.ChildProducts)
                {
                    AddProduct(product);
                }
            }
        }

        private void LoadDecks()
        {
            AddDeck(new WarriorsOfTheWhiteTower());
            AddDeck(new TheSpiritOfGondor());
            AddDeck(new FaramirAndTheRangersOfIthilien());
            AddDeck(new BoromirLeadsTheCharge());
            AddDeck(new CaldarasSacrifice());
            AddDeck(new KeepItSecretKeepItSafe());
            AddDeck(new RangersAndTraps());
            AddDeck(new SecretsOfTheWise());
            AddDeck(new CalledToTheSea());
            AddDeck(new PrisonerOfTheDarkForest());
            AddDeck(new ReclaimingKhazadDum());
            AddDeck(new ThreeKingsAQueenAndAPrince());
            AddDeck(new TheGreyCompanyDefendsGondor());
            AddDeck(new MagaliTribute());
            AddDeck(new BeornAttacks());
            AddDeck(new MastersOfTheForest());
            AddDeck(new TheFieldOfCormallen());
            AddDeck(new IsildursHeir());
            AddDeck(new FlightToTheFord());
            AddDeck(new TwoKingdomsReunited());
            AddDeck(new HamaTakesArcheryLessons());
            AddDeck(new BardGoesHunting());
            AddDeck(new TheIslandOfMisfitHeroes());
            AddDeck(new BalinHoldsTheLine());
            AddDeck(new TheRohirrimRideWithTheGreyCompany());
            AddDeck(new ThePowerOfThePalantir());
            AddDeck(new LoreMastery());
            AddDeck(new EaglesAndHorsesAndBearsOhMy());
            AddDeck(new TheDwarvesAndFaramir());
            AddDeck(new LocationControl());
            AddDeck(new Gluttony());
            AddDeck(new BoromirAndTheSevenDwarves());
            AddDeck(new VilyaTheRingOfAir());
            AddDeck(new DirectDamageTacticsLeadership());
            AddDeck(new DirectDamageLeadershipSpiritLore());
            AddDeck(new RenewedFriendshipsDwarvesElvesAndMen());
            AddDeck(new RenewedFriendshipsElvesAndElfFriends());
            AddDeck(new BattleOfPelennorRideToRuin());
            AddDeck(new BattleOfPelennorAndTheWorldsEnding());
            AddDeck(new SecretsOfErebor());
            AddDeck(new SecretsOfErebor2());
            AddDeck(new TheOrcHuntersOfImladris());
            AddDeck(new WardensOfImladris());

            AddDeck(new RideToRuin());
            AddDeck(new SpearmanSuperhero());
            AddDeck(new PalantirSupport());
            AddDeck(new TrapsOfIthilien());
            AddDeck(new EleanorsBigAdventure());
            AddDeck(new OutlandsGoneWild());
            AddDeck(new BlazeOfGlory());
            AddDeck(new RidingWithRohan());
            AddDeck(new WhereEaglesDare());

            AddDeck(new BeornsPathPTMLeadershipLore());
            AddDeck(new BeornsPathJAtALeadershipLore());
            AddDeck(new BeornsPathEFDGTacticsSpirit());
            AddDeck(new BeornsPathTHFGLeadershipLore());
            AddDeck(new BeornsPathTHFGTacticsSpirit());
            AddDeck(new BeornsPathCatCLeadershipLore());
            AddDeck(new BeornsPathAJtRLeadershipLore());
            AddDeck(new BeornsPathTHoEMLeadershipLore());
            AddDeck(new BeornsPathTDMLeadershipLore());
            AddDeck(new BeornsPathRtMTacticsSpirit());
        }

        /*
        private void AddRelationship(string leftTitle, string leftSet, string rightTitle, string rightSet)
        {
            var leftCard = cards.Values.Where(x => (string.Equals(x.Title, leftTitle) || string.Equals(x.NormalizedTitle, leftTitle)) && (string.IsNullOrEmpty(leftSet) || string.Equals(x.CardSet.Abbreviation, leftSet))).FirstOrDefault();
            var rightCard = cards.Values.Where(x => (string.Equals(x.Title, rightTitle) || string.Equals(x.NormalizedTitle, rightTitle)) && (string.IsNullOrEmpty(rightSet) || string.Equals(x.CardSet.Abbreviation, rightSet))).FirstOrDefault();

            if (leftCard == null || rightCard == null)
                return;

            if (!leftCard.RelatedCards.Any(x => x.Id == rightCard.Id))
                leftCard.RelatedCards.Add(rightCard);

            if (!rightCard.RelatedCards.Any(x => x.Id == leftCard.Id))
                rightCard.RelatedCards.Add(leftCard);
        }

        private void LoadRelationships()
        {
            AddRelationship("Aragorn", "Core", "Aragorn", "TWitW");
            AddRelationship("Aragorn", "Core", "Faramir", "Core");
            AddRelationship("Aragorn", "Core", "Steward of Gondor", "Core");
            AddRelationship("Aragorn", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Aragorn", "Core", "Sword that was Broken", "TWitW");
            AddRelationship("Aragorn", "Core", "Ring of Barahir", "TSF");

            AddRelationship("Theodred", "Core", "Snowbourn Scout", "Core");
            AddRelationship("Theodred", "Core", "Steward of Gondor", "Core");
            AddRelationship("Theodred", "Core", "Aragorn", "Core");

            AddRelationship("Gloin", "Core", "Lure of Moria", "RtR");
            AddRelationship("Gloin", "Core", "Longbeard Elder", "FoS");
            AddRelationship("Gloin", "Core", "We Are Not Idle", "SaF");
            AddRelationship("Gloin", "Core", "Cram", "THOHaUH");
            AddRelationship("Gloin", "Core", "Gloin", "THOtD");

            AddRelationship("Gimli", "Core", "Citadel Plate", "Core");
            AddRelationship("Gimli", "Core", "Horn of Gondor", "Core");
            AddRelationship("Gimli", "Core", "Feint", "Core");
            AddRelationship("Gimli", "Core", "Quick Strike", "Core");
            AddRelationship("Gimli", "Core", "Swift Strike", "Core");
            AddRelationship("Gimli", "Core", "Thalin", "Core");

            AddRelationship("Legolas", "Core", "Blade of Gondolin", "Core");
            AddRelationship("Legolas", "Core", "Rivendell Blade", "RtR");
            AddRelationship("Legolas", "Core", "Hands Upon the Bow", "SaF");
            AddRelationship("Legolas", "Core", "Foe-hammer", "THOHaUH");
            AddRelationship("Legolas", "Core", "Horn of Gondor", "Core");

            AddRelationship("Thalin", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Thalin", "Core", "Feint", "Core");
            AddRelationship("Thalin", "Core", "Swift Strike", "Core");
            AddRelationship("Thalin", "Core", "Blade of Gondolin", "Core");
            AddRelationship("Thalin", "Core", "Horn of Gondor", "Core");
            AddRelationship("Thalin", "Core", "Gimli", "Core");

            AddRelationship("Eowyn", "Core", "Northern Tracker", "Core");
            AddRelationship("Eowyn", "Core", "A Test of Will", "Core");
            AddRelationship("Eowyn", "Core", "Unexpected Courage", "Core");
            AddRelationship("Eowyn", "Core", "Gandalf", "Core");
            AddRelationship("Eowyn", "Core", "Dunhere", "Core");

            AddRelationship("Faramir", "Core", "Steward of Gondor", "Core");
            AddRelationship("Faramir", "Core", "Celebrian's Stone", "Core");
            AddRelationship("Faramir", "Core", "Faramir", "AoO");

            AddRelationship("Longbeard Orc Slayer", "Core", "Sneak Attack", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Steward of Gondor", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Gandalf", "Core");
            AddRelationship("Longbeard Orc Slayer", "Core", "Lure of Moria", "RtR");
            AddRelationship("Longbeard Orc Slayer", "Core", "Longbeard Elder", "FoS");
            AddRelationship("Longbeard Orc Slayer", "Core", "We Are Not Idle", "SaF");
            AddRelationship("Longbeard Orc Slayer", "Core", "Miner of the Iron Hills", "Core");

            AddRelationship("Sneak Attack", "Core", "Gandalf", "Core");

            AddRelationship("Steward of Gondor", "Core", "Sneak Attack", "Core");
            AddRelationship("Steward of Gondor", "Core", "Gandalf", "Core");

            AddRelationship("Celebrian's Stone", "Core", "Steward of Gondor", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Aragorn", "Core");
            AddRelationship("Celebrian's Stone", "Core", "Aragorn", "TWitW");
            AddRelationship("Celebrian's Stone", "Core", "Faramir", "Core");
            
            AddRelationship("Gondorian Spearman", "Core", "Feint", "Core");
            AddRelationship("Gondorian Spearman", "Core", "Gandalf", "Core");
            AddRelationship("Gondorian Spearman", "Core", "Horn of Gondor", "Core");
            
            AddRelationship("Feint", "Core", "Quick Strike", "Core");
            AddRelationship("Feint", "Core", "Hama", "TLD");

            AddRelationship("Quick Strike", "Core", "Gimli", "Core");
            AddRelationship("Quick Strike", "Core", "Feint", "Core");
            AddRelationship("Quick Strike", "Core", "Swift Strike", "Core");

            AddRelationship("Swift Strike", "Core", "Feint", "Core");
            AddRelationship("Swift Strike", "Core", "Gondorian Spearman", "Core");
            AddRelationship("Swift Strike", "Core", "Thalin", "Core");

            AddRelationship("Blade of Gondolin", "Core", "Legolas", "Core");
            AddRelationship("Blade of Gondolin", "Core", "Foe-hammer", "THOHaUH");
            AddRelationship("Blade of Gondolin", "Core", "Goblin-cleaver", "THOHaUH");
            
            AddRelationship("Citadel Plate", "Core", "Gimli", "Core");
            AddRelationship("Citadel Plate", "Core", "Horn of Gondor", "Core");

            AddRelationship("Horn of Gondor", "Core", "Sneak Attack", "Core");
            AddRelationship("Horn of Gondor", "Core", "Prince Imrahil", "AJtR");
            
            AddRelationship("The Galadhrim's Greeting", "Core", "Gandalf", "Core");
            AddRelationship("The Galadhrim's Greeting", "Core", "Gandalf", "THOHaUH");
            
            AddRelationship("Hasty Stroke", "Core", "A Test of Will", "Core");
            AddRelationship("Hasty Stroke", "Core", "A Burning Brand", "CatC");

            AddRelationship("A Test of Will", "Core", "Unexpected Courage", "Core");
            AddRelationship("A Test of Will", "Core", "Gandalf", "Core");
            
            AddRelationship("Stand and Fight", "Core", "Eowyn", "Core");
            AddRelationship("Stand and Fight", "Core", "Horn of Gondor", "Core");
            AddRelationship("Stand and Fight", "Core", "Dwarven Tomb", "Core");
            AddRelationship("Stand and Fight", "Core", "Escort from Edoras", "AJtR");

            AddRelationship("Dwarven Tomb", "Core", "A Test of Will", "Core");
            AddRelationship("Dwarven Tomb", "Core", "Hasty Stroke", "Core");
            AddRelationship("Dwarven Tomb", "Core", "Elrond's Counsel", "SaF");
            AddRelationship("Dwarven Tomb", "Core", "The Galadhrim's Greeting", "Core");
            AddRelationship("Dwarven Tomb", "Core", "Escort from Edoras", "AJtR");
            AddRelationship("Dwarven Tomb", "Core", "Miruvor", "SaF");

            AddRelationship("Unexpected Courage", "Core", "A Test of Will", "Core");
            AddRelationship("Unexpected Courage", "Core", "Imladris Stargazer", "FoS");

            AddRelationship("Erebor Hammersmith", "Core", "Gleowine", "Core");

            AddRelationship("Henamarth Riversong", "Core", "Denethor", "Core");
            AddRelationship("Henamarth Riversong", "Core", "Rumour from the Earth", "RtM");

            AddRelationship("Miner of the Iron Hills", "Core", "Erebor Hammersmith", "Core");

            AddRelationship("Beravor", "Core", "Aragorn", "TWitW");
            AddRelationship("Gleowine", "Core", "Beravor", "Core");
            
            AddRelationship("Radagast's Cunning", "Core", "Secret Paths", "Core");
            
            AddRelationship("Secret Paths", "Core", "Radagast's Cunning", "Core");
            
            AddRelationship("Forest Snare", "Core", "Protector of Lorien", "Core");
            AddRelationship("Forest Snare", "Core", "Anborn", "TBoG");

            AddRelationship("Protector of Lorien", "Core", "Gleowine", "Core");
            AddRelationship("Protector of Lorien", "Core", "A Burning Brand", "CatC");
            AddRelationship("Protector of Lorien", "Core", "Beravor", "Core");

            AddRelationship("Rivendell Minstrel", "THfG", "Song of Kings", "THfG");
            AddRelationship("Rivendell Minstrel", "THfG", "Song of Travel", "THoEM");
            AddRelationship("Rivendell Minstrel", "THfG", "Song of Battle", "TDM");
            AddRelationship("Rivendell Minstrel", "THfG", "Song of Wisdom", "CatC");
            AddRelationship("Rivendell Minstrel", "THfG", "Gleowine", "Core");
            
            AddRelationship("Westfold Horse-Breaker", "THfG", "Eowyn", "Core");
            AddRelationship("Westfold Horse-Breaker", "THfG", "A Test of Will", "Core");
            AddRelationship("Westfold Horse-Breaker", "THfG", "Unexpected Courage", "Core");

            AddRelationship("Winged Guardian", "THfG", "Horn of Gondor", "Core");
            AddRelationship("Winged Guardian", "THfG", "The Eagles Are Coming!", "THfG");
            AddRelationship("Winged Guardian", "THfG", "Vassal of the Windlord", "TDM");
            AddRelationship("Winged Guardian", "THfG", "Eagles of the Misty Mountains", "RtM");
            AddRelationship("Winged Guardian", "THfG", "Descendant of Thorondor", "THoEM");
            AddRelationship("Winged Guardian", "THfG", "Landroval", "AJtR");

            AddRelationship("Frodo Baggins", "CatC", "Fast Hitch", null);
            AddRelationship("Frodo Baggins", "CatC", "The Galadhrim's Greeting", "Core");
            AddRelationship("Frodo Baggins", "CatC", "Aragorn", "TWitW");
            AddRelationship("Frodo Baggins", "CatC", "Wandering Took", "Core");
            AddRelationship("Frodo Baggins", "CatC", "Gandalf", "Core");

            AddRelationship("Dunedain Warning", "CatC", "Dunedain Quest", null);
            AddRelationship("Dunedain Warning", "CatC", "Dunedain Mark", null);
            AddRelationship("Dunedain Warning", "CatC", "Dunedain Cache", null);
            AddRelationship("Dunedain Warning", "CatC", "Dunedain Signal", null);

            AddRelationship("Haldir of Lorien", "AJtR", "Mirlonde", "TDF");
            AddRelationship("Haldir of Lorien", "AJtR", "Gildor Inglorion", "THoEM");
            AddRelationship("Haldir of Lorien", "AJtR", "Protector of Lorien", "Core");
            AddRelationship("Haldir of Lorien", "AJtR", "A Burning Brand", "CatC");
            AddRelationship("Haldir of Lorien", "AJtR", "Silvan Tracker", "TDM");
            AddRelationship("Haldir of Lorien", "AJtR", "Mirkwood Runner", "RtM");
            AddRelationship("Haldir of Lorien", "AJtR", "Henamarth Riversong", "Core");
            AddRelationship("Haldir of Lorien", "AJtR", "Daughter of the Nimrodel", "Core");

            AddRelationship("Ancient Mathom", "AJtR", "Northern Tracker", "Core");
            AddRelationship("Ancient Mathom", "AJtR", "The Riddermark's Finest", "THoEM");
            AddRelationship("Ancient Mathom", "AJtR", "Asfaloth", "FoS");
            AddRelationship("Ancient Mathom", "AJtR", "West Road Traveller", "RtM");

            AddRelationship("Escort from Edoras", "AJtR", "Horn of Gondor", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "A Test of Will", "Core");
            AddRelationship("Escort from Edoras", "AJtR", "Ancient Mathom", "AJtR");
            AddRelationship("Escort from Edoras", "AJtR", "West Road Traveller", "RtM");

            AddRelationship("Descendant of Thorondor", "THoEM", "Winged Guardian", "THfG");
            AddRelationship("Descendant of Thorondor", "THoEM", "Radagast", "AJtR");
            AddRelationship("Descendant of Thorondor", "THoEM", "Vassal of the Windlord", "TDM");
            AddRelationship("Descendant of Thorondor", "THoEM", "Eagles of the Misty Mountains", "RtM");
            AddRelationship("Descendant of Thorondor", "THoEM", "Hail of Stones", "RtR");
            AddRelationship("Descendant of Thorondor", "THoEM", "Sneak Attack", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Horn of Gondor", "Core");
            AddRelationship("Descendant of Thorondor", "THoEM", "Born Aloft", "CatC");
            AddRelationship("Descendant of Thorondor", "THoEM", "Gondorian Spearman", "Core");

            AddRelationship("Gildor Inglorion", "THoEM", "Gleowine", "Core");
            AddRelationship("Gildor Inglorion", "THoEM", "Haldir of Lorien", "AJtR");
            AddRelationship("Gildor Inglorion", "THoEM", "Asfaloth", "FoS");
            AddRelationship("Gildor Inglorion", "THoEM", "A Burning Brand", "CatC");

            AddRelationship("Song of Travel", "THoEM", "Song of Battle", "");
            AddRelationship("Song of Travel", "THoEM", "Song of Kings", "");
            AddRelationship("Song of Travel", "THoEM", "Song of Wisdome", "");
            
            AddRelationship("Elfhelm", "TDM", "Eowyn", "Core");
            AddRelationship("Elfhelm", "TDM", "Dunhere", "Core");
            AddRelationship("Elfhelm", "TDM", "Eomund", "CatC");
            AddRelationship("Elfhelm", "TDM", "Escort from Edoras", "AJtR");
            AddRelationship("Elfhelm", "TDM", "Westfold Horse-Breaker", "THfG");
            AddRelationship("Elfhelm", "TDM", "The Riddermark's Finest", "THoEM");

            AddRelationship("Fast Hitch", "TDM", "Unexpected Courage", "Core");
            AddRelationship("Fast Hitch", "TDM", "Peace, and Thought", "SaF");

            AddRelationship("Silvan Tracker", "TDM", "Haldir of Lorien", "AJtR");
            AddRelationship("Silvan Tracker", "TDM", "Mirlonde", "TDF");
            AddRelationship("Silvan Tracker", "TDM", "Daughter of the Nimrodel", "Core");
            AddRelationship("Silvan Tracker", "TDM", "Elrond", "SaF");
            AddRelationship("Silvan Tracker", "TDM", "Mirkwood Runner", "RtM");

            AddRelationship("Vassal of the Windlord", "TDM", "Sneak Attack", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Steward of Gondor", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Gondorian Spearman", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Feint", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Gandalf", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Horn of Gondor", "Core");
            AddRelationship("Vassal of the Windlord", "TDM", "Winged Guardian", "THfG");
            AddRelationship("Vassal of the Windlord", "TDM", "Eagles of the Misty Mountains", "RtM");
            
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Horn of Gondor", "Core");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Gandalf", "Core");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "The Eagles Are Coming!", "THfG");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Winged Guardian", "THfG");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Landroval", "AJtR");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Vassal of the Windlord", "TDM");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Support of the Eagles", "RtM");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Radagast", "AJtR");
            AddRelationship("Eagles of the Misty Mountains", "RtM", "Descendant of Thorondor", "THoEM");

            AddRelationship("Bifur", "KD", "Bifur", "THOtD");

            AddRelationship("Erebor Record Keeper", "KD", "Miner of the Iron Hills", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Lure of Moria", "RtR");
            AddRelationship("Erebor Record Keeper", "KD", "Legacy of Durin", "TWitW");
            AddRelationship("Erebor Record Keeper", "KD", "Daeron's Runes", "FoS");
            AddRelationship("Erebor Record Keeper", "KD", "We Are Not Idle", "SaF");
            AddRelationship("Erebor Record Keeper", "KD", "Fili", "THOHaUH");
            AddRelationship("Erebor Record Keeper", "KD", "Kili", "THOHaUH");
            AddRelationship("Erebor Record Keeper", "KD", "Gloin", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Erebor Hammersmith", "Core");
            AddRelationship("Erebor Record Keeper", "KD", "Longbeard Map-Maker", "CatC");
            AddRelationship("Erebor Record Keeper", "KD", "Bifur", "KD");

            AddRelationship("Narvi's Belt", "KD", "Thorin Oakenshield", "THOtD");
            AddRelationship("Narvi's Belt", "KD", "Dain Ironfoot", "RtM");
            AddRelationship("Narvi's Belt", "KD", "Thorin Oakenshield", "THOtD");
            AddRelationship("Narvi's Belt", "KD", "Gloin", "Core");
            AddRelationship("Narvi's Belt", "KD", "Balin", "THOtD");

            AddRelationship("Zigil Miner", "KD", "Imladris Stargazer", "FoS");
            AddRelationship("Zigil Miner", "KD", "Hidden Cache", "TMV");

            AddRelationship("Bofur", "TRG", "Bofur", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Zigil Miner", "KD");
            AddRelationship("Bofur", "TRG", "Fili", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Kili", "THOHaUH");
            AddRelationship("Bofur", "TRG", "Horn of Gondor", "Core");
            
            AddRelationship("Hail of Stones", "RtR", "Gondorian Spearman", "Core");
            AddRelationship("Hail of Stones", "RtR", "Horn of Gondor", "Core");
            AddRelationship("Hail of Stones", "RtR", "Winged Guardian", "THfG");
            AddRelationship("Hail of Stones", "RtR", "Vassal of the Windlord", "TDM");
            AddRelationship("Hail of Stones", "RtR", "Thalin", "Core");
            AddRelationship("Hail of Stones", "RtR", "Hands Upon the Bow", "SaF");

            AddRelationship("Lure of Moria", "RtR", "Erebor Record Keeper", "KD");
            AddRelationship("Lure of Moria", "RtR", "We Are Not Idle", "SaF");
            AddRelationship("Lure of Moria", "RtR", "Fili", "THOHaUH");
            AddRelationship("Lure of Moria", "RtR", "Kili", "THOHaUH");
            AddRelationship("Lure of Moria", "RtR", "Longbeard Orc Slayer", "Core");
            AddRelationship("Lure of Moria", "RtR", "Longbeard Elder", "FoS");
            AddRelationship("Lure of Moria", "RtR", "Cram", "THOHaUH");
            
            AddRelationship("Rivendell Blade", "RtR", "Legolas", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Blade of Gondolin", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Elladan", "RtR");
            AddRelationship("Rivendell Blade", "RtR", "Elrohir", "TRG");
            AddRelationship("Rivendell Blade", "RtR", "Glorfindel", "Core");
            AddRelationship("Rivendell Blade", "RtR", "Glorfindel", "FoS");
            AddRelationship("Rivendell Blade", "RtR", "Rivendell Bow", "TWitW");
            AddRelationship("Rivendell Blade", "RtR", "Mirlonde", "TDF");
            AddRelationship("Rivendell Blade", "RtR", "Elrond", "SaF");
            AddRelationship("Rivendell Blade", "RtR", "Foe-hammer", "THOHaUH");
            AddRelationship("Rivendell Blade", "RtR", "Goblin-cleaver", "THOHaUH");

            AddRelationship("Aragorn", "TWitW", "Celebrian's Stone", "Core");
            AddRelationship("Aragorn", "TWitW", "Protector of Lorien", "Core");
            AddRelationship("Aragorn", "TWitW", "Rivendell Minstrel", "THfG");
            AddRelationship("Aragorn", "TWitW", "Song of Kings", "THfG");
            AddRelationship("Aragorn", "TWitW", "A Burning Brand", "CatC");
            AddRelationship("Aragorn", "TWitW", "Song of Travel", "THoEM");
            AddRelationship("Aragorn", "TWitW", "Sword that was Broken", "TWitW");
            AddRelationship("Aragorn", "TWitW", "Ring of Barahir", "TSF");

            AddRelationship("Arwen Undomiel", "TWitW", "Gandalf", "THOHaUH");
            AddRelationship("Arwen Undomiel", "TWitW", "Elrond's Counsel", "TWitW");
            
            AddRelationship("Elrond's Counsel", "TWitW", "A Test of Will", "Core");
            AddRelationship("Elrond's Counsel", "TWitW", "Gandalf", "Core");
            AddRelationship("Elrond's Counsel", "TWitW", "Gandalf", "THOHaUH");
            AddRelationship("Elrond's Counsel", "TWitW", "Arwen Undomiel", "TWitW");
            AddRelationship("Elrond's Counsel", "TWitW", "Glorfindel", "FoS");
            
            AddRelationship("Legacy of Durin", "TWitW", "Miner of the Iron Hills", "Core");
            AddRelationship("Legacy of Durin", "TWitW", "Erebor Record Keeper", "KD");
            AddRelationship("Legacy of Durin", "TWitW", "Lure of Moria", "RtR");
            AddRelationship("Legacy of Durin", "TWitW", "Daeron's Runes", "FoS");
            AddRelationship("Legacy of Durin", "TWitW", "We Are Not Idle", "SaF");
            AddRelationship("Legacy of Durin", "TWitW", "Erebor Hammersmith", "Core");
            AddRelationship("Legacy of Durin", "TWitW", "Longbeard Map-Maker", "CatC");
            AddRelationship("Legacy of Durin", "TWitW", "Bifur", "KD");
            AddRelationship("Legacy of Durin", "TWitW", "Ori", "THOHaUH");
            AddRelationship("Legacy of Durin", "TWitW", "Bombur", "THOtD");
            
            AddRelationship("Resourceful", "TWitW", "Arwen Undomiel", "TWitW");
            AddRelationship("Resourceful", "TWitW", "Glorfindel", "FoS");
            
            AddRelationship("Sword that was Broken", "TWitW", "Steward of Gondor", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Celebrian's Stone", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Aragorn", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Faramir", "Core");
            AddRelationship("Sword that was Broken", "TWitW", "Visionary Leadership", "TMV");

            AddRelationship("Erebor Battle Master", "TLD", "Gimli", "Core");
            AddRelationship("Erebor Battle Master", "TLD", "Thalin", "Core");
            AddRelationship("Erebor Battle Master", "TLD", "Bofur", "THOHaUH");
            AddRelationship("Erebor Battle Master", "TLD", "Veteran Axehand", "Core");
            AddRelationship("Erebor Battle Master", "TLD", "Veteran of Nanduhirion", "Core");
            AddRelationship("Erebor Battle Master", "TLD", "We Are Not Idle", "SaF");
            
            AddRelationship("Warden of Healing", "TLD", "Elrond", "SaF");
            AddRelationship("Warden of Healing", "TLD", "Daughter of the Nimrodel", "Core");
            AddRelationship("Warden of Healing", "TLD", "Denethor", "Core");
            AddRelationship("Warden of Healing", "TLD", "Faramir", "AoO");

            AddRelationship("Asfaloth", "FoS", "Glorfindel", "Core");
            AddRelationship("Asfaloth", "FoS", "Glorfindel", "FoS");
            AddRelationship("Asfaloth", "FoS", "Elf-Stone", "TBR");
            AddRelationship("Asfaloth", "FoS", "Ancient Mathom", "AJtR");
            AddRelationship("Asfaloth", "FoS", "Northern Tracker", "Core");

            AddRelationship("Glorfindel", "FoS", "Glorfindel", "Core");
            AddRelationship("Glorfindel", "FoS", "Unexpected Courage", "Core");
            AddRelationship("Glorfindel", "FoS", "Arwen Undomiel", "TWitW");
            AddRelationship("Glorfindel", "FoS", "Imladris Stargazer", "FoS");
            AddRelationship("Glorfindel", "FoS", "Light of Valinor", "FoS");
            AddRelationship("Glorfindel", "FoS", "Gandalf", "Core");
            AddRelationship("Glorfindel", "FoS", "Gandalf", "THOHaUH");
            AddRelationship("Glorfindel", "FoS", "Elrond's Counsel", "TWitW");
            AddRelationship("Glorfindel", "FoS", "Asfaloth", "FoS");

            AddRelationship("Imladris Stargazer", "FoS", "Elrond", "SaF");
            AddRelationship("Imladris Stargazer", "FoS", "Vilya", "SaF");
            AddRelationship("Imladris Stargazer", "FoS", "Zigil Miner", "KD");
            
            AddRelationship("Light of Valinor", "FoS", "Glorfindel", "FoS");
            AddRelationship("Light of Valinor", "FoS", "Glorfindel", "Core");
            AddRelationship("Light of Valinor", "FoS", "Elrond", "SaF");
            AddRelationship("Light of Valinor", "FoS", "Elladan", "RtR");
            AddRelationship("Light of Valinor", "FoS", "Elrohir", "TRG");
            
            AddRelationship("Longbeard Elder", "FoS", "Longbeard Orc Slayer", "Core");
            AddRelationship("Longbeard Elder", "FoS", "Lure of Moria", "RtR");
            AddRelationship("Longbeard Elder", "FoS", "We Are Not Idle", "SaF");
            AddRelationship("Longbeard Elder", "FoS", "Fili", "THOHaUH");
            AddRelationship("Longbeard Elder", "FoS", "Kili", "THOHaUH");
            AddRelationship("Longbeard Elder", "FoS", "Gloin", "Core");
            AddRelationship("Longbeard Elder", "FoS", "Brok Ironfist", "Core");

            AddRelationship("Elrond", "SaF", "Gildor Inglorion", "THoEM");
            AddRelationship("Elrond", "SaF", "A Burning Brand", "CatC");
            AddRelationship("Elrond", "SaF", "Vilya", "SaF");
            AddRelationship("Elrond", "SaF", "Warden of Healing", "TLD");
            AddRelationship("Elrond", "SaF", "Imladris Stargazer", "FoS");
            AddRelationship("Elrond", "SaF", "Unexpected Courage", "Core");
            AddRelationship("Elrond", "SaF", "Miruvor", "SaF");

            AddRelationship("Hands Upon the Bow", "SaF", "Legolas", "Core");
            AddRelationship("Hands Upon the Bow", "SaF", "Vassal of the Windlord", "TDM");
            AddRelationship("Hands Upon the Bow", "SaF", "Brand son of Bain", "THoEM");
            AddRelationship("Hands Upon the Bow", "SaF", "Bard the Bowman", "THOtD");
            AddRelationship("Hands Upon the Bow", "SaF", "Horseback Archer", "Core");
            AddRelationship("Hands Upon the Bow", "SaF", "Trollshaw Scout", "FoS");

            AddRelationship("Master of the Forge", "SaF", "Gleowine", "Core");
            AddRelationship("Master of the Forge", "SaF", "Imladris Stargazer", "FoS");
            AddRelationship("Master of the Forge", "SaF", "Gildor Inglorion", "THoEM");
            
            AddRelationship("Miruvor", "SaF", "Unexpected Courage", "Core");
            
            AddRelationship("Peace, and Thought", "SaF", "Gleowine", "Core");
            AddRelationship("Peace, and Thought", "SaF", "Unexpected Courage", "Core");
            AddRelationship("Peace, and Thought", "SaF", "Fast Hitch", "TDM");
            AddRelationship("Peace, and Thought", "SaF", "Cram", "THOHaUH");
            AddRelationship("Peace, and Thought", "SaF", "Miruvor", "SaF");

            AddRelationship("We Are Not Idle", "SaF", "Lure of Moria", "RtR");
            AddRelationship("We Are Not Idle", "SaF", "Longbeard Elder", "FoS");
            AddRelationship("We Are Not Idle", "SaF", "Cram", "THOHaUH");
            
            AddRelationship("Envoy of Pelargir", "HoN", "White Tower Watchman", "TDF");
            
            AddRelationship("Errand-rider", "HoN", "Faramir", "Core");
            AddRelationship("Errand-rider", "HoN", "Steward of Gondor", "Core");
            AddRelationship("Errand-rider", "HoN", "Boromir", "HoN");
            
            AddRelationship("Ithilien Tracker", "HoN", "Denethor", "Core");
            AddRelationship("Ithilien Tracker", "HoN", "Warden of Healing", "TLD");
            AddRelationship("Ithilien Tracker", "HoN", "Ranger Spikes", "HoN");
            AddRelationship("Ithilien Tracker", "HoN", "Anborn", "TBoG");
            AddRelationship("Ithilien Tracker", "HoN", "Ithilien Archer", "EaAD");
            AddRelationship("Ithilien Tracker", "HoN", "Ranger Bow", "AoO");
            
            AddRelationship("Ranger Spikes", "HoN", "Erebor Hammersmith", "Core");
            AddRelationship("Ranger Spikes", "HoN", "Anborn", "TBoG");
            AddRelationship("Ranger Spikes", "HoN", "Poisoned Stakes", "TBoG");
            AddRelationship("Ranger Spikes", "HoN", "Ithilien Pit", "EaAD");

            AddRelationship("Gondorian Shield", "TSF", "Denethor", "Core");
            AddRelationship("Gondorian Shield", "TSF", "Boromir", "TDM");
            AddRelationship("Gondorian Shield", "TSF", "Beregond", "HoN");
            AddRelationship("Gondorian Shield", "TSF", "Horn of Gondor", "Core");
            
            AddRelationship("A Very Good Tale", "THOHaUH", "Sneak Attack", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "Steward of Gondor", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "Gandalf", "Core");
            AddRelationship("A Very Good Tale", "THOHaUH", "Gandalf", "THOHaUH");
            AddRelationship("A Very Good Tale", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("A Very Good Tale", "THOHaUH", "Fili", "THOHaUH");
            AddRelationship("A Very Good Tale", "THOHaUH", "Kili", "THOHaUH");
            
            AddRelationship("Fili", "THOHaUH", "Zigil Miner", "KD");
            AddRelationship("Fili", "THOHaUH", "Bofur", "TRG");
            AddRelationship("Fili", "THOHaUH", "Lure of Moria", "RtR");
            AddRelationship("Fili", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("Fili", "THOHaUH", "A Very Good Tale", "THOHaUH");
            AddRelationship("Fili", "THOHaUH", "Kili", "THOHaUH");
            AddRelationship("Fili", "THOHaUH", "Balin", "THOtD");
            AddRelationship("Fili", "THOHaUH", "King Under the Mountain", "THOtD");
            AddRelationship("Fili", "THOHaUH", "Longbeard Elder", "FoS");
            AddRelationship("Fili", "THOHaUH", "Cram", "THOHaUH");

            AddRelationship("Foe-hammer", "THOHaUH", "Gondorian Spearman", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Feint", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Gandalf", "Core");
            AddRelationship("Foe-hammer", "THOHaUH", "Hands Upon the Bow", "SaF");
            AddRelationship("Foe-hammer", "THOHaUH", "Bofur", "THOHaUH");

            AddRelationship("Gandalf", "THOHaUH", "Glorfindel", "FoS");
            AddRelationship("Gandalf", "THOHaUH", "Elrond's Counsel", "TWitW");
            AddRelationship("Gandalf", "THOHaUH", "The Galadhrim's Greeting", "Core");

            AddRelationship("Kili", "THOHaUH", "Erebor Record Keeper", "KD");
            AddRelationship("Kili", "THOHaUH", "Narvi's Belt", "KD");
            AddRelationship("Kili", "THOHaUH", "Zigil Miner", "KD");
            AddRelationship("Kili", "THOHaUH", "Bofur", "TRG");
            AddRelationship("Kili", "THOHaUH", "Lure of Moria", "RtR");
            AddRelationship("Kili", "THOHaUH", "We Are Not Idle", "SaF");
            AddRelationship("Kili", "THOHaUH", "A Very Good Tale", "THOHaUH");
            AddRelationship("Kili", "THOHaUH", "Fili", "THOHaUH");
            AddRelationship("Kili", "THOHaUH", "Balin", "THOtD");
            AddRelationship("Kili", "THOHaUH", "King Under the Mountain", "THOtD");
            AddRelationship("Kili", "THOHaUH", "Longbeard Elder", "FoS");

            AddRelationship("Balin", "THOtD", "Steward of Gondor", "Core");
            AddRelationship("Balin", "THOtD", "Gandalf", "Core");
            AddRelationship("Balin", "THOtD", "We Are Not Idle", "SaF");
            AddRelationship("Balin", "THOtD", "Errand-rider", "HoN");
            AddRelationship("Balin", "THOtD", "Gaining Strength", "TSF");
            AddRelationship("Balin", "THOtD", "A Very Good Tale", "THOHaUH");
            AddRelationship("Balin", "THOtD", "King Under the Mountain", "THOtD");
            AddRelationship("Balin", "THOtD", "Narvi's Belt", "KD");
            AddRelationship("Balin", "THOtD", "Lure of Moria", "RtR");
            AddRelationship("Balin", "THOtD", "Fili", "THOHaUH");
            AddRelationship("Balin", "THOtD", "Kili", "THOHaUH");
            
            AddRelationship("King Under the Mountain", "THOtD", "Steward of Gondor", "Core");
            AddRelationship("King Under the Mountain", "THOtD", "We Are Not Idle", "SaF");
            AddRelationship("King Under the Mountain", "THOtD", "A Very Good Tale", "THOHaUH");
            AddRelationship("King Under the Mountain", "THOtD", "Balin", "THOtD");
            AddRelationship("King Under the Mountain", "THOtD", "Narvi's Belt", "KD");
            AddRelationship("King Under the Mountain", "THOtD", "Lure of Moria", "RtR");
            AddRelationship("King Under the Mountain", "THOtD", "Fili", "THOHaUH");
            AddRelationship("King Under the Mountain", "THOtD", "Kili", "THOHaUH");

            AddRelationship("Boromir", "TDM", "Boromir", "HoN");
            AddRelationship("Boromir", "TDM", "Gandalf", "Core");

            AddRelationship("Pippin", "EaAD", "Pippin", "TBR");

            AddRelationship("Eomer", "VoI", "Theoden", "TMV");
            AddRelationship("Eomer", "VoI", "Hama", "TLD");
            AddRelationship("Eomer", "VoI", "Eomund", "CatC");

            AddRelationship("Legacy of Numenor", "VoI", "Deep Knowledge", "VoI");
            AddRelationship("Legacy of Numenor", "VoI", "Power of Orthanc", "VoI");
            AddRelationship("Legacy of Numenor", "VoI", "The Wizard's Voice", "VoI");
            AddRelationship("Legacy of Numenor", "VoI", "The Seeing-stone", "VoI");
            AddRelationship("Deep Knowledge", "VoI", "Power of Orthanc", "VoI");
            AddRelationship("Deep Knowledge", "VoI", "The Wizard's Voice", "VoI");
            AddRelationship("Deep Knowledge", "VoI", "The Seeing-stone", "VoI");
            AddRelationship("Power of Orthanc", "VoI", "The Wizard's Voice", "VoI");
            AddRelationship("Power of Orthanc", "VoI", "The Seeing-Stone", "VoI");
            AddRelationship("The Wizard's Voice", "VoI", "The Seeing-Stone", "VoI");

            AddRelationship("Westfold Horse-breeder", "VoI", "Steed of the Mark", "TMV");
            AddRelationship("Westfold Horse-breeder", "VoI", "Rohan Warhorse", "VoI");
            AddRelationship("Westfold Horse-breeder", "VoI", "Asfaloth", "FoS");
        }*/

        public IEnumerable<Card> All()
        {
            return cards.Values.ToList();
        }

        private Func<Card, bool> NegateFilter(Func<Card, bool> predicate)
        {
            return (card) => { return !predicate(card); };
        }

        private List<Card> FilterByByte(string typeName, string value, List<Card> results, bool negate)
        {
            //Params: card value, filter values. returns bool
            Func<byte, List<byte>, bool> comparison = null;

            if (value.StartsWith(">"))
            {
                comparison = (cardValue, filters) => { return filters.Any(f => cardValue >  f); };
            }
            else if (value.StartsWith("<"))
            {
                comparison = (cardValue, filters) => { return filters.Any(f => cardValue < f); };
            }
            else if (value.Contains("-"))
            {
                comparison = (cardValue, filters) => {
                    if (filters.Count != 2 || filters[0] > filters[1])
                        return false;

                    return cardValue >= filters[0] && cardValue <= filters[1];
                };
            }
            else
            {
                comparison = (cardValue, filters) => { return filters.Any(f => cardValue == f); };
            }

            value = value.TrimStart('>', '<').Replace("-", ",");

            var tokens = value.SplitOnComma();

            var bytes = new List<byte>();

            foreach (var token in tokens)
            {
                var item = (byte)0;
                if (byte.TryParse(token, out item))
                {
                    bytes.Add(item);
                }
            }

            Func<Card, bool> predicate = null;

            switch (typeName)
            {
                case "rcost":
                    predicate = (card) => { return card.ResourceCost.HasValue && comparison(card.ResourceCost.Value, bytes); };
                    break;
                case "tcost":
                    predicate = (card) => { return card.ThreatCost.HasValue && comparison(card.ThreatCost.Value, bytes); };
                    break;
                case "ecost":
                    predicate = (card) => { return card.EngagementCost.HasValue && comparison(card.EngagementCost.Value, bytes); };
                    break;
                case "threat":
                    predicate = (card) => { return comparison(card.Threat, bytes); };
                    break;
                case "wp":
                    predicate = (card) => { return comparison(card.Willpower, bytes); };
                    break;
                case "atk":
                    predicate = (card) => { return comparison(card.Attack, bytes); };
                    break;
                case "def":
                    predicate = (card) => { return comparison(card.Defense, bytes); };
                    break;
                case "hp":
                    predicate = (card) => { return card.HitPoints.HasValue && comparison(card.HitPoints.Value, bytes); };
                    break;
                case "victory":
                    predicate = (card) => { return comparison(card.VictoryPoints, bytes); };
                    break;
                default:
                    break;
            }

            if (predicate != null)
            {
                if (negate)
                {
                    predicate = NegateFilter(predicate);
                }

                results = results.Where(predicate).ToListSafe();
            }

            return results;
        }

        private List<Card> FilterByString(string typeName, string value, List<Card> results, bool negate)
        {
            var names = value.SplitOnComma().Select(x => x.Replace('+', ' ').Replace('_', ' ')).ToList();
            Func<Card, bool> predicate = null;

            switch (typeName)
            {
                case "cycle":
                    predicate = (card) => { return names.Any(y => card.CardSet.Cycle.MatchesWildcard(y)); };
                    break;
                case "set":
                    predicate = (card) => { return names.Any(y => card.CardSet.Name.MatchesWildcard(y) || card.CardSet.Abbreviation.MatchesWildcard(y) || (!string.IsNullOrEmpty(card.CardSet.AlternateName) && card.CardSet.AlternateName.MatchesWildcard(y)) || (!string.IsNullOrEmpty(card.CardSet.NormalizedName) && card.CardSet.NormalizedName.MatchesWildcard(y))); };
                    break;
                case "encounter":
                    predicate = (card) => { return names.Any(y => card.EncounterSet.MatchesWildcard(y)); };
                    break;
                case "trait":
                    predicate = (card) => { return names.Any(y => card.Traits.Select(z => z.Trim(' ','.')).Any(a => a.MatchesWildcard(y)) || card.NormalizedTraits.Select(z => z.Trim(' ', '.')).Any(a => a.MatchesWildcard(y))); };
                    break;
                case "keyword":
                    predicate = (card) => { return names.Any(y => card.Keywords.Select(z => z.Trim(' ','.')).Any(a => a.MatchesWildcard(y)) || card.NormalizedKeywords.Select(z => z.Trim(' ', '.')).Any(a => a.MatchesWildcard(y))); };
                    break;
                case "artist":
                    predicate = (card) => { return names.Any(y => (card.Artist != null && card.Artist.Name.MatchesWildcard(y)) || (card.SecondArtist != null && card.SecondArtist.Name.MatchesWildcard(y))); };
                    break;
                default:
                    break;
            }

            if (predicate != null)
            {
                if (negate)
                {
                    predicate = NegateFilter(predicate);
                }

                results = results.Where(predicate).ToListSafe();
            }

            return results;
        }

        private List<Card> FilterByBool(string typeName, List<Card> results, bool negate)
        {
            Func<Card, bool> predicate = null;

            switch (typeName)
            {
                case "unique":
                    predicate = (card) => { return card.IsUnique; };
                    break;
                case "custom":
                    predicate = (card) => { return card.CardSet.SetType == SetType.Custom_Expansion; };
                    break;
                default:
                    break;
            }

            if (predicate != null)
            {
                if (negate)
                {
                    predicate = NegateFilter(predicate);
                }

                results = results.Where(predicate).ToListSafe();
            }

            return results;
        }

        private List<Card> FilterByEnum<TEnum>(string value, List<Card> results, bool negate)
            where TEnum: struct
        {
            var tokens = value.SplitOnComma();

            var enums = new List<TEnum>();

            foreach (var token in tokens)
            {
                var parsedItem = token.ParseEnum<TEnum>();
                if (parsedItem.Item2)
                {
                    enums.Add(parsedItem.Item1);
                }
            }

            var typeName = typeof(TEnum).Name.ToLowerSafe();
            Func<Card, bool> predicate = null;

            switch (typeName)
            {
                case "cardtype":
                    predicate = (card) => { return enums.Any(y => y.ToEnum<CardType>() == card.CardType); };
                    break;
                case "sphere":
                    predicate = (card) => { return enums.Any(y => y.ToEnum<Sphere>() == card.Sphere); };
                    break;
                case "category":
                    predicate = (card) => { return enums.Any(y => card.Categories.Any(z => y.ToEnum<Category>() == z)); };
                    break;
                default:
                    break;
            }

            if (predicate != null)
            {
                if (negate)
                {
                    predicate = NegateFilter(predicate);
                }

                results = results.Where(predicate).ToListSafe();
            }

            return results;
        }

        private List<Card> AdvancedSearch(SearchViewModel searchModel, List<Card> results)
        {
            var query = searchModel.Query;
            var filters = query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToListSafe().Where(x => x.StartsWith("-") || x.StartsWith("+")).ToListSafe();

            foreach (var filter in filters)
            {
                var fields = filter.Split(new char[] { ':', '=' }, StringSplitOptions.RemoveEmptyEntries).ToListSafe();

                if (fields.Count == 0 || string.IsNullOrEmpty(fields[0]))
                    continue;

                var negate = (fields[0][0] == '-');
                var field = fields[0].ToLower().Trim('-', '+');
                var value = string.Empty;

                if (fields.Count > 1)
                {
                    value = fields[1];
                }

                switch (field)
                {
                    case "cycle":
                        results = FilterByString(field, value, results, negate);
                        break;
                    case "set":
                        results = FilterByString(field, value, results, negate);
                        break;
                    case "type":
                        results = FilterByEnum<CardType>(value, results, negate);
                        break;
                    case "sphere":
                        results = FilterByEnum<Sphere>(value, results, negate);
                        break;
                    case "rcost":
                    case "tcost":
                    case "ecost":
                    case "threat":
                    case "wp":
                    case "atk":
                    case "def":
                    case "hp":
                        results = FilterByByte(field, value, results, negate);
                        break;
                    case "trait":
                        results = FilterByString(field, value, results, negate);
                        break;
                    case "keyword":
                        results = FilterByString(field, value, results, negate);
                        break;
                    case "encounter":
                        results = FilterByString(field, value, results, negate);
                        break;
                    case "artist":
                        results = FilterByString(field, value, results, negate);
                        break;
                    case "category":
                        results = FilterByEnum<Category>(value, results, negate);
                        break;
                    case "victory":
                        results = FilterByByte(field, value, results, negate);
                        break;
                    case "unique":
                        results = FilterByBool(field, results, negate);
                        break;
                    case "custom":
                        results = FilterByBool(field, results, negate);
                        if (!negate)
                        {
                            searchModel.Custom = true;
                        }
                        break;
                    default:
                        break;
                }
            }

            return results;
        }

        /*
        public IEnumerable<Card> SearchOld(SearchViewModel model)
        {
            var hasFilter = model.HasFilter();
            var isAdvancedSearch = model.IsAdvancedSearch();
            var query = model.BasicQuery();

            var results = !string.IsNullOrEmpty(query) ?
                cards.Values.Where(
                    x => (x.Title.ToLower().Contains(query) || model.IsRandom())
                    || (!string.IsNullOrEmpty(x.NormalizedTitle) && x.NormalizedTitle.ToLower().Contains(query))
                    || (!string.IsNullOrEmpty(x.Text) && x.Text.ToLower().Contains(query))
                    || (!string.IsNullOrEmpty(x.OppositeText) && x.OppositeText.ToLower().Contains(query))
                    || x.Keywords.Any(y => y != null && y.ToLower().Contains(query))
                    || x.NormalizedKeywords.Any(y => y != null && y.ToLower().Contains(query))
                    || x.Traits.Any(y => y != null && y.ToLower().Contains(query))
                    || x.NormalizedTraits.Any(y => y != null && y.ToLower().Contains(query))
                    )
                .ToList()
                : cards.Values.ToList();

            if (model.CardType != CardType.None)
            {
                hasFilter = true;
                if (model.CardType == CardType.Player)
                {
                    results = results.Where(x => x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Attachment || x.CardType == CardType.Event).ToList();
                }
                else if (model.CardType == CardType.Character)
                {
                    results = results.Where(x => x.CardType == CardType.Hero || x.CardType == CardType.Ally || x.CardType == CardType.Objective_Ally || (x.CardType == CardType.Objective && x.HitPoints > 0)).ToList();
                }
                else if (model.CardType == CardType.Encounter)
                {
                    results = results.Where(x => x.CardType == CardType.Enemy || x.CardType == CardType.Location || x.CardType == CardType.Treachery || x.CardType == CardType.Objective || x.CardType == CardType.Objective_Ally).ToList();
                }
                else if (model.CardType == CardType.Objective)
                {
                    results = results.Where(x => x.CardType == CardType.Objective || x.CardType == CardType.Objective_Ally).ToList();
                }
                else if (model.CardType == CardType.Boon)
                {
                    results = results.Where(x => x.CampaignCardType == CampaignCardType.Boon).ToList();
                }
                else if (model.CardType == CardType.Burden)
                {
                    results = results.Where(x => x.CampaignCardType == CampaignCardType.Burden).ToList();
                }
                else
                    results = results.Where(x => x.CardType == model.CardType).ToList();
            }

            if (model.CardSet != null && model.CardSet != "Any")
            {
                hasFilter = true;
                results = results.Where(x => x.CardSet.Name == model.CardSet || (!string.IsNullOrEmpty(x.CardSet.AlternateName) && x.CardSet.AlternateName == model.CardSet) || (!string.IsNullOrEmpty(x.CardSet.NormalizedName) && x.CardSet.NormalizedName == model.CardSet) || (!string.IsNullOrEmpty(x.CardSet.Cycle) && x.CardSet.Cycle.ToUpper() == model.CardSet)).ToList();
            }

            if (model.Trait != null && model.Trait != "Any")
            {
                hasFilter = true;
                results = results.Where(x => x.HasTrait(model.Trait)).ToList();
            }

            if (model.Keyword != null && model.Keyword != "Any")
            {
                hasFilter = true;
                results = results.Where(x => x.HasKeyword(model.Keyword)).ToList();
            }

            if (model.Sphere != Sphere.None)
            {
                hasFilter = true;
                results = results.Where(x => x.Sphere == model.Sphere).ToList();
            }

            var category = model.GetCategory();
            if (category != Category.None)
            {
                hasFilter = true;
                results = results.Where(x => x.Categories.Any(y => y == category)).ToList();
            }

            if (!string.IsNullOrEmpty(model.Cost) && model.Cost != "Any")
            {
                hasFilter = true;
                results = results.Where(x => !string.IsNullOrEmpty(x.ResourceCostLabel) && x.ResourceCostLabel == model.Cost).ToList();
            }

            if (model.Unique)
            {
                hasFilter = true;
                results = results.Where(x => x.IsUnique).ToList();
            }

            if (model.IsUnique != Uniqueness.Any)
            {
                hasFilter = true;
                results = model.IsUnique == Uniqueness.Yes ?
                    results.Where(x => x.IsUnique).ToList()
                    : results.Where(x => !x.IsUnique).ToList();
            }

            if (!string.IsNullOrEmpty(model.Artist) && model.Artist != "Any")
            {
                hasFilter = true;
                results = results.Where(x => x.Artist != null && x.Artist.Name == model.Artist).ToList();
            }

            if (!string.IsNullOrEmpty(model.EncounterSet) && model.EncounterSet != "Any")
            {
                hasFilter = true;
                results = results.Where(x => !string.IsNullOrEmpty(x.EncounterSet) && x.EncounterSet == model.EncounterSet).ToList();
            }

            if (!string.IsNullOrEmpty(model.VictoryPoints) && model.VictoryPoints != "Any")
            {
                byte victoryPoints = 0;
                if (byte.TryParse(model.VictoryPoints.Replace("Victory", string.Empty).Trim('.'), out victoryPoints))
                {
                    hasFilter = true;
                    results = results.Where(x => x.VictoryPoints == victoryPoints).ToList();
                }
            }

            if (isAdvancedSearch)
            {
                results = AdvancedSearch(model, results);
            }

            var takeCount = hasFilter || model.IsRandom() ? results.Count : MAX_RESULTS;

            results = results.Take(takeCount).ToList();

            if (!model.Custom)
            {
                if ((model.CardSet == null || model.CardSet == "Any") && (model.EncounterSet == null || model.EncounterSet == "Any") && model.Sphere != Sphere.Mastery && (model.Trait == null || model.Trait == "Any") && (model.Keyword == null || model.Keyword == "Any"))
                {
                    results = results.Where(x => x.CardSet.SetType != SetType.Custom_Expansion).ToList();
                }
            }

            if (model.IsRandom())
            {
                var total = results.Count();
                if (total > 1)
                {
                    var random = new Random();
                    var choice = random.Next(0, total - 1);
                    results = new List<Card> { results[choice] };
                }

                return results;
            }
            else
            {
                switch (model.Sort)
                {
                    case Sort.Alphabetical:
                        return results.OrderBy(x => x.Title);
                    case Sort.Sphere_Type_Cost:
                        return results.OrderBy(x => x.Sphere).ThenBy(x => x.CardType).ThenBy(x => 
                            {
                                if (x.ThreatCost.HasValue && x.ThreatCost.Value > 0)
                                    return x.ThreatCost.Value;
                                else if (x.ResourceCost.HasValue && x.ResourceCost.Value > 0)
                                    return x.ResourceCost.Value;
                                else if (x.EngagementCost.HasValue && x.EngagementCost.Value > 0)
                                    return x.EngagementCost.Value;
                                else if (x.QuestPoints.HasValue && x.QuestPoints > 0)
                                    return x.QuestPoints.Value;
                                else
                                    return -1;
                            });
                    case Sort.Set_Number:
                    default:
                        return results.OrderBy(x => x.CardSet.Number).ThenBy(x => x.Number);
                 }
            }
        }*/

        public IEnumerable<Card> Search(SearchViewModel model)
        {
            var filters = new List<WeightedSearchFilter>();
            var results = new Dictionary<string, CardScore>();

            if (model.HasQuery)
            {
                filters.Add(new WeightedSearchFilter((s, c) => { return c.Title.IsEqualToLower(s.BasicQuery()); }, 200));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.NormalizedTitle.IsEqualToLower(s.BasicQuery()); }, 200));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.Title.StartsWithLower(s.BasicQuery()); }, 111));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.NormalizedTitle.StartsWithLower(s.BasicQuery()); }, 111));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.Title.ContainsLower(s.BasicQuery()); }, 100));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.NormalizedTitle.ContainsLower(s.BasicQuery()); }, 100));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.Text.ContainsLower(s.BasicQuery()); }, 100));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.OppositeText.ContainsLower(s.BasicQuery()); }, 100));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.Traits.Any(t => t.ToLowerSafe().Contains(s.BasicQuery())); }, 100));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.NormalizedTraits.Any(t => t.ToLowerSafe().Contains(s.BasicQuery())); }, 100));
                filters.Add(new WeightedSearchFilter((s, c) => { return c.Keywords.Any(k => k.ToLowerSafe().Contains(s.BasicQuery())); }, 100));
            }

            if (model.HasCardType)
                filters.Add(new WeightedSearchFilter((s, c) => { return s.CardTypeMatches(c); }, 100));

            if (model.HasCardSet)
                filters.Add(new WeightedSearchFilter((s, c) => { return s.CardSetMatches(c); }, 100));

            if (model.HasTrait)
                filters.Add(new WeightedSearchFilter((s, c) => { return c.HasTrait(s.Trait); }, 100));

            if (model.HasKeyword)
                filters.Add(new WeightedSearchFilter((s, c) => { return c.HasKeyword(s.Keyword); }, 100));
            
            if (model.HasSphere)
                filters.Add(new WeightedSearchFilter((s, c) => { return s.Sphere == c.Sphere; }, 100));

            if (model.HasCategory)
                filters.Add(new WeightedSearchFilter((s, c) => { return c.Categories.Any(x => x == s.GetCategory()); }, 100));

            if (model.HasCost)
                filters.Add(new WeightedSearchFilter((s, c) => { return s.Cost == c.ResourceCostLabel; }, 100));

            if (model.HasArtist)
                filters.Add(new WeightedSearchFilter((s, c) => { return s.Artist == c.Artist.Name; }, 100));

            if (model.HasEncounterSet)
                filters.Add(new WeightedSearchFilter((s, c) => { return s.EncounterSet == c.EncounterSet; }, 100));

            if (model.HasVictoryPoints)
                filters.Add(new WeightedSearchFilter((s, c) => { return s.VictoryPointsMatch(c); }, 100));
            
            if (model.Unique)
                filters.Add(new WeightedSearchFilter((s, c) => { return c.IsUnique; }, 100));

            if (model.IsUnique != Uniqueness.Any)
                filters.Add(new WeightedSearchFilter((s, c) => { return (s.IsUnique == Uniqueness.Yes && c.IsUnique) || (s.IsUnique == Uniqueness.No && !c.IsUnique); }, 50));

            if (filters.Count > 0)
            {
                foreach (var card in cards.Values)
                {
                    foreach (var filter in filters)
                    {
                        var score = filter.Score(model, card);
                        
                        if (results.ContainsKey(card.Id))
                        {
                            var existing = results[card.Id].Score;
                            if (score == 0 || (existing > 0 && score > existing))
                            {
                                results[card.Id].Score = score;
                            }
                        }
                        else
                        {
                            results[card.Id] = new CardScore(card, score);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in cards.Where(x => x.Value.CardSet.Name == "Core Set" && x.Value.Number < 74))
                {
                    results[item.Value.Id] = new CardScore(item.Value, 74 - item.Value.Number);
                }
            }

            if (!model.Custom)
            {
                var official = new Dictionary<string, CardScore>();

                foreach (var score in results)
                {
                    if (!model.IsCustom(score.Value.Card))
                        official.Add(score.Key, score.Value);
                }

                results = official;
            }

            if (model.IsRandom())
            {
                var total = results.Count();
                if (total > 1)
                {
                    var random = new Random();
                    var choice = random.Next(0, total - 1);
                    var score = results.Values.ToList()[choice];
                    results = new Dictionary<string, CardScore>();
                    results[score.Card.Id] = score;
                }
            }

            var takeCount = model.HasFilter() || model.IsRandom() ? results.Count : MAX_RESULTS;

            var sortedResults = new List<Card>();

            switch (model.Sort)
            {
                case Sort.Alphabetical:
                    sortedResults = results.Where(x => x.Value.Score > 0).OrderBy(x => x.Value.Card.Title).Select(x => x.Value.Card).Take(takeCount).ToList();
                    break;
                case Sort.Sphere_Type_Cost:
                    sortedResults = results.Where(x => x.Value.Score > 0).OrderBy(x => x.Value.Card.Sphere).ThenBy(x => x.Value.Card.CardType).ThenBy(x =>
                    {
                        if (x.Value.Card.ThreatCost.HasValue && x.Value.Card.ThreatCost.Value > 0)
                            return x.Value.Card.ThreatCost.Value;
                        else if (x.Value.Card.ResourceCost.HasValue && x.Value.Card.ResourceCost.Value > 0)
                            return x.Value.Card.ResourceCost.Value;
                        else if (x.Value.Card.EngagementCost.HasValue && x.Value.Card.EngagementCost.Value > 0)
                            return x.Value.Card.EngagementCost.Value;
                        else if (x.Value.Card.QuestPoints.HasValue && x.Value.Card.QuestPoints > 0)
                            return x.Value.Card.QuestPoints.Value;
                        else
                            return -1;
                    }).Select(x => x.Value.Card).Take(takeCount).ToList();
                    break;
                case Sort.Set_Number:
                    sortedResults = results.Where(x => x.Value.Score > 0).OrderBy(x => x.Value.Card.CardSet.Number).ThenBy(x => x.Value.Card.Number).Select(x => x.Value.Card).Take(takeCount).ToList();
                    break;
                default:
                    sortedResults = results.Where(x => x.Value.Score > 0).OrderByDescending(x => x.Value.Score).Select(y => y.Value.Card).Take(takeCount).ToList();
                    break;
            }

            if (model.IsAdvancedSearch())
            {
                sortedResults = AdvancedSearch(model, sortedResults);
            }

            return sortedResults;
        }

        public Card Find(string id)
        {
            return cards.ContainsKey(id) ?
                cards[id]
                : null;
        }

        public Card FindBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;

            var exact = cards.Values.Where(x => x.Slug.ToLower() == slug.ToLower()).FirstOrDefault();
            if (exact != null)
                return exact;

            var partials = cards.Values.Where(x => x.Slug.ToLower().StartsWith(slug.ToLower())).ToList();
            if (partials.Count == 1)
                return partials.First();

            return null;
        }

        public IEnumerable<string> Costs()
        {
            return cards.Values.Where(x => !string.IsNullOrEmpty(x.ResourceCostLabel)).Select(x => x.ResourceCostLabel).Distinct().OrderBy(x => x).ToList();
        }

        public IEnumerable<string> SetNames
        {
            get { return setNames; }
        }

        public IEnumerable<CardSet> CardSets()
        {
            return sets;
        }

        public IEnumerable<string> EncounterSetNames
        {
            get { return encounterSetNames.Keys; }
        }

        public IEnumerable<string> Keywords()
        {
            return keywords.Values.ToList().OrderBy(x => x).ToList();
        }

        public IEnumerable<string> Traits()
        {
            return traits.Values.ToList().OrderBy(x => x).ToList();
        }

        public IEnumerable<string> Spheres()
        {
            foreach (var item in SearchViewModel.Spheres)
                yield return item.Text;
        }

        public IEnumerable<Category> Categories()
        {
            return categories.Values.ToList().OrderBy(x => x).ToList();
        }

        public IEnumerable<Product> Products()
        {
            return products;
        }

        public IEnumerable<ProductGroup> ProductGroups()
        {
            return productGroups;
        }

        public IEnumerable<ScenarioGroup> ScenarioGroups()
        {
            var scenarioGroups = new Dictionary<string, ScenarioGroup>();

            foreach (var scenario in scenarios)
            {
                var name = scenario.Value.GroupName;

                if (!scenarioGroups.ContainsKey(name))
                {
                    scenarioGroups.Add(name, new ScenarioGroup() { Name = name });
                }

                scenarioGroups[name].AddScenario(scenario.Value);
            }

            return scenarioGroups.Values.ToList();
        }

        public Scenario GetScenario(string scenarioTitle)
        {
            return scenarios.ContainsKey(scenarioTitle) ?
                scenarios[scenarioTitle]
                : null;
        }

        public IEnumerable<string> VictoryPointValues()
        {
            return victoryPointValues.Keys.OrderBy(x => x).Select(y => victoryPointValues[y]).ToList();
        }
    }
}