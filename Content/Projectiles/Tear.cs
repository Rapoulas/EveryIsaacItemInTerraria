using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace IsaacItems.Content.Projectiles
{
	public class Tear : ModProjectile
	{
        public sealed override void SetDefaults() {
			Projectile.width = 13;
			Projectile.height = 13;
			Projectile.tileCollide = true;
			Projectile.penetrate = 1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.alpha = 255;
		}

        public override void AI()
        {
            Projectile.alpha -= 20;
            Projectile.ai[0]++;

            if (Projectile.ai[0] > 45){
                Projectile.velocity.Y += 0.15f;
                Projectile.velocity.X *= 0.98f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.netUpdate = true;
            for (int i = 0; i < 1; i++) {
                Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            }

            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnKill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<TearSplash>(), 0, 0);
        }
    }

    public class TearSplash : ModProjectile
    {
        public override void SetStaticDefaults() {
			Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 6;
		}
        public sealed override void SetDefaults() {
			Projectile.width = 64;
			Projectile.height = 64;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
            Projectile.timeLeft = 14;
            Projectile.damage = 0;
		}

        public override void AI()
        {
            Projectile.ai[0]++;
            
            if (Projectile.ai[0] % 2 == 0){
                Projectile.frame++;
            }
        }
    }
}