using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.DataStructures;

namespace IsaacItems.Content.Items.Rebirth
{
	public class Item20_Transcendence : ModItem
	{ 
        public override void SetDefaults() {
			      Item.rare = ItemRarityID.Blue;
            Item.SetShopValues(ItemRarityColor.Purple11, Item.buyPrice(gold: 1));
            Item.CloneDefaults(ItemID.AngelWings);
		    }

    }
}