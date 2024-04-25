using System.Diagnostics;

namespace Sparta_Dungeon_Game
{
    internal class Program
    {
        private static int level = 1;
        private static string name = "Chad";
        private static string job = "전사";
        private static int attack = 10;        
        private static int defense = 5;        
        private static int health = 100;
        private static int gold = 1500;               

        private class Item
        {
            public string Name { get; set; }
            public int AttackBonus { get; set; }
            public int DefenseBonus { get; set; }
            public bool Equipped { get; set; }
            public int Price { get; set; }

            public Item(string name, int attackBonus, int defenseBonus, int price)
            {
                Name = name;
                AttackBonus = attackBonus;
                DefenseBonus = defenseBonus;
                Equipped = false;
                Price = price;
            }
        }

        private static List<Item> inventory = new List<Item>
            {
              new Item("무쇠갑옷", 0, 9, 1500),
              new Item("스파르타의 창", 7, 0, 2000),
              new Item("낡은 검", 2, 0, 600)
            };

        private class ShopItem
        {
            public string Name { get; set; }
            public int AttackBonus { get; set; }
            public int DefenseBonus { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public bool Purchased { get; set; }

            public ShopItem(string name, int attackBonus, int defenseBonus, string description, int price)
            {
                Name = name;
                AttackBonus = attackBonus;
                DefenseBonus = defenseBonus;
                Description = description;
                Price = price;
                Purchased = false;
            }
        }

