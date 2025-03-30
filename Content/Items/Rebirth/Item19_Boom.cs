using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using IsaacItems.Content.Globals;
using IsaacItems.Content.Projectiles;
using Terraria.GameContent.ItemDropRules;

namespace IsaacItems.Content.Items.Rebirth
{
	public class Item19_Boom : ModItem
	{ 
        public override void SetDefaults() {
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.rare = ItemRarityID.Green;
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
			int[] bombs = [
				ItemID.Bomb,
				ItemID.BombFish,
				ItemID.BouncyBomb,
				ItemID.DryBomb,
				ItemID.WetBomb,
				ItemID.LavaBomb,
				ItemID.HoneyBomb,
				ItemID.DirtBomb,
				ItemID.ScarabBomb,
				ItemID.DirtStickyBomb,
				ItemID.StickyBomb,
			];
			for (int i = 0; i < 10; i++){
				itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, bombs));
			}
		}
    }
}