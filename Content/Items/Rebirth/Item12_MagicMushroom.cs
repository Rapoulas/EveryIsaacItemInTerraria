using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using IsaacItems.Content.Globals;
using IsaacItems.Content.Projectiles;

namespace IsaacItems.Content.Items.Rebirth
{
	public class Item12_MagicMushroom : ModItem
	{ 
        public override void SetDefaults() {
			ItemID.Sets.AnimatesAsSoul[Item.type] = true; 
			Item.DefaultToAccessory(26, 34);
			Item.SetShopValues(ItemRarityColor.Purple11, Item.buyPrice(gold: 1));
			Item.rare = ItemRarityID.Yellow;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<MyPlayer>().hasMagicMushroom = Item;
		}
    }
}