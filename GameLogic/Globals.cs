using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EverAI
{
    class Globals
    {
        static Everhood.Battle.BattlePlayer LocalPlayer;
        static Everhood.Battle.PlayerVerticalMovement VerticalMoveMentController;
        static Everhood.Battle.PlayerHorizontalMovement HorizontalMoveMentController;
        static HashSet<GameObject> ProjectileList;
        static HashSet<GameObject> UnjumpableProjectileList;

        public static void ResetControllers()
        {
            ProjectileList = null;
            UnjumpableProjectileList = null;
            VerticalMoveMentController = null;
            HorizontalMoveMentController = null;
        }

        public static Everhood.Battle.BattlePlayer GetLocalPlayer()
        {
            if (LocalPlayer == null)
            {
                LocalPlayer = UnityEngine.Object.FindObjectOfType<Everhood.Battle.BattlePlayer>();
            }

            return LocalPlayer;
        }
        public static HashSet<GameObject> GetProjectileList()
        {
            //Everhood.ShootProjectile.projectiles
            if (ProjectileList == null)
            {
                ProjectileList = Everhood.ShootProjectile.projectiles;
            }

            return ProjectileList;
        }

        public static HashSet<GameObject> GetUnJumpableProjectileList()
        {
            //Everhood.ShootProjectile.projectiles
            if (UnjumpableProjectileList == null)
            {
                UnjumpableProjectileList = Everhood.ShootProjectile.unjumpablesProjectiles;
            }

            return UnjumpableProjectileList;
        }

        public static bool IsUnJumpableProjectile(GameObject proj)
        {
            return UnjumpableProjectileList.Contains(proj);
        }

        public static void MoveVertical(int direction)
        {
            if (VerticalMoveMentController == null)
            {
                VerticalMoveMentController = UnityEngine.Object.FindObjectOfType<Everhood.Battle.PlayerVerticalMovement>();
            }
            
            if (VerticalMoveMentController != null)
            {
                VerticalMoveMentController.Move(direction);
            }
        }

        public static void MoveHorizontal(int direction)
        {
            if (HorizontalMoveMentController == null)
            {
                HorizontalMoveMentController = UnityEngine.Object.FindObjectOfType<Everhood.Battle.PlayerHorizontalMovement>();
            }

            if (HorizontalMoveMentController != null)
            {
                HorizontalMoveMentController.Move(direction);
            }
        }

    }
}
