using Terraria;
using Terraria.ModLoader;
using IsaacItems.Content.Globals;
using Microsoft.Xna.Framework;
using System;
using IsaacItems.Content.Projectiles;

namespace IsaacItems.Content.Familiars
{
	public class BrotherBobbyProj : ModProjectile
	{
		Vector2 tearVelocity = Vector2.Zero;
		bool shootIsOnCooldown = false;
        public override void SetStaticDefaults() {
			Main.projPet[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 8;
		}
        public sealed override void SetDefaults() {
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.tileCollide = false;
			Projectile.minion = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.scale = 1.5f;
			Projectile.friendly = true;
		}

        public override bool? CanCutTiles() {
			return false;
		}

        public override bool? CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner)) {
				return;
			}
			
			Movement(owner);
			HandleDirection();
			HandleShooting(owner);
        }

		public void HandleShooting(Player owner){
			if (owner.controlUseItem && !shootIsOnCooldown){
				shootIsOnCooldown = true;
				Projectile.NewProjectile(owner.GetSource_FromThis(), Projectile.Center.X-5, Projectile.Center.Y-16, tearVelocity.X, tearVelocity.Y, ModContent.ProjectileType<Tear>(), 5, 1);
				Projectile.ai[0] = 0;
			}

			if (shootIsOnCooldown){
				if (Projectile.ai[0] >= 45){
					shootIsOnCooldown = false;
				}
				Projectile.ai[0]++;
			}
		}

		public void Movement(Player owner){
			if (Projectile.Center.Distance(owner.Center) > 25){
				Projectile.Center = Vector2.Lerp(Projectile.Center, owner.Center, 0.05f);
			}

			Projectile.ai[0]++;
			Projectile.position.Y += (float)(0.5 * Math.Sin(Projectile.ai[0]/20));
		}

        private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active || owner.GetModPlayer<MyPlayer>().hasBrotherBobby == null) {
                Projectile.Kill();
				return false;
			}
            Projectile.timeLeft = 2;
			return true;
		}

        private void HandleDirection(){
			Player owner = Main.player[Projectile.owner];
			Vector2 playerToMouse = owner.Center.DirectionTo(Main.MouseWorld);
			playerToMouse.Normalize();
			float rotation = playerToMouse.ToRotation();

			if (rotation >= -1 * MathHelper.PiOver4 && rotation < MathHelper.PiOver4){ //Right
				Projectile.frame = 2;
				tearVelocity.X = 10;
				tearVelocity.Y = 0;
				if (Projectile.ai[0] > 0 && Projectile.ai[0] < 35){
					Projectile.frame = 3;
				}
			}
			else if (rotation >= -3 * MathHelper.PiOver4 && rotation < -1 * MathHelper.PiOver4){ //Down
				Projectile.frame = 6;
				tearVelocity.X = 0;
				tearVelocity.Y = -10;
				if (Projectile.ai[0] > 0 && Projectile.ai[0] < 35){
					Projectile.frame = 7;
				}
			}
			else if (rotation >= 3 * MathHelper.PiOver4 || rotation < -3 * MathHelper.PiOver4){ //Left
				Projectile.frame = 4;
				tearVelocity.X = -10;
				tearVelocity.Y = 0;
				if (Projectile.ai[0] > 0 && Projectile.ai[0] < 35){
					Projectile.frame = 5;
				}
			}
			else if (rotation >= MathHelper.PiOver4 && rotation < 3 * MathHelper.PiOver4){ //Up
				Projectile.frame = 0;
				tearVelocity.X = 0;
				tearVelocity.Y = 10;
				if (Projectile.ai[0] > 0 && Projectile.ai[0] < 35){
					Projectile.frame = 1;
				}
			}
		}
    }
}