using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RogueWorld.Utilities;
using RogueWorld.GUI;

namespace RogueWorld.Managers 
{
    internal class GUIManager 
    {

        public GUI_Stats statsGUI;
        public GUI_Equipment equipmentGUI;
        public GUI_ActiveItem activeItemGUI1;
        public GUI_ActiveItem activeItemGUI2;
        public Unit_Rogue Player { get; set; }

        /// <summary>
        /// Constructor for the GUI Manager.
        /// </summary>
        public GUIManager(Unit_Rogue player)
        {

            Player = player;

            statsGUI = new GUI_Stats(GameManager.COLS, 0, 
                18, 10,
                'x', ' ',
                ConsoleColor.Blue, ConsoleColor.Cyan);

            equipmentGUI = new GUI_Equipment(GameManager.COLS, 11,
                18, 10,
                'x', ' ',
                ConsoleColor.Blue, ConsoleColor.Cyan);

            activeItemGUI1 = new GUI_ActiveItem(GameManager.COLS + 1, 22,
                8, 8,
                ' ', 'o',
                ConsoleColor.Black, ConsoleColor.DarkYellow);

            activeItemGUI2 = new GUI_ActiveItem(GameManager.COLS + 9, 22,
                8, 8,
                ' ', 'o',
                ConsoleColor.Black, ConsoleColor.DarkYellow);

        }

        #region MainMenu

        private int _currentButton = -1;
        readonly int ButtonMax = 2;
        readonly int xOffset = 10;

        public List<MenuButton> buttons;

        internal void Update()
        {
            statsGUI.DrawGUI();
            equipmentGUI.DrawGUI();
            activeItemGUI1.DrawGUI();
            activeItemGUI2.DrawGUI();
        }

        public struct MenuButton
        {
            public int Id;
            public string Label;
            public int yOffset;
        }
        public void InitMenu()
        {
            buttons = new List<MenuButton>();
        }
        public void DrawMenu()
        {
            Console.Clear();
            DrawLogo();
            DrawButtons(0, "PLAY");
            DrawButtons(1, "TROPHIES");
            DrawButtons(2, "CREDITS");
            if (_currentButton == -1) { _currentButton = 0; }
            DrawButtonSelector();

        }
        private void DrawLogo()
        {

            Util.Write("ROGUE WORLD",
                GameManager.COLS / 2 - xOffset, 5,
                ConsoleColor.DarkRed, ConsoleColor.Black);
        }

        private void CreateButton(int buttonId, string buttonText, int buttonYOffset = 0)
        {
            MenuButton newButton = new MenuButton();
            newButton.Id = buttonId;
            newButton.Label = buttonText;
            newButton.yOffset = buttonYOffset;
            buttons.Add(newButton);

        }

        private void DrawButtons(int buttonId, string buttonText, int buttonYOffset = 0)
        {

            if (buttonId == _currentButton)
            {
                Util.Write(buttonText,
                      GameManager.COLS / 2 - xOffset, GameManager.ROWS - 10 + buttonId + buttonYOffset,
                      ConsoleColor.Black, ConsoleColor.White);

            }
            else
            {
                Util.Write(buttonText,
                    GameManager.COLS / 2 - xOffset, GameManager.ROWS - 10 + buttonId + buttonYOffset,
                    ConsoleColor.DarkGray, ConsoleColor.Black);
            }
        }

        private void DrawButtonSelector()
        {
            Util.Write(">",
                GameManager.COLS / 2 - xOffset - 2, GameManager.ROWS - 10 + _currentButton,
                ConsoleColor.Red, ConsoleColor.Black);
        }

        public void IncrementButtonSelection()
        {
            if (_currentButton < ButtonMax)
            {
                _currentButton++;
            }
            else
            {
                _currentButton = 0;
            }
        }

        public void DecrementButtonSelection()
        {
            if (_currentButton > 0)
            {
                _currentButton--;
            }
            else
            {
                _currentButton = ButtonMax;
            }
        }

        public void ActivateButtonSelection()
        {
            if (_currentButton == 0)
            {
                Program.GameState = GameState.Continue;
                _currentButton = -1;
            }
        }

        #endregion

        #region Log System

        public void PrintLog(string logContent, ConsoleColor FgColor, ConsoleColor BgColog)
        {
            ClearLog();
            Util.Write("Log: " + logContent,
                0, GameManager.ROWS - 1,
                FgColor, BgColog);
        }

        public void ClearLog()
        {
            for (var i = 0; i < GameManager.COLS; i++)
            {
                Util.Write(" ",
               i, GameManager.ROWS - 1,
               ConsoleColor.White, ConsoleColor.Black);
            }
        }

        #endregion

    }

}
