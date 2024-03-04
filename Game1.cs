using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Loops_and_Grids
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		KeyboardState keyboardState;

		Texture2D grassTexture;
        List<Rectangle> grassTiles;

        Texture2D mowerTexture;
		Rectangle mowerRect;
		SoundEffect mowerSound;
		SoundEffectInstance mowerSoundInstance;
		Vector2 mowerSpeed;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			_graphics.PreferredBackBufferWidth = 600;
			_graphics.PreferredBackBufferHeight = 500;
			_graphics.ApplyChanges();

			grassTiles = new List<Rectangle>();
			mowerRect = new Rectangle(100, 100, 30, 30);

			for (int i = 0; i < 600; i += 5) // generates x coordinates
				for (int j = 0; j < 500; j += 5) // generates y coordinates
					grassTiles.Add(new Rectangle(i, j, 5, 5));
			
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			grassTexture = Content.Load<Texture2D>("Images/long_grass");
			mowerTexture = Content.Load<Texture2D>("Images/mower");

			mowerSound = Content.Load<SoundEffect>("Sounds/mower_sound");
			mowerSoundInstance = mowerSound.CreateInstance();
			mowerSoundInstance.IsLooped = true;

		}

		protected override void Update(GameTime gameTime)
		{
			keyboardState = Keyboard.GetState();

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			mowerSpeed = Vector2.Zero;
			if (keyboardState.IsKeyDown(Keys.W))
				mowerSpeed.Y -= 1;
			if (keyboardState.IsKeyDown(Keys.S))
				mowerSpeed.Y += 1;
			if (keyboardState.IsKeyDown(Keys.A))
				mowerSpeed.X -= 1;
			if (keyboardState.IsKeyDown(Keys.D))
				mowerSpeed.X += 1;

			if (mowerSpeed == Vector2.Zero)
				mowerSoundInstance.Stop();
			else
				mowerSoundInstance.Play();

			mowerRect.Offset(mowerSpeed);

			for(int i = 0; i < grassTiles.Count; i++)
				if (mowerRect.Contains(grassTiles[i]))
				{
					grassTiles.Remove(grassTiles[i]);
					i--;
				}


			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.LightGreen);

			// TODO: Add your drawing code here
			_spriteBatch.Begin();

			// Draws Grass
			foreach(Rectangle grass in grassTiles)
				_spriteBatch.Draw(grassTexture, grass, Color.White);

			_spriteBatch.Draw(mowerTexture, mowerRect, Color.White);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
