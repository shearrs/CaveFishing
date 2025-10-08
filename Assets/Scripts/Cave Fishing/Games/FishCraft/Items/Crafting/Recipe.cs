using UnityEngine;

namespace CaveFishing.Games.FishCraftGame
{
    public interface IRecipe
    {
        public ItemData Result { get; }

        public bool IsValid(ItemData[] items);
    }

    public readonly struct Recipe3x3 : IRecipe
    {
        private readonly ItemData item0, item1, item2,
                                    item3, item4, item5,
                                    item6, item7, item8;
        private readonly ItemData result;

        public ItemData Result => result;

        public Recipe3x3(ItemData item0, ItemData item1, ItemData item2,
                        ItemData item3, ItemData item4, ItemData item5,
                        ItemData item6, ItemData item7, ItemData item8, ItemData result)
        {
            this.item0 = item0;
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item7;
            this.item8 = item8;
            this.result = result;
        }

        public readonly bool IsValid(ItemData[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != ItemAt(i))
                    return false;
            }

            return true;
        }

        private readonly ItemData ItemAt(int index)
        {
            return index switch
            {
                0 => item0,
                1 => item1,
                2 => item2,
                3 => item3,
                4 => item4,
                5 => item5,
                6 => item6,
                7 => item7,
                8 => item8,
                _ => null
            };
        }
    }

    public readonly struct Recipe2x2 : IRecipe
    {
        private readonly ItemData item0, item1,
                                    item2, item3;
        private readonly ItemData result;

        public ItemData Result => result;

        public Recipe2x2(ItemData item0, ItemData item1, ItemData item2,
                        ItemData item3, ItemData result)
        {
            this.item0 = item0;
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.result = result;
        }

        public readonly bool IsValid(ItemData[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != ItemAt(i))
                    return false;
            }

            return true;
        }

        private readonly ItemData ItemAt(int index)
        {
            return index switch
            {
                0 => item0,
                1 => item1,
                2 => item2,
                3 => item3,
                _ => null
            };
        }
    }
}
