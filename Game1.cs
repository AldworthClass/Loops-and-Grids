using Microsoft.Xna.Framework;
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
		Texture2D mowerTexture;
		Rectangle mowerRect;

		Vector2 mowerSpeed;

		List<Rectangle> grassTiles;

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
			_graphics.PreferredBackBufferHeight = 600;
			_graphics.ApplyChanges();

			grassTiles = new List<Rectangle>();
			mowerRect = new Rectangle(100, 100, 30, 30);

			for (int i = 0; i < 600; i += 5)
				for (int j = 0; j < 600; j += 5)
					grassTiles.Add(new Rectangle(i, j, 5, 5));
			


			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			grassTexture = Content.Load<Texture2D>("Images/long_grass");
			mowerTexture = Content.Load<Texture2D>("Images/mower");
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
