using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Media;

namespace IsaacItems.Content.Globals
{
    public class MyPlayer : ModPlayer {
        #region item field 
        public Item hasSadOnion;
        public Item hasInnerEye;
        public Item hasSpoonBender;
        public Item hasCricketsHead;
        public Item hasMyReflection;
        public Item hasNumberOne;
        #endregion

        #region player stats
        public float tearStat = 1;
        public float tearStatMulti = 1;
        public int extraTearCount = 0;
        public bool homingTears = false;
        public float damageMult = 1;
        public int extraFlatDamage = 0;
        public float extraRange = 0.3f;
        public float extraRangeMult = 1;
        public float luckMult = 1;
        public float shotSpeedMult = 1;
        #endregion
        public override void ResetEffects(){
            tearStat = 1;
            tearStatMulti = 1;
            extraTearCount = 0;
            homingTears = false;
            damageMult = 1;
            extraFlatDamage = 0;
            extraRange = 0.3f;
            extraRangeMult = 1;
            luckMult = 1;
            shotSpeedMult = 1;
            
            hasSadOnion = null;
            hasInnerEye = null;
            hasSpoonBender = null;
            hasCricketsHead = null;
            hasMyReflection = null;
            hasNumberOne = null;
        }

        public override void PostUpdateEquips()
        {
            if (hasSadOnion != null){
                tearStat += 0.15f;
            }
            if (hasInnerEye != null){
                tearStat *= 0.51f;
                extraTearCount += 2;
            }
            if (hasSpoonBender != null ){
                homingTears = true;
            }
            if (hasCricketsHead != null){
                extraFlatDamage += 5;
                damageMult *= 1.5f;
            }
            if (hasMyReflection != null){
                extraFlatDamage += 5;
                luckMult *= 0.9f;
                extraRange += 0.15f;
                extraRangeMult *= 2;
                shotSpeedMult *= 1.6f;
            }
            if (hasNumberOne != null){
                extraRange -= 0.15f;
                extraRangeMult *= 0.8f;
                tearStat += 0.3f;
            }

            if (extraFlatDamage > 0 || damageMult > 1){
                Player.GetDamage(DamageClass.Generic).Base += extraFlatDamage;
                Player.GetDamage(DamageClass.Generic) *= damageMult;
            }

            extraRange *= extraRangeMult;
            tearStat *= tearStatMulti;
            HomingProj();
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (extraTearCount > 0){
                ExtraTear(source, position, velocity, type, damage, knockback);
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }

        public override void ModifyLuck(ref float luck)
        {
            luck *= luckMult;
            base.ModifyLuck(ref luck);
        }

        void ExtraTear(EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
            for (int i=1; i <= extraTearCount; i++){
                Vector2 NewVelocity;
                if (i % 2 == 1){
                    NewVelocity = velocity.RotatedBy(MathHelper.ToRadians(15 * (i/2+1)));
                }
                else {
                    NewVelocity = velocity.RotatedBy(MathHelper.ToRadians(-15 * i/2));
                }
                Projectile.NewProjectile(source, position, NewVelocity, type, damage, knockback);
            }
        }

        void HomingProj(){
            if (!homingTears){
                return;
            }
            for (int i = 0; i < Main.maxProjectiles; i++){
                if (Main.projectile[i].owner == Main.myPlayer){
                    NPC closestNPC = FindClosestNPC(128, Main.projectile[i].Center);
                    if (closestNPC != null){
                        Vector2 desiredVelocity = Main.projectile[i].DirectionTo(closestNPC.Center) * 17;
                        Main.projectile[i].velocity = Vector2.Lerp(Main.projectile[i].velocity, desiredVelocity, 0.075f);
                    }
                }
            }
        }

        private static NPC FindClosestNPC(float maxDetectDistance, Vector2 position) {
			NPC closestNPC = null;

			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			for (int k = 0; k < Main.maxNPCs; k++) {
				NPC target = Main.npc[k];
				if (target.CanBeChasedBy()) {
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, position);

					if (sqrDistanceToTarget < sqrMaxDetectDistance) {
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}
            return closestNPC;
        }
    }
}