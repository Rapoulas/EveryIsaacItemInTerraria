using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacItems.Content.Buffs
{
	public class OneUpCooldown : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.debuff[Type] = true;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
		}
	}
}