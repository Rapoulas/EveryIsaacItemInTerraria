using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using IsaacItems.Content.Globals;
using IsaacItems.Content.Projectiles;
using Terraria.GameContent.ItemDropRules;

namespace IsaacItems.Content.Items.Rebirth
{
	public class Item18_Dollar : ModItem
	{ 
        public override void SetDefaults() {
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.rare = ItemRarityID.Purple;
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
			itemLoot.Add(ItemDropRule.Common(ItemID.CopperCoin, 1, 100, 100));
		}
    }
}