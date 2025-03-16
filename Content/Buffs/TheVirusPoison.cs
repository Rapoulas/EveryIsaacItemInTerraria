using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacItems.Content.Buffs
{
	public class TheVirusPoison : ModBuff
	{
        public override string Texture => $"Terraria/Images/Buff_{BuffID.Poisoned}";
		public override void SetStaticDefaults() {
			Main.debuff[Type] = true;
		}
	}
}