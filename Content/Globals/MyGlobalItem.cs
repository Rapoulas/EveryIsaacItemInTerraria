using Terraria;
using Terraria.ModLoader;

namespace IsaacItems.Content.Globals
{
    public class MyGlobalItem : GlobalItem
    {
        public override float UseTimeMultiplier(Item item, Player player)
        {
            float tearMultiplier = 1/player.GetModPlayer<MyPlayer>().tearStat;
            return tearMultiplier;
        }

        public override float UseAnimationMultiplier(Item item, Player player)
        {
            float tearMultiplier = 1/player.GetModPlayer<MyPlayer>().tearStat;
            return tearMultiplier;
        }

        public override void ModifyItemScale(Item item, Player player, ref float scale)
        {
            scale += player.GetModPlayer<MyPlayer>().extraRange;
        }
    }
}