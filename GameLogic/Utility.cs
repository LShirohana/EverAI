using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EverAI
{
    class Utility
    {
        static bool[] OpenCorridor;
        static bool[] CooridorUnJumpable;

        enum Corridor
        {
            LEFT = 0,
            MID_LEFT = 1,
            CENTER = 2,
            MID_RIGHT = 3,
            RIGHT = 4,
            INVALID = -1
        }

        public static void InitiateVariables()
        {
            OpenCorridor = new bool[5];
            CooridorUnJumpable = new bool[5];
        }
        public static void ResetCorridors()
        {
            for (int i = 0; i < OpenCorridor.Length; i++)
            {
                OpenCorridor[i] = true;
                CooridorUnJumpable[i] = false;
            }
        }

        public static int GetCorridorFromPosition(Vector3 pos)
        {
            // Corridor is unusable.
            // Corridor X positions FOR PROJECTILES
            //-4.93, -2.36, 0, 2.36, 4.93

            if (pos.x > 3)
            {
                return (int)Corridor.RIGHT;
            }
            else if (pos.x > 1 && pos.x < 2.8)
            {
                return (int)Corridor.MID_RIGHT;
            }
            else if (pos.x > -0.1 && pos.x < 0.1)
            {
                return (int)Corridor.CENTER;
            }
            else if (pos.x < -1.0 && pos.x > -2.9)
            {
                return (int)Corridor.MID_LEFT;
            }
            else if (pos.x < -3)
            {
                return (int)Corridor.LEFT;
            }

            return -1;
        }

        public static void UpdateCorridors()
        {
            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return;

            HashSet<GameObject> ProjectileList = Globals.GetProjectileList();
            if (ProjectileList == null)
                return;

            foreach (GameObject proj in ProjectileList)
            {
                if (proj.activeSelf)
                {
                    if (proj.transform.position.z < -1.0)
                    {
                        int ProjectileCorridor = GetCorridorFromPosition(proj.transform.position);
                        if (ProjectileCorridor != -1) // I actually got a value.
                        {
                            OpenCorridor[ProjectileCorridor] = false;
                            if (Everhood.ShootProjectile.unjumpablesProjectiles.Contains(proj)) // can i jump it?
                                CooridorUnJumpable[ProjectileCorridor] = true;
                        }
                        else
                            UnityEngine.Debug.LogError("WHY IS CORRIDOR -1?");
                    }
                }
            }
        }

        public static bool IsCorridorOpen(int corridor)
        {
            return OpenCorridor[corridor];
        }

        public static bool ShouldJump()
        {
            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return false;

            HashSet<GameObject> ProjectileList = Globals.GetProjectileList();
            if (ProjectileList == null)
                return false;

            foreach (GameObject proj in ProjectileList)
            {
                if (proj.activeSelf)
                {
                    if (!Everhood.ShootProjectile.unjumpablesProjectiles.Contains(proj))
                    {
                        if (Vector3.Distance(proj.transform.position, pLocal.transform.position) < 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool CanJumpCorridor(int corridor)
        {
            return !CooridorUnJumpable[corridor];
        }

        public static bool CanMoveRight()
        {
            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return false;

            if (pLocal.currentCorridor == (int)Corridor.RIGHT)
                return false;
            else
                return OpenCorridor[pLocal.currentCorridor + 1];
        }
        public static bool CanMoveLeft()
        {
            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return false;

            if (pLocal.currentCorridor == (int)Corridor.LEFT)
                return false;
            else
                return OpenCorridor[pLocal.currentCorridor - 1];
        }

        public static bool IsPathOpen(int targetCorridor)
        {
            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return false;

            int curLocalCorridor = pLocal.currentCorridor;
            if (curLocalCorridor > targetCorridor)
            {
                while (curLocalCorridor > targetCorridor)
                {
                    if (!OpenCorridor[targetCorridor])
                    {
                        return false;
                    }

                    ++targetCorridor;
                }
            }
            else if (curLocalCorridor < targetCorridor)
            {
                while (curLocalCorridor < targetCorridor)
                {
                    if (!OpenCorridor[curLocalCorridor])
                    {
                        return false;
                    }

                    ++curLocalCorridor;
                }
            }

            return true;
        }

        public static bool CanMakeProgress(int targetCorridor)
        {
            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return false;

            int curLocalCorridor = pLocal.currentCorridor;

            // If we are moving left, check if we can move left once.
            if (curLocalCorridor > targetCorridor)
            {
                curLocalCorridor -= 1;
                if (OpenCorridor[curLocalCorridor])
                    return true;
                else 
                    return false;
            }// If we are moving right, check if we can move right once.
            else if (curLocalCorridor < targetCorridor)
            {
                curLocalCorridor += 1;
                if (OpenCorridor[curLocalCorridor])
                    return true;
                else
                    return false;
            }

            return false; // This will only ever be hit if target and current cooridor are the same.
        }
        // This function is for checking if Projectiles are in the same cooridor as the local player.
        public static bool IsInSameCorridor(Vector3 pos)
        {
            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return false;

            int curLocalCorridor = pLocal.currentCorridor;

            // Corridor is unusable.
            // Corridor X positions FOR PROJECTILES
            //-4.93, -2.36, 0, 2.36, 4.93

            if (pos.x > 3)
            {
                if (curLocalCorridor == (int)Corridor.RIGHT)
                    return true;
            }
            else if (pos.x > 1 && pos.x < 2.8)
            {
                if (curLocalCorridor == (int)Corridor.MID_RIGHT)
                    return true;
            }
            else if (pos.x > -0.1 && pos.x < 0.1)
            {
                if (curLocalCorridor == (int)Corridor.CENTER)
                    return true;
            }
            else if (pos.x < -1.0 && pos.x > -2.9)
            {
                if (curLocalCorridor == (int)Corridor.MID_LEFT)
                    return true;
            }
            else if (pos.x < -3)
            {
                if (curLocalCorridor == (int)Corridor.LEFT)
                    return true;
            }

            return false;
        }

    }
}
