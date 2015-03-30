using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BananaFramework.Managers
{
	/// <summary>
	/// The InputManager class aggregates, updates, and provides helper methods for various forms 
	/// of input for game control.
	/// </summary>
	public class InputManager
	{
		/// <summary>
		/// The InputBinding class represents a grouping of different input methods -- such as 
		/// keyboard and gamepad -- as a single input.
		/// </summary>
		public class InputBinding
		{
			Keys keyBinding;
			Buttons gamePadBinding;
			public PlayerIndex playerIndex;
			public bool isHit;
			public bool isHeld;

			/// <summary>
			/// Constructs a new InputBinding with the given key, button, and player index.
			/// </summary>
			/// <param name="KeyboardKey">The keyboard key to assign to this binding.</param>
			/// <param name="GamePadButton">The gamepad button to assign to this binding.</param>
			/// <param name="GPlayerIndex">The index of the gamepad to use for this binding.</param>
			public InputBinding(Keys KeyboardKey, Buttons GamePadButton, PlayerIndex GPlayerIndex)
			{
				keyBinding = KeyboardKey;
				gamePadBinding = GamePadButton;
				playerIndex = GPlayerIndex;
				isHit = false;
				isHeld = true;
			}

			/// <summary>
			/// Updates the state of the InputBinding to determine if it's being held or has been hit.
			/// </summary>
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

		/// <summary>
		/// Initializes the InputManager to prepare it to accept new InputBindings and check input.
		/// </summary>
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

		/// <summary>
		/// Creates a new InputBinding and associates it with a given string key.
		/// </summary>
		/// <param name="Key">The key with which this new binding will be associated with.</param>
		/// <param name="KeyboardKey">The keyboard key to assign to the new InputBinding.</param>
		/// <param name="GamePadButton">The gamepad button to assign to the new InputBinding.</param>
		/// <param name="GPlayerIndex">The index of the player to use for the gamepad button binding.</param>
		public static void BindInputs(string Key, Keys KeyboardKey, Buttons GamePadButton, PlayerIndex GPlayerIndex)
		{
			// Ensure that these bindings haven't been added already
			if (!inputBindings.ContainsKey(Key))
			{
				inputBindings.Add(Key, new InputBinding(KeyboardKey, GamePadButton, GPlayerIndex));
			}
			else
			{
				inputBindings[Key] = new InputBinding(KeyboardKey, GamePadButton, GPlayerIndex);
			}
		}

		/// <summary>
		/// Returns true on the first frame that the associated input has been pressed, otherwise false.
		/// </summary>
		/// <param name="Key">The key of the associated InputBinding to check.</param>
		/// <returns></returns>
		public static bool IsInputHit(string Key)
		{
			return inputBindings[Key].isHit;
		}

		/// <summary>
		/// Returns true if the associated input is being held.
		/// </summary>
		/// <param name="Key">The key of the associated InputBinding to check.</param>
		/// <returns></returns>
		public static bool IsInputHeld(string Key)
		{
			return inputBindings[Key].isHeld;
		}

		/// <summary>
		/// Updates the InputManager with the current states of the various input devices.
		/// </summary>
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
