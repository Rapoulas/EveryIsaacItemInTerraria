using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace IsaacItems.Content.Globals
{
    public class MyGlobalProj : GlobalProjectile
    {
        Player player = Main.LocalPlayer;
        Vector2[] startVelocity = new Vector2[Main.maxProjectiles];
        public override bool InstancePerEntity => true;
        
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.owner == Main.myPlayer){
                if (player.GetModPlayer<MyPlayer>().shotSpeedMult > 1){
                    projectile.velocity *= player.GetModPlayer<MyPlayer>().shotSpeedMult;
                }
                if (player.GetModPlayer<MyPlayer>().hasMyReflection != null){
                    startVelocity[projectile.whoAmI] = projectile.velocity;
                }
                
                int timeLeftMult = 1 + (int)(player.GetModPlayer<MyPlayer>().extraRange - 0.3f);
                if (timeLeftMult < 0){
                    projectile.timeLeft /= timeLeftMult * -1;
                }
                if (timeLeftMult > 0){
                    projectile.timeLeft *= timeLeftMult;
                }
            }
        }

        public override void AI(Projectile projectile)
        {
            if (projectile.owner == Main.myPlayer){
                projectile.velocity = Vector2.Lerp(projectile.velocity, startVelocity[projectile.whoAmI] * -1, 0.005f);
            }
        }
    }
}