using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using IsaacItems.Content.Familiars;
using System.Collections.Generic;
using System;
using IsaacItems.Content.Buffs;

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
        public Item hasBloodOfTheMartyr;
        public Item hasBrotherBobby;
        public Item hasSkatole;
        public Item hasHaloOfFlies;
        public Item hasOneUp;
        public Item hasMagicMushroom;
        public Item hasTheVirus;
        public Item hasRoidRage;
        public Item hasLessThan3;
        public Item hasRawLiver;
        public Item hasLunch;
        public Item hasDinner;
        public Item hasDessert;
        public Item hasBreakfast;
        public Item hasRottenMeat;
        public Item hasWoodenSpoon;
        public Item hasBelt;
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
        public int conjoinedProgress;
        public int spunProgress;
        public float speedMult = 1;
        public int extraHp = 0;
        #endregion

        public List<Projectile> Orbitals = [];
        public List<Projectile> Familiars = [];
        float orbitalRotation = 0;
        
        public override void ResetEffects(){
            tearStat = 1;
            tearStatMulti = 1;
            extraFlatDamage = 0;
            damageMult = 1;
            extraRange = 0.3f;
            extraRangeMult = 1;
            luckMult = 1;
            shotSpeedMult = 1;
            speedMult = 1;
            extraHp = 0;
            
            homingTears = false;
            extraTearCount = 0;

            conjoinedProgress = 0;
            spunProgress = 0;

            hasSadOnion = null;
            hasInnerEye = null;
            hasSpoonBender = null;
            hasCricketsHead = null;
            hasMyReflection = null;
            hasNumberOne = null;
            hasBloodOfTheMartyr = null;
            hasBrotherBobby = null;
            hasSkatole = null;
            hasHaloOfFlies = null;
            hasOneUp = null;
            hasMagicMushroom = null;
            hasTheVirus = null;
            hasRoidRage = null;
            hasLessThan3 = null;
            hasRawLiver = null;
            hasLunch = null;
            hasDinner = null;
            hasDessert = null;
            hasBreakfast = null;
            hasRottenMeat = null;
            hasWoodenSpoon = null;
            hasBelt = null;

            base.ResetEffects();
        }

        public override void PostUpdateEquips()
        {
            RebirthItems();

            if (speedMult > 2){
                speedMult = 2;
            }
            if (extraFlatDamage > 0 || damageMult > 1){
                Player.GetDamage(DamageClass.Generic).Base += extraFlatDamage;
                Player.GetDamage(DamageClass.Generic) *= damageMult;
            }

            extraRange *= extraRangeMult;
            tearStat *= tearStatMulti;
            HomingProj();
            OrbitalHandler();

            Player.statLifeMax2 += extraHp;
            Player.moveSpeed *= speedMult;
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (hasTheVirus != null){
                npc.SimpleStrikeNPC(48, npc.direction, false, 5, DamageClass.Default, true, 0, true);
                npc.AddBuff(ModContent.BuffType<TheVirusPoison>(), 300);
            }

            base.OnHitByNPC(npc, hurtInfo);
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (hasOneUp != null && !Player.HasBuff(ModContent.BuffType<OneUpCooldown>())){
                Player.AddBuff(ModContent.BuffType<OneUpCooldown>(), 10800);
                Player.Heal(Player.statLifeMax2);
                return false;
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
        }

        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (extraTearCount > 0){
                ExtraTear(source, position, velocity, type, damage, knockback);
            }
            return base.Shoot(item, source, position, velocity, type, damage, knockback);
        }

        void OrbitalHandler(){
            if (Orbitals.Count > 0){
                orbitalRotation += (float)Math.PI / 100f;
                for (int i=0; i < Orbitals.Count; i++){
                    Projectile orbital = Orbitals[i];
                    
                    Vector2 distFromPlayer = new(0f, -48f);
                    float circleRotation = orbitalRotation  + (float)Math.PI * 2 / Orbitals.Count * i;
                    Vector2 orbitalPos = distFromPlayer.RotatedBy(circleRotation);
                    orbital.position = Vector2.Lerp(orbital.position, orbitalPos + Player.position, 0.2f);
                }
            }
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

        private static NPC FindClosestNPC(float maxDetectDistance, Vector2 position){
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

        void RebirthItems(){
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
            if (hasBloodOfTheMartyr != null){
                extraFlatDamage += 10;
            }
            if (hasBrotherBobby != null){
                conjoinedProgress += 1;
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<BrotherBobbyProj>()] <= 0){
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<BrotherBobbyProj>(), 0, 0, Player.whoAmI);
                }
            }
            if (hasHaloOfFlies != null){
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<HaloOfFliesProj>()] <= 1 && Orbitals.Count < 32){
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<HaloOfFliesProj>(), 0, 0, Player.whoAmI);
                }
            }
            if (hasOneUp != null){
                if (Player.ownedProjectileCounts[ModContent.ProjectileType<OneUpProj>()] <= 0 && !Player.HasBuff(ModContent.BuffType<OneUpCooldown>())){
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<OneUpProj>(), 0, 0, Player.whoAmI);
                }
            }
            if (hasMagicMushroom != null){
                extraFlatDamage += 3;
                damageMult *= 1.5f;
                extraRange += 0.25f;
                speedMult += 0.3f;
                extraHp += 40;
            }
            if (hasTheVirus != null){
                speedMult += 0.2f;
                spunProgress += 1;
            }
            if (hasRoidRage != null){
                speedMult += 0.3f;
                extraRange += 0.25f;
                spunProgress += 1;
            }
            if (hasLessThan3 != null){
                extraHp += 40;
            }
            if (hasRawLiver != null){
                extraHp += 80;
            }
            if (hasLunch != null){
                extraHp += 40;
            }
            if (hasDinner != null){
                extraHp += 40;
            }
            if (hasDessert != null){
                extraHp += 40;
            }
            if (hasBreakfast != null){
                extraHp += 40;
            }
            if (hasRottenMeat != null){
                extraHp += 40;
            }
            if (hasWoodenSpoon != null){
                speedMult += 0.3f;
            }
            if (hasBelt != null){
                speedMult += 0.3f;
            }
        }
    }
}