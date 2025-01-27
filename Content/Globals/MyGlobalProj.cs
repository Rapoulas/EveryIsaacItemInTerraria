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
            if (player.GetModPlayer<MyPlayer>().shotSpeedMult > 1){
                projectile.velocity *= player.GetModPlayer<MyPlayer>().shotSpeedMult;
            }
            if (player.GetModPlayer<MyPlayer>().hasMyReflection != null && projectile.owner == Main.myPlayer){
                startVelocity[projectile.whoAmI] = projectile.velocity;
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