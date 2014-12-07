using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BananaFramework.GameManagers
{
	public class InputManager
	{
		public class InputBinding
		{
			Keys keyBinding;
			Buttons gamePadBinding;
			public PlayerIndex playerIndex;
			public bool isHit;
			public bool isHeld;

			public InputBinding(Keys KeyboardKey, Buttons GamePadButton, PlayerIndex GPlayerIndex)
			{
				keyBinding = KeyboardKey;
				gamePadBinding = GamePadButton;
				playerIndex = GPlayerIndex;
				isHit = false;
				isHeld = true;
			}

			public void UpdateInputBinding()
			{
				if (keyboardState.IsKeyDown(keyBinding) || gamePadStates[playerIndex].IsButtonDown(gamePadBinding))
				{
					isHit = isHeld ? false : true;
					isHeld = true;
				}
				else
				{
					isHit = false;
					isHeld = false;
				}
			}
		}

		private static KeyboardState keyboardState;
		private static Dictionary<PlayerIndex, GamePadState> gamePadStates;

		private static Dictionary<string, InputBinding> inputBindings;

		public static void Initialize()
		{
			keyboardState = Keyboard.GetState();

			gamePadStates = new Dictionary<PlayerIndex, GamePadState>();
			gamePadStates.Add(PlayerIndex.One, GamePad.GetState(PlayerIndex.One));
			gamePadStates.Add(PlayerIndex.Two, GamePad.GetState(PlayerIndex.Two));
			gamePadStates.Add(PlayerIndex.Three, GamePad.GetState(PlayerIndex.Three));
			gamePadStates.Add(PlayerIndex.Four, GamePad.GetState(PlayerIndex.Four));

			inputBindings = new Dictionary<string, InputBinding>();
		}

		public static void BindInputs(string Key, Keys KeyboardKey, Buttons GamePadButton, PlayerIndex GPlayerIndex)
		{
			inputBindings.Add(Key, new InputBinding(KeyboardKey, GamePadButton, GPlayerIndex));
		}

		public static bool IsInputHit(string Key)
		{
			return inputBindings[Key].isHit;
		}

		public static bool IsInputHeld(string Key)
		{
			return inputBindings[Key].isHeld;
		}

		public static void Update()
		{
			keyboardState = Keyboard.GetState();

			gamePadStates[PlayerIndex.One] = GamePad.GetState(PlayerIndex.One);
			gamePadStates[PlayerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
			gamePadStates[PlayerIndex.Three] = GamePad.GetState(PlayerIndex.Three);
			gamePadStates[PlayerIndex.Four] = GamePad.GetState(PlayerIndex.Four);

			foreach (InputBinding ib in inputBindings.Values)
			{
				ib.UpdateInputBinding();
			}
		}
	}
}
