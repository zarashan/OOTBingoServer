using Newtonsoft.Json;

namespace ZootrBingoServer.BingoFiles;

public class BingoItem
{
    public string name { get; set; }

    [JsonIgnore] private static List<CollectionType> CollectionTypes = Enum.GetValues<CollectionType>().ToList();
    [JsonIgnore] private static List<WierdType> WierdTypes = Enum.GetValues<WierdType>().ToList();
    [JsonIgnore] private static List<KillType> KillTypes = Enum.GetValues<KillType>().ToList();
    [JsonIgnore] private static List<SceneType> SceneTypes = Enum.GetValues<SceneType>().ToList();

    public static void ResetBingo()
    {
        CollectionTypes = Enum.GetValues<CollectionType>().ToList();
        WierdTypes = Enum.GetValues<WierdType>().ToList();
        KillTypes = Enum.GetValues<KillType>().ToList();
        SceneTypes = Enum.GetValues<SceneType>().ToList();
    }

    private static BingoItem CreateBingoItem(string Generatedname)
    {
        return new BingoItem
        {
            name = Generatedname
        };
    }

    public static BingoItem GenerateCollection(Random Rand)
    {
        var square = CollectionTypes[Rand.Next(CollectionTypes.Count)];
        CollectionTypes.Remove(square);
        switch (square)
        {
            case CollectionType.ZoraGear1:
                return CreateBingoItem("Zora Gear (Tunic, Irons, and Longshot)");
            case CollectionType.GoronGear1:
                return CreateBingoItem("Goron Gear (Tunic, Biggoron Sword, and Hammer)");
            case CollectionType.AllSwordsTunics:
                return CreateBingoItem("3 Tunics and 3 Swords");
            case CollectionType.AllTunicBoots:
                return CreateBingoItem("3 Tunics and 3 Boots");
            case CollectionType.AllSwordsTunicsBoots:
                return CreateBingoItem("3 Tunics, 3 Swords and 3 Boots");
            case CollectionType.AllShieldAndSwords:
                return CreateBingoItem("3 Shields and 3 Swords");
            case CollectionType.XMedalions:
                var dungeonRewards = Enum.GetValues<DungeonRewards>().ToList();
                var count = Rand.Next(2, dungeonRewards.Count);

                if (count == dungeonRewards.Count)
                {
                    return CreateBingoItem("All Medallions");
                }

                var selectedDungeonRewards = new List<DungeonRewards>();
                for (int i = 0; i < count; i++)
                {
                    var dungeonReward = dungeonRewards[Rand.Next(dungeonRewards.Count)];
                    selectedDungeonRewards.Add(dungeonReward);
                    dungeonRewards.Remove(dungeonReward);
                }

                if (selectedDungeonRewards.All(b => b <= DungeonRewards.Zora))
                {
                    return CreateBingoItem($"Collect {selectedDungeonRewards.Count} Spirital Stones");
                }

                if (selectedDungeonRewards.All(b => b >= DungeonRewards.Light))
                {
                    return CreateBingoItem($"Collect {selectedDungeonRewards.Count} Medallions");
                }

                if (count > 3)
                {
                    return CreateBingoItem($"Collect {selectedDungeonRewards.Count} Dungeon Rewards");
                }

                return CreateBingoItem($"Collect {string.Join("&", selectedDungeonRewards.Select(b => b.ToString()).ToArray())} Dungeon Rewards");

            case CollectionType.AllMedalions:
                return CreateBingoItem("All Medallions");
            case CollectionType.ZeldaThrownTreasure:
                return CreateBingoItem("Get the treasure thrown by Princess Zelda");
            case CollectionType.Greg:
                return CreateBingoItem("Greg");
            case CollectionType.MapsAndCompass:
                var DungeonList = Enum.GetValues<Dungeon>().ToList();
                return CreateBingoItem($"Map and Compass in {(Dungeon) Rand.Next(DungeonList.Count)}");
            case CollectionType.Maps6:
                return CreateBingoItem($"Collect 6 Maps");
            case CollectionType.Maps4:
                return CreateBingoItem($"Collect 4 Maps");
            case CollectionType.Compasses6:
                return CreateBingoItem($"Collect 6 Compasses");
            case CollectionType.Compasses4:
                return CreateBingoItem($"Collect 4 Compasses");
            case CollectionType.XSongs:
                return CreateBingoItem($"Collect {((SongTypes) Rand.Next(Enum.GetValues<SongTypes>().Length)).ToString().Replace("_", "'")}");
            case CollectionType.AllSongs:
                return CreateBingoItem($"All Songs");
            case CollectionType.AllTopRowSongs:
                return CreateBingoItem($"All Top Row Songs");
            case CollectionType.AllWarpSongs:
                return CreateBingoItem($"All Warp Songs");
            case CollectionType.Trade3AdultTrade:
                return CreateBingoItem($"Trade 3 Adult Trade Items");
            case CollectionType.XSpells:

                var Spells = Enum.GetValues<Spells>();
                if (Rand.Next(100) % 2 > 0)
                {
                    return CreateBingoItem($"Collect 3 Magic Spells");
                }

                return CreateBingoItem(((Spells) Rand.Next(Spells.Length)).ToString());

            case CollectionType.FillXRowInventory:

                switch (Rand.Next(2))
                {
                    default:
                        return CreateBingoItem("Collect all items on the Top Row");
                    case 1:
                        return CreateBingoItem("Collect all items for the Second Row");
                }
            case CollectionType.XHearts:
                return CreateBingoItem($"{Rand.Next(11, 14)} Hearts");
            case CollectionType.PoeInBottle:
                return CreateBingoItem("Poe In a Bottle");
            case CollectionType.BottlePercent:
                return CreateBingoItem("4 Bottles");
            case CollectionType.BossKeys:
                switch (Rand.Next(8))
                {
                    default:
                        return CreateBingoItem("Forest Temple Boss Key");
                    case 1:
                        return CreateBingoItem("Fire Temple Boss Key");
                    case 2:
                        return CreateBingoItem("Water Temple Boss Key");
                    case 3:
                        return CreateBingoItem("Shadow Temple Boss Key");
                    case 4:
                        return CreateBingoItem("Spirit Temple Boss Key");
                    case 5:
                        return CreateBingoItem("Forest Temple Boss Key");
                    case 6:
                        return CreateBingoItem("Ganon Temple Boss Key");
                    case 7:
                        return CreateBingoItem($"{(Rand.Next(100) % 2 == 0 ? "3" : "4")} boss keys");
                }

            case CollectionType.Xkeys:
                var keyRings = Enum.GetValues<KeyRings>().ToList();

                var selectedDungeonKeys = new List<KeyRings>();

                for (int i = 0; i < 2; i++)
                {
                    var keyRingRand = keyRings[Rand.Next(keyRings.Count)];
                    selectedDungeonKeys.Add(keyRingRand);
                    keyRings.Remove(keyRingRand);
                }

                return CreateBingoItem($"{string.Join("&", selectedDungeonKeys.Select(b => b.ToString()))} Keyrings");


            case CollectionType.XDekuCapacity:
                switch (Rand.Next(2))
                {
                    default:
                        return CreateBingoItem("20 Deku Stick Capacity");
                    case 1:
                        return CreateBingoItem("30 Deku Stick Capacity");
                }

                break;
            case CollectionType.XNutCapacity:
                switch (Rand.Next(2))
                {
                    default:
                        return CreateBingoItem("30 Deku Nut Capacity");
                    case 1:
                        return CreateBingoItem("40 Deku Nut Capacity");
                }

                break;
                break;
            case CollectionType.XBulletCapacity:
                switch (Rand.Next(2))
                {
                    default:
                        return CreateBingoItem("40 Bullet Capacity");
                    case 1:
                        return CreateBingoItem("50 Bullet Capacity");
                }

                break;
                break;
            case CollectionType.XQuiverCapacity:
                switch (Rand.Next(2))
                {
                    default:
                        return CreateBingoItem("40 Quiver");
                    case 1:
                        return CreateBingoItem("50 Quiver");
                }
            case CollectionType.XBombCapacity:
                switch (Rand.Next(2))
                {
                    default:
                        return CreateBingoItem("30 Bomb Bag");
                    case 1:
                        return CreateBingoItem("40 Bomb Bag");
                }
            case CollectionType.GoldenGauntlets:
                return CreateBingoItem("Golden Gauntlets");
            case CollectionType.GoldenScale:
                return CreateBingoItem("Golden Scale");
            case CollectionType.GoMode:
                return CreateBingoItem("Go Mode (Have all of Bow, Lights, Magic, Master Sword, Ganon BK)");
            case CollectionType.XSword:
                switch (Rand.Next(4))
                {
                    default:
                        return CreateBingoItem("Kokiri Sowrd");
                    case 1:
                        return CreateBingoItem("Master Sword");
                    case 2:
                        return CreateBingoItem("Biggoron Sword");
                    case 3:
                        return CreateBingoItem("All Swords");
                }
            case CollectionType.XTunic:
                switch (Rand.Next(3))
                {
                    default:
                        return CreateBingoItem("Goron Tunic");
                    case 1:
                        return CreateBingoItem("Zora Tunic");
                    case 2:
                        return CreateBingoItem("All Tunic");
                }
            case CollectionType.XShield:
                switch (Rand.Next(4))
                {
                    default:
                        return CreateBingoItem("Kokiri Shield");
                    case 1:
                        return CreateBingoItem("Hylian Shield");
                    case 2:
                        return CreateBingoItem("Mirror Shield");
                    case 3:
                        return CreateBingoItem("All Shields");
                }
            case CollectionType.XMagicArrow:
                switch (Rand.Next(4))
                {
                    default:
                        return CreateBingoItem("Fire Arrow");
                    case 1:
                        return CreateBingoItem("Ice Arrow");
                    case 2:
                        return CreateBingoItem("Light Arrow");
                    case 3:
                        return CreateBingoItem("All Magic Arrow");
                }
            case CollectionType.StoneOfBurton:
                return CreateBingoItem("Stone Of Agony");
            case CollectionType.DungeonSkulltulas:
                switch (Rand.Next(10))
                {
                    default:
                        return CreateBingoItem("All 4 Deku Tree Skulltulas");
                    case 1:
                        return CreateBingoItem("All 5 Dodongo's Cavern Skulltulas");
                    case 2:
                        return CreateBingoItem("All 5 Fire Temple Skulltulas");
                    case 3:
                        return CreateBingoItem("Both Scarecrow Skulltulas in Fire Temple");
                    case 4:
                        return CreateBingoItem("All 5 Forest Temple Skulltulas");
                    case 5:
                        return CreateBingoItem("All 3 Ice Cavern Skulltulas");
                    case 6:
                        return CreateBingoItem("All 4 Jabu-Jabu's Belly Skulltulas");
                    case 7:
                        return CreateBingoItem("All 5 Shadow Temple Skulltulas");
                    case 8:
                        return CreateBingoItem("All 5 Spirit Temple Skulltulas");
                    case 9:
                        return CreateBingoItem("All 5 Water Temple Skulltulas");
                }
            case CollectionType.AreaSkulltulas:
                switch (Rand.Next(10))
                {
                    default:
                        return CreateBingoItem("All 4 Lost Woods area Skulltulas");
                    case 1:
                        return CreateBingoItem("All 3 Kokiri Forest area Skulltulas");
                    case 2:
                        return CreateBingoItem("All 8 Kakariko area Skulltulas");
                    case 3:
                        return CreateBingoItem("All 5 Lake Hylia Skulltulas");
                    case 4:
                        return CreateBingoItem("All 4 Lon-Lon Ranch area Skulltulas");
                    case 5:
                        return CreateBingoItem("All 8 Zora's River/Domain Skulltulas");
                    case 6:
                        return CreateBingoItem("Both Gerudo Fortress Skulltulas");
                    case 7:
                        return CreateBingoItem("Both Hyrule Field area Skulltulas");
                    case 8:
                        return CreateBingoItem("Both Hyrule Castle (Child) Skulltulas");
                    case 9:
                        return CreateBingoItem("4 Soft Soil Skulltulas");
                }
            case CollectionType.XTotalSkulltulas:
                switch (Rand.Next(3))
                {
                    default:
                        return CreateBingoItem("Obtain any prize from the House of Skulltula");
                    case 1:
                        return CreateBingoItem("20 Skulltula Tokens");
                    case 2:
                        return CreateBingoItem("30 Skulltula Tokens");
                }
            case CollectionType.ChestOpening:
                switch (Rand.Next(8))
                {
                    default:
                        return CreateBingoItem("Open Haunted Wasteland Chest");
                    case 1:
                        return CreateBingoItem("Open Both Timer Chests in Firetemple");
                    case 2:
                        return CreateBingoItem("Open the Chest in the Toilet Room in Gerudo Training Ground");
                    case 3:
                        return CreateBingoItem("Open the Golden Gauntlets Chest in Ganon's Castle");
                    case 4:
                        return CreateBingoItem("Open the Chests on both of the Desert Colossus Hands");
                    case 5:
                        return CreateBingoItem("Open the Ice Arrow Chest in Gerudo Training Grounds");
                    case 6:
                        return CreateBingoItem("Open the Ganon Boss Key Chest");
                    case 7:
                        return CreateBingoItem("Complete Chest Game");
                }
            case CollectionType.GrannysPotion:
                return CreateBingoItem("Buy Grannys Potion");
            case CollectionType.XGreatFairy:
                switch (Rand.Next(3))
                {
                    default:
                        return CreateBingoItem("Access 4 Fairy Fountains");
                    case 1:
                        return CreateBingoItem("Perform at 4 Fairy Fountains");
                    case 2:
                        return CreateBingoItem("Play at Outside Ganon Castle Great Fairy");
                }
            case CollectionType.SlingShotBow:
                return CreateBingoItem("Slingshot and Bow");
            case CollectionType.CItem:
                return CreateBingoItem(((Item) Rand.Next(Enum.GetValues<Item>().Length)).ToString());
            case CollectionType.Magic:
                switch (Rand.Next(2))
                {
                    default:
                        return CreateBingoItem("Single Magic");
                    case 1:
                        return CreateBingoItem("Double Magic");
                }
        }

        return CreateBingoItem("I Broke Sorry GenerateCollection");
    }


    public static BingoItem GenerateWierd(Random Rand)
    {
        var square = WierdTypes[Rand.Next(WierdTypes.Count)];
        WierdTypes.Remove(square);
        switch (square)
        {
            case WierdType.ThawZora:
                return CreateBingoItem("Thaw king Zora (No Glitches)");
            case WierdType.ThawBoth:
                return CreateBingoItem("Thaw the entrance to the Zora Shop as Adult");
            case WierdType.BombChuBowling:
                return CreateBingoItem("Win Bombchu Bowling at least once");
            case WierdType.AllBiggoron:
                return CreateBingoItem("All Biggoron checks");
            case WierdType.LeftSideOfShop:
                return CreateBingoItem("Buy All Items On the Left Side Shop");
                break;
            case WierdType.NavigateHauntedToCollossus:
                return CreateBingoItem("Navigate Haunted Wasteland to get to Desert Colossus");
                break;
            case WierdType.ReachEndGanonATrial:
                switch (Rand.Next(5))
                {
                    default:
                        return CreateBingoItem("Get to the end of Water Trial");
                    case 1:
                        return CreateBingoItem("Get to the end of Spirit Trial");
                    case 2:
                        return CreateBingoItem("Get to the end of Forest Trial");
                    case 3:
                        return CreateBingoItem("Get to the end of Shadow Trial");
                    case 4:
                        return CreateBingoItem("Get to the end of Light Trial");
                }

                break;
            case WierdType.FullAndHalfMilk:
                return CreateBingoItem("Have one full and one half bottle of milk at the same time");
                break;
            case WierdType.XSoftSoilPlants:

                var soilString = Rand.Next(100) % 2 == 0 ? "Plant a Bean Next to Bean Daddy" : "Plant the Death Mountain Crater Bean";

                return CreateBingoItem(soilString);
                break;
            case WierdType.FeedLittleDodongo:
                return CreateBingoItem("Feed a Regular Dodongo a Bomb");
            case WierdType.TossRutoIntoDrink:
                return CreateBingoItem("Toss Princess Ruto Into the Drink");
            case WierdType.FrozenByKeese:
                return CreateBingoItem("Get Frozen By an Ice Keese");
            case WierdType.XSalesMan:
                switch (Rand.Next(2))
                {
                    default:
                        return CreateBingoItem("Buy Carpet Salesman's Item");
                    case 1:
                        return CreateBingoItem("Buy from Bean Daddy");
                }

                break;
            case WierdType.AllDungeons:
                return CreateBingoItem("Beat All Dungeons");
                break;
            case WierdType.XDungeons:
                switch (Rand.Next(11))
                {
                    default:
                        return CreateBingoItem("Buy Carpet Salesman's Item");
                    case 1:
                        return CreateBingoItem("Beat 2 Temples");
                    case 2:
                        return CreateBingoItem("Beat 4 Temples");
                    case 3:
                        return CreateBingoItem("Beat Deku Tree");
                    case 4:
                        return CreateBingoItem("Beat Dodongo's Cavern");
                    case 5:
                        return CreateBingoItem("Beat Jabu-Jabu's Belly");
                    case 6:
                        return CreateBingoItem("Beat Forest Temple");
                    case 7:
                        return CreateBingoItem("Beat Fire Temple");
                    case 8:
                        return CreateBingoItem("Beat Water Temple");
                    case 9:
                        return CreateBingoItem("Beat Shadow Temple");
                    case 10:
                        return CreateBingoItem("Beat Spirit Temple");
                }
            case WierdType.BuyXDekuScrubs:
                return CreateBingoItem("Buy from 10 Deku Scrubs");
                break;
            case WierdType.FlattenGossipStone:
                return CreateBingoItem("Flatten the Gossip Stone behind the wall in Death Mountain Crater");
                break;
            case WierdType.DivingMinigame:
                return CreateBingoItem("Feed a Regular Dodongo a Bomb");
                break;
            case WierdType.XShootingGallery:
                return CreateBingoItem("Win Every Shooting Gallery Minigame (Horsebslkack  Archery Included)");
                break;
            case WierdType.WakeTalonAsAdult:
                return CreateBingoItem("Feed a Regular Dodongo a Bomb");
                break;
            case WierdType.XPierre:
                return CreateBingoItem("Summon Pierre in 3 different locations");
                break;
            case WierdType.XSongStormsGrotto:
                return CreateBingoItem("Access 3 Song of Storms grottos");
                break;
            case WierdType.EnterAreaGrotto:
                var t = Rand.Next(100) % 2 == 0 ? "Enter both Kakariko Village grottos" : "Access 3 Song of Storms grottos";
                return CreateBingoItem(t);
            case WierdType.FrogsSongOfStorms:
                return CreateBingoItem("Frogs Song of Storms Check");
            // case WierdType.XFrogsBig:
            //     break;
            case WierdType.XRollingGoron:
                return CreateBingoItem("Stop Both Rolling Gorons in Goron City");
                break;
            case WierdType.BigGoronPotHappy:
                return CreateBingoItem("Make the Goron City Big Pot Happy");
                break;
            case WierdType.ZoraFountainFreeStanding:
                return CreateBingoItem("Get Both Freestanding Items in Zora's Fountain");
                break;
            case WierdType.RepairGerudoValleyBridge:
                return CreateBingoItem("Repair the Gerudo Valley Bridge (Save the Carpenter)");
                break;
            case WierdType.StatuePiercingHookShot:
                return CreateBingoItem("Give the Statue in Spirit Temple a Nipple Piercing with your Hookshot");
                break;
            case WierdType.XScareDekuScrubsDt:
                return CreateBingoItem("Scare off all 5 Deku Scrubs in Deku Tree");
                break;
            case WierdType.FillOasis:
                return CreateBingoItem("Fill the Oasis at Desert Colossus");
                break;
        }

        return CreateBingoItem("I Broke Sorry GenerateWierd");
    }

    public static BingoItem GenerateKill(Random Rand)
    {
        var square = KillTypes[Rand.Next(KillTypes.Count)];
        KillTypes.Remove(square);
        switch (square)
        {
            case KillType.XBosses:
                var BossList = Enum.GetValues<Bosses>().ToList();
                var count = Rand.Next(5, BossList.Count);
                return CreateBingoItem($"Defeat {count} Bosses");

            // if (count == BossList.Count)
            // {
            //     return CreateBingoItem("All Bosses");
            // }
            //
            // var SelectedBosses = new List<Bosses>();
            // for (int i = 0; i < count; i++)
            // {
            //     var boss = BossList[Rand.Next(BossList.Count)];
            //     SelectedBosses.Add(boss);
            //     BossList.Remove(boss);
            // }
            //
            // if(SelectedBosses.All(b=> b < Bosses.PhantomGanon))
            // {
            //     var newList = BossList.Where(b => b >= Bosses.PhantomGanon).ToList();
            //     SelectedBosses.Add(newList[Rand.Next(newList.Count)]);
            // }
            //
            // if (SelectedBosses.All(b => b <= DungeonRewards.Zora))
            // {
            //     return CreateBingoItem($"Collect {SelectedBosses.Count} Spirital Stones");
            // }
            // if (SelectedBosses.All(b => b >= DungeonRewards.Light))
            // {
            //     return CreateBingoItem($"Collect {SelectedBosses.Count} Medallions");
            // }
            // if (count > 4)
            // {
            //     return CreateBingoItem($"Defeat {SelectedBosses.Count} Bosses");
            // }

            //return CreateBingoItem($"Defeat {string.Join("&", SelectedBosses.Select(b => b.ToString()).ToArray())}");
            case KillType.BigOcto:
                return CreateBingoItem("Defeat Big Octo");
            case KillType.IronNuckles:
                return CreateBingoItem("Kill 4 Different Iron Knuckles");
            case KillType.Lizalfos:
                var lizalfosText = Rand.Next(100) % 2 == 0 ? "Kill all 4 Lizalfos in Spirit Temple" : "Kill all 4 Lizalfos in Dodongo's Cavern";
                return CreateBingoItem(lizalfosText);
            case KillType.GreenBubbles:
                return CreateBingoItem("Kill 2 Different Green Bubbles");
            case KillType.WhiteBubbles:
                return CreateBingoItem("Kill a White Bubble");
            case KillType.BigPoe:
                return CreateBingoItem("Kill and catch a Big Poe in Hyrule Field");
            case KillType.Dinolfos:
                return CreateBingoItem("Kill 4 Different Dinolfos");
            case KillType.DarkLink:
                return CreateBingoItem("Defeat Dark Link");
            case KillType.LikeLike:
                return CreateBingoItem("Kill a Like-Like in 3 different dungeons");
            case KillType.FlareDancers:
                return CreateBingoItem("Defeat both Flare Dancers");
            case KillType.DefeatForestTemplePoe:
                switch (Rand.Next(3))
                {
                    default:
                        return CreateBingoItem("Defeat Amy (Green Forest Poe)");
                    case 1:
                        return CreateBingoItem("Defeat Joelle and Beth (Red and Blue Poe)");
                    case 2:
                        return CreateBingoItem("Defeat Meg (Purple Poe)");
                }
            case KillType.PeaHat:
                return CreateBingoItem("Defeat a Peahat");
            case KillType.SkullKid:
                return CreateBingoItem("Defeat a Skull Kid");
            case KillType.XWhiteWolfos:
                var whiteWolf = Rand.Next(100) % 2 == 0 ? "Defeat 3 Different White Wolfos" : "Beat the White Wolfos in Ice Cavern";
                return CreateBingoItem(whiteWolf);
            case KillType.MoblinsForestMaze:
                return CreateBingoItem("Defeat Every Moblin in the Forest Maze");
            case KillType.AllLikeLikeWolfosGTG:
                return CreateBingoItem("Kill all the Wolfos and Like-Likes in Gerudo Training Ground");
            case KillType.GerodoInCombat:
                return CreateBingoItem("Best a Gerudo in combat");
            case KillType.DeadHand:
                var DeadHandCheck = Rand.Next(100) % 2 == 0 ? "Beat both Dead Hands" : "Beat the Dead Hand in Bottom of the Well";
                return CreateBingoItem(DeadHandCheck);
        }

        return CreateBingoItem("I Broke Sorry GenerateKill");
    }

    public static BingoItem GenerateScene(Random Rand)
    {
        var square = SceneTypes[Rand.Next(SceneTypes.Count)];
        SceneTypes.Remove(square);
        switch (square)
        {
            case SceneType.XDungeonSilverRuppee:
                switch (Rand.Next(3))
                {
                    default:
                        return CreateBingoItem("Complete all 3 Silver Rupee Challenges in Shadow Temple");
                    case 1:
                        return CreateBingoItem("Complete Both Silver Rupee Challenges in Ice Cavern");
                    case 2:
                        return CreateBingoItem("Complete all 4 Silver Rupee Challenges in Ganon's Castle");
                }
            case SceneType.LowerWaterBotw:
                return CreateBingoItem("Lower the water in Bottom of the Well");
            case SceneType.SetWaterMidLevel:
                return CreateBingoItem("Set the water to the middle level in Water Temple");
            case SceneType.TwistUnTwistForest:
                return CreateBingoItem("Twist/Untwist a Hallway in Forest Temple");
            case SceneType.BurnSpikedWallsShadow:
                return CreateBingoItem("Burn the wooden spiked walls in Shadow Temple");
            case SceneType.XZlTriforcesSpirit:
                return CreateBingoItem("Play Zelda's Lullaby for all 3 Triforces in Spirit Temple");
            case SceneType.RescueXGoronsFire:
                return CreateBingoItem($"Rescue all {(Rand.Next(100) % 2 == 0 ? 9 : 5)} Gorons in Fire Temple");
            case SceneType.XSpeakDekuScrubDungeon:
                return CreateBingoItem("Speak with all 4 Business Scrubs in Dodongo's Cavern");
            case SceneType.OpenBossDoor:
                var dungeonDoors = Enum.GetValues<Dungeon>().Where(b => b >= Dungeon.Forest).ToList();
                var t = (Dungeon) Rand.Next(dungeonDoors.Count);
                switch (t)
                {
                    case Dungeon.Forest:
                        return CreateBingoItem("Open Boss Key Door for Forest");
                    case Dungeon.Fire:
                        return CreateBingoItem("Open Boss Key Door for Fire");
                    case Dungeon.Water:
                        return CreateBingoItem("Open Boss Key Door for Water");
                    case Dungeon.Shadow:
                        return CreateBingoItem("Open Boss Key Door for Shadow");
                    default:
                        return CreateBingoItem("Open Boss Key Door for Spirit");
                }
            // case SceneType.UseXKeysDungeons:
            //     return CreateBingoItem("Lower");
            case SceneType.HammerSwitchesSpirit:
                return CreateBingoItem("Both Hammer Switches in Spirit Temple");
            case SceneType.MedallionCheck:
                return CreateBingoItem(Rand.Next(100) % 2 == 0 ? "Prelude of Light Check (Master Sword pedestal with Forest Medallion)" : "Nocturne of Shadow Check (Go to Kak with Forest, Fire, and Water Medallion)");
            case SceneType.SariaSongCheckBoth:
                return CreateBingoItem("Both Saria's Song Checks (Darunia and Skull Kid)");
            case SceneType.XMilkCows:
                switch (Rand.Next(3))
                {
                    default:
                        return CreateBingoItem("Milk 5 Different Cows");
                    case 1:
                        return CreateBingoItem("Cow in Link's House");
                    case 2:
                        return CreateBingoItem("Milk 5 Different Cows");
                }
            case SceneType.ExposeBaldManFishing:
                return CreateBingoItem("Expose the Fishing Man as Bald");
            case SceneType.XShootEyeDungeon:
                return CreateBingoItem(Rand.Next(100) % 2 == 0 ? "Shoot 5 Different Eye Switches" : "Shoot an Eye Switch in 4 different dungeons");
            case SceneType.XSuperFairies:
                return CreateBingoItem("Spawn and collect any 3 Super Fairies");
            // case SceneType.XGossipFairies:
            //     return CreateBingoItem("Lower");
            //     break;
            case SceneType.BothRoyalGraveChecks:
                return CreateBingoItem("Both Checks in the Royal Family's Tomb");
        }

        return CreateBingoItem("I Broke Sorry GenerateScene");
    }
}