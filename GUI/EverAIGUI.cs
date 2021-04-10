using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EverAI
{
    class EverAIGUI
    {
        static public GUIStyle boldtext = new GUIStyle(UnityEngine.GUI.skin.label);
        static public Texture2D GreenTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        static public Texture2D RedTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        static public bool init = false;
        static public Camera mainCam;

        public static void InitializeGUIVars()
        {
            if (!init)
            {
                mainCam = Camera.main;

                boldtext.fontStyle = FontStyle.Bold;
                boldtext.fontSize = 20;

                GreenTexture.SetPixel(0, 0, Color.green);
                GreenTexture.SetPixel(1, 0, Color.green);
                GreenTexture.SetPixel(0, 1, Color.green);
                GreenTexture.SetPixel(1, 1, Color.green);
                GreenTexture.Apply();

                RedTexture.SetPixel(0, 0, Color.red);
                RedTexture.SetPixel(1, 0, Color.red);
                RedTexture.SetPixel(0, 1, Color.red);
                RedTexture.SetPixel(1, 1, Color.red);
                RedTexture.Apply();

                init = true;
            }
        }

        public static void DrawVisuals()
        {
            UIHelper.DrawOutline(new UnityEngine.Rect(580, 10, 200, 40), "Ever AI V0.2", boldtext);

            Everhood.Battle.BattlePlayer pLocal = Globals.GetLocalPlayer();
            if (pLocal == null)
                return;

            // Debug info in the top left
            // Player position and cooridor
            UIHelper.DrawOutline(new UnityEngine.Rect(180, 20, 400, 90), "Player Pos: " + pLocal.transform.position.x + ", " + pLocal.transform.position.y + ", " + pLocal.transform.position.z, EverAIGUI.boldtext);
            UIHelper.DrawOutline(new UnityEngine.Rect(180, 40, 400, 90), "Player Corridor: " + pLocal.currentCorridor, EverAIGUI.boldtext);

            // Player input on horizonal and vertical axis
            Rewired.Player pLocalInput = Rewired.ReInput.players.GetPlayer(0);
            string vertaxis = "Vertical Axis Input: " + pLocalInput.GetAxis("VerticalBattle");
            string horiaxis = "Horizontal Axis Input: " + pLocalInput.GetAxis("HorizontalBattle");
            UIHelper.DrawOutline(new UnityEngine.Rect(180, 80, 400, 90), vertaxis + "\n" + horiaxis, EverAIGUI.boldtext);

            //this.battlePlayer.transform.localPosition + this.offsets[this.battlePlayer.currentCorridor] -> True screen position.

            // Corridors, whethey they're blocked and etc
            string BlockedCorridors = "";
            for (int i = 0; i < 5; i++)
            {
                BlockedCorridors += Utility.IsCorridorOpen(i) ? "[X] " : "[ ] ";
            }
            UIHelper.DrawOutline(new UnityEngine.Rect(180, 60, 400, 90), "Corridors: " + BlockedCorridors, EverAIGUI.boldtext);

            // Debug ESP for projectiles
           // if (Everhood.ShootProjectile.projectiles != null)
            //{
                //foreach (GameObject proj in Everhood.ShootProjectile.projectiles)
                //{
                   // if (proj.activeSelf)
                    //{
                    //    UIHelper.Box(proj.transform.position.x, proj.transform.position.y, 30, 30);
                    //}
                //}
            //}
            
            //UIHelper.DrawOutline(new UnityEngine.Rect(580, 30, 200, 40), Features.KillPlayersInstantly ? "Kill Players: ON" : "Kill Players: OFF", boldtext);
            //UIHelper.DrawOutline(new UnityEngine.Rect(580, 50, 200, 40), Features.Speedhack ? "Speedhack: ON" : "Speedhack: OFF", boldtext);
            //UIHelper.DrawOutline(new UnityEngine.Rect(580, 70, 200, 40), Features.KillMonstersAtAll ? "Kill Monsters: ON" : "Kill Monsters: OFF", boldtext);

            //if (Utility.GetPlayerCount() < 1)
            //return;

            //Player pLocal = Player.m_localPlayer;
            //if (pLocal == null)
            //return;

            //Debug.DrawLine(GameCamera.instance.transform.position, GameCamera.instance.transform.forward * 100f);

            //string PlayerCount = "Players Near Me: " + Utility.GetPlayerCount().ToString();
            //UIHelper.DrawOutline(new UnityEngine.Rect(580, 90, 500, 40), PlayerCount, boldtext);
        }
    }
}
