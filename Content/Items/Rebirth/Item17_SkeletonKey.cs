using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using IsaacItems.Content.Globals;
using IsaacItems.Content.Projectiles;
using Terraria.GameContent.ItemDropRules;

namespace IsaacItems.Content.Items.Rebirth
{
	public class Item17_SkeletonKey : ModItem
	{ 
        public override void SetDefaults() {
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.rare = ItemRarityID.Blue;
            Item.SetShopValues(ItemRarityColor.Purple11, Item.buyPrice(gold: 1));
		}

        public override bool CanRightClick()
        {
            return true;
        }

		public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.GoodieBags;
        }


        public override void ModifyItemLoot(ItemLoot itemLoot) {
			int[] keys = [
				ItemID.GoldenKey,
				ItemID.ShadowKey,
				ItemID.TempleKey,
				ItemID.JungleKey,
				ItemID.FrozenKey,
				ItemID.HallowedKey,
				ItemID.CorruptionKey,
				ItemID.CrimsonKey,
				ItemID.DungeonDesertKey,
				ItemID.LightKey,
				ItemID.NightKey
			];
			for (int i = 0; i < 99; i++){
				itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, keys));
			}
		}
    }
}