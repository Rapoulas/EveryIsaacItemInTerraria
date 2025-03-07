using Terraria;
using Terraria.ModLoader;
using IsaacItems.Content.Globals;
using Microsoft.Xna.Framework;
using System;
using IsaacItems.Content.Projectiles;
using Terraria.DataStructures;
using IsaacItems.Content.Buffs;
using Terraria.Audio;

namespace IsaacItems.Content.Familiars
{
	public class OneUpProj : ModProjectile
	{
        public override void SetStaticDefaults() {
			Main.projPet[Projectile.type] = true;
		}
        public sealed override void SetDefaults() {
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.tileCollide = false;
			Projectile.minion = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.penetrate = -1;
			Projectile.scale = 1.25f;
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
        }

        public override void OnSpawn(IEntitySource source)
        {
            Player owner = Main.player[Projectile.owner];
            
            owner.GetModPlayer<MyPlayer>().Familiars.Add(Projectile);
        }

        public override void OnKill(int timeLeft)
        {
            Player owner = Main.player[Projectile.owner];
            owner.GetModPlayer<MyPlayer>().Familiars.Remove(Projectile);
			SoundStyle OneUp = new($"{nameof(IsaacItems)}/Content/Familiars/OneUpSoundEffect");
            SoundEngine.PlaySound(OneUp, owner.Center);
        }

		public void Movement(Player owner){
			int projIndex = owner.GetModPlayer<MyPlayer>().Familiars.IndexOf(Projectile);
			foreach (Projectile proj in Main.projectile){
				if (projIndex == 0){
					break;
				}

				if (owner.GetModPlayer<MyPlayer>().Familiars.Contains(proj) && proj != Projectile){
					int nextInLineProjIndex = owner.GetModPlayer<MyPlayer>().Familiars.IndexOf(proj);

					if (owner.GetModPlayer<MyPlayer>().Familiars[nextInLineProjIndex] == owner.GetModPlayer<MyPlayer>().Familiars[projIndex-1]){
						if (Projectile.Center.Distance(proj.Center) > 25){
							Projectile.Center = Vector2.Lerp(Projectile.Center, proj.Center, 0.05f);
						}
					}
				}
			}
			if (projIndex == 0 && Projectile.Center.Distance(owner.Center) > 25){
				Projectile.Center = Vector2.Lerp(Projectile.Center, owner.Center, 0.05f);
			}

			Projectile.ai[0]++;
			Projectile.position.Y += (float)(0.5 * Math.Sin(Projectile.ai[0]/20));
		}

        private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active || owner.GetModPlayer<MyPlayer>().hasOneUp == null || owner.HasBuff(ModContent.BuffType<OneUpCooldown>())) {
                Projectile.Kill();
				return false;
			}
            Projectile.timeLeft = 2;
			return true;
		}
    }
}