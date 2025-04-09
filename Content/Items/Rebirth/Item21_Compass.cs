using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
namespace IsaacItems.Content.Items.Rebirth
{
	public class Item21_Compass : ModItem
	{ 
        public override void SetDefaults() {
			Item.consumable = true;
			Item.rare = ItemRarityID.Green;
            Item.maxStack = Item.CommonMaxStack;
            Item.SetShopValues(ItemRarityColor.Purple11, Item.buyPrice(gold: 1));
		}

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					if (!Main.Map.IsRevealed(i, j) && Main.tile[i, j].HasTile && WorldGen.InWorld(i, j) && Main.tile[i, j].TileType == TileID.Containers && Main.rand.NextBool(50)){
                        for (int x = -5; x <= 5; x++){
                            for (int y = -5; y <= 5; y++){
                                Main.Map.Update(i+x, j+y, 255);
                            }
                        }
                    }
				}
			}
			Main.refreshMap = true;
            base.RightClick(player);
        }

    }
}