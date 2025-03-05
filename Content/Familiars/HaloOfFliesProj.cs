using IsaacItems.Content.Globals;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace IsaacItems.Content.Familiars
{
	public class HaloOfFliesProj : ModProjectile
	{
        
        public override void SetStaticDefaults() {
			Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 2;
		}
        public sealed override void SetDefaults()
        {
            Projectile.width = 32;
			Projectile.height = 32;
			Projectile.tileCollide = false;
			Projectile.minion = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
            Projectile.scale = 1.25f;
		}

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner)) {
				return;
			}

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 3) {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                    Projectile.frame = 0;
            }

            foreach(Projectile proj in Main.projectile){
                if (proj.hostile && proj.Hitbox.Distance(Projectile.Center) < 5){
                    proj.Kill();
                }
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player owner = Main.player[Projectile.owner];
            
            owner.GetModPlayer<MyPlayer>().Orbitals.Add(Projectile);
            if (owner.GetModPlayer<MyPlayer>().Orbitals.Count > 32){
                owner.GetModPlayer<MyPlayer>().Orbitals[0].Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            Player owner = Main.player[Projectile.owner];
            owner.GetModPlayer<MyPlayer>().Orbitals.Remove(Projectile);
        }

        private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active || owner.GetModPlayer<MyPlayer>().hasHaloOfFlies == null) {
                Projectile.Kill();
				return false;
			}
            Projectile.timeLeft = 2;
			return true;
		}
    }
}