        private static List<ShopItem> Shopinventory = new List<ShopItem>
        {
            new ShopItem("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
            new ShopItem("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
            new ShopItem("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000),
            new ShopItem("황충의 활", 30, 0, "삼국지의 황충이 사용하던 활입니다.", 9000),
            new ShopItem("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
            new ShopItem("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500),
            new ShopItem("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
            new ShopItem("황충의 갑옷", 0, 25, "삼국지의 황충이 착용하던 갑옷입니다.", 8000)

        };

        static void Main(string[] args)
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            VillageMenu();                      

            static void VillageMenu()
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("1. 상태 보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine();
                    Console.Write("원하시는 행동을 입력해주세요: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ShowStat();
                            break;
                        case "2":
                            ShowInventory();
                            break;
                        case "3":
                            VisitShop();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
            }

            static void ShowStat()
            {
                int totalAttack = attack + GetEquipAttack();
                int totalDefense = defense + GetEquipDefense();

                Console.WriteLine("☆상태 보기☆");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();

                Console.WriteLine($"Lv. {level:D2}");
                Console.WriteLine($"{name} ( {job} )");
                Console.WriteLine($"공격력 : {totalAttack} (+{GetEquipAttack()})");
                Console.WriteLine($"방어력 : {totalDefense} (+{GetEquipDefense()})");
                Console.WriteLine($"체 력 : {health}");
                Console.WriteLine($"Gold : {gold} G");
                Console.WriteLine();

                Console.WriteLine("0. 마을로 돌아가기");
                Console.WriteLine();

                Console.Write("원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        VillageMenu();
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        ShowStat();
                        break;
                }
            }           

            static void ShowInventory()
            {
                Console.WriteLine("☆인벤토리☆");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();

                foreach (var item in inventory)
                {
                    Console.WriteLine($"[{(item.Equipped ? "E" : " ")}]{item.Name} | 공격력 +{item.AttackBonus} | 방어력 +{item.DefenseBonus}");
                }
                Console.WriteLine();

                Console.WriteLine("1. 장비 관리");
                Console.WriteLine("0. 마을로 돌아가기");
                Console.WriteLine();

                Console.Write("원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageInventory();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        ShowInventory();
                        break;
                }
            }

            static void ManageInventory()
            {
                Console.WriteLine("☆장비 관리☆");
                Console.WriteLine("아이템을 선택하여 장착 또는 장착 해제할 수 있습니다.");
                Console.WriteLine();

                for (int i = 0; i < inventory.Count; i++)
                {
                    var item = inventory[i];
                    Console.WriteLine($"{i + 1} [{(item.Equipped ? "E" : " ")}] {item.Name} | 공격력 +{item.AttackBonus} | 방어력 +{item.DefenseBonus}");
                }
                Console.WriteLine();

                Console.WriteLine("0. 나가기");
                Console.WriteLine();

                Console.Write("원하시는 아이템 번호를 입력해주세요: ");
                string choice = Console.ReadLine();

                int itemIndex;
                if (int.TryParse(choice, out itemIndex))
                {
                    if (itemIndex == 0)
                    {
                        ShowInventory();                        
                    }
                    else if (itemIndex >= 1 && itemIndex <= inventory.Count)
                    {
                        int selectedIndex = itemIndex - 1;

                        inventory[selectedIndex].Equipped = !inventory[selectedIndex].Equipped;

                        Console.WriteLine(inventory[selectedIndex].Equipped
                            ? "아이템을 장착했습니다."
                            : "아이템을 장착 해제했습니다.");
                        Console.WriteLine();
                        ManageInventory();
                    }
                    else
                    {                        
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        Console.WriteLine();
                        ManageInventory();
                    }
                }             
            }

            static void VisitShop()
            {
                Console.WriteLine("☆상점☆");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G");
                Console.WriteLine();

                Console.WriteLine("[아이템 목록]");
                foreach (var item in Shopinventory)
                {                    
                    bool Owned = inventory.Any(i => i.Name == item.Name);                                        
                    string priceOrOwned = Owned ? "보유중" : $"{item.Price} G";

                    Console.WriteLine($"{item.Name} | {(item.AttackBonus > 0 ? "공격력" : "방어력")} +{(item.AttackBonus > 0 ? item.AttackBonus : item.DefenseBonus)} | {item.Description} | {priceOrOwned}");
                }
                Console.WriteLine();

                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 마을로 돌아가기");
                Console.WriteLine();

                Console.Write("원하시는 행동을 입력해주세요.");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BuyItem();
                        break;
                    case "2":
                        SellItem();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        VisitShop();
                        break;                        
                        
                       

                }
            }

            static void BuyItem()
            {
                Console.WriteLine("☆아이템 구매하기☆");
                Console.WriteLine("구매할 아이템 번호를 입력하세요");
                Console.WriteLine();

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G");
                Console.WriteLine();

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Shopinventory.Count; i++)
                {
                    var item = Shopinventory[i];
                    bool Owned = inventory.Any(invItem => invItem.Name == item.Name);
                    string priceOrOwned = Owned ? "보유중" : $"{item.Price} G";

                    Console.WriteLine($" {i + 1} {item.Name} | {(item.AttackBonus > 0 ? "공격력" : "방어력")} +{(item.AttackBonus > 0 ? item.AttackBonus : item.DefenseBonus)} | {item.Description} | {priceOrOwned}");
                }

                Console.WriteLine();

                Console.WriteLine("0. 나가기");
                Console.WriteLine();

                Console.Write("구매할 아이템 번호를 입력하세요: ");
                string choice = Console.ReadLine();

                int selectedIndex;
                if (int.TryParse(choice, out selectedIndex))
                {
                    if (selectedIndex >= 1 && selectedIndex <= Shopinventory.Count)
                    {
                        var selectedShopItem = Shopinventory[selectedIndex - 1];
                        bool Owned = inventory.Any(invItem => invItem.Name == selectedShopItem.Name);

                        if (Owned)
                        {
                            Console.WriteLine();
                            Console.WriteLine("이미 구매한 아이템입니다.");
                            Console.WriteLine();
                            BuyItem();
                        }
                        else
                        {
                            if (gold >= selectedShopItem.Price)
                            {
                                Console.WriteLine();
                                Console.WriteLine("구매를 완료했습니다.");
                                Console.WriteLine();
                                gold -= selectedShopItem.Price;                                
                                inventory.Add(new Item(selectedShopItem.Name, selectedShopItem.AttackBonus, selectedShopItem.DefenseBonus, selectedShopItem.Price));                                
                                selectedShopItem.Purchased = true;
                                BuyItem();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Gold 가 부족합니다.");
                                Console.WriteLine();
                                BuyItem();
                            }
                        }
                    }
                    else if (selectedIndex == 0)
                    {
                        VisitShop();                        
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.WriteLine();
                        BuyItem();
                    }
                }                

            }

            static void SellItem()
            {
                Console.WriteLine("☆아이템 판매하기☆");
                Console.WriteLine("판매할 아이템 번호를 입력해주세요.");
                Console.WriteLine();

                for (int i = 0; i < inventory.Count; i++)
                {
                    var item = inventory[i];
                    Console.WriteLine($"{i + 1}. {item.Name} | 판매 가격: {CalculateSellPrice(item)} G");
                }

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{gold} G");
                Console.WriteLine();

                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                Console.WriteLine("0. 나가기");
                Console.WriteLine();

                Console.Write("판매할 아이템 번호를 입력해주세요: ");
                string choice = Console.ReadLine();

                int itemIndex;

                if (int.TryParse(choice, out itemIndex))
                {
                    if (itemIndex == 0)
                    {
                        VisitShop();                        
                    }
                    else if (itemIndex >= 1 && itemIndex <= inventory.Count)
                    {                        
                        int selectedIndex = itemIndex - 1;
                        var selectedItem = inventory[selectedIndex];
                                                
                        int sellPrice = CalculateSellPrice(selectedItem);
                        Console.WriteLine($"{selectedItem.Name} 아이템을 판매하였습니다. (+{sellPrice} G)");
                        gold += sellPrice;
                                                
                        inventory.RemoveAt(selectedIndex);

                        Console.WriteLine();
                        SellItem();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        Console.WriteLine();
                        SellItem();
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine();
                    SellItem();
                }
            }

            static int CalculateSellPrice(Item item)
            {                
                return (int)(item.Price * 0.85);
            }

        }              

        private static int GetEquipAttack()
        {
            int equipAttack = 0;
            foreach (var item in inventory)
            {
                equipAttack += item.Equipped ? item.AttackBonus : 0;
            }
            return equipAttack;
        }

        private static int GetEquipDefense()
        {
            int equipDefense = 0;
            foreach (var item in inventory)
            {
                equipDefense += item.Equipped ? item.DefenseBonus : 0;
            }
            return equipDefense;
        }

    }
}