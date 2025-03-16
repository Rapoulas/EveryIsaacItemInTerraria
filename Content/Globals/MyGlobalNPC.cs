using System.Linq;
using IsaacItems.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacItems.Content.Globals
{
    public class Item5_MyReflection : GlobalNPC
    {
        Player player = Main.LocalPlayer;
        public override bool InstancePerEntity => true;
        int[] batIDs = [NPCID.CaveBat, NPCID.SporeBat, NPCID.JungleBat, NPCID.Hellbat, NPCID.IceBat, NPCID.GiantBat, NPCID.IlluminantBat, NPCID.Lavabat, NPCID.Slimer, NPCID.GiantFlyingFox, NPCID.Vampire];

        public override void AI(NPC npc)
        {
            if (batIDs.Contains(npc.type) && player.GetModPlayer<MyPlayer>().hasSkatole != null){
                npc.friendly = true;

                foreach (NPC nonBatNPC in Main.npc){
                    if (batIDs.Contains(nonBatNPC.type)){
                        continue;
                    }
                    if (nonBatNPC.Hitbox.Intersects(npc.Hitbox) && !nonBatNPC.friendly){
                        nonBatNPC.SimpleStrikeNPC(npc.damage, npc.direction, false, 5, DamageClass.Default, true, 0, true);
                    }
                }
            }
            else if (batIDs.Contains(npc.type) && player.GetModPlayer<MyPlayer>().hasSkatole == null){
                npc.friendly = false;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<TheVirusPoison>())) {
                int DPS = (int)(0.4 * (player.statLifeMax2/20) * (player.statLifeMax2/20) + 2);
				if (npc.lifeRegen > 0)
					npc.lifeRegen = 0;
				npc.lifeRegen -= DPS;

                if (damage < DPS/4)
                    damage = DPS/4;
                }

            base.UpdateLifeRegen(npc, ref damage);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<TheVirusPoison>())) {
                drawColor.R = (byte)(drawColor.R * 0.65f );
                drawColor.B = (byte)(drawColor.B * 0.75f );
			}

            base.DrawEffects(npc, ref drawColor);
        }
    }
}