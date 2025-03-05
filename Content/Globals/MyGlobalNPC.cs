using System.Linq;
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
    }
}