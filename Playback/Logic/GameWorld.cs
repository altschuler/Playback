using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Playback.Data;
using Playback.Data.Element;

namespace Playback.Logic
{
    public class GameWorld
    {
        public Level Level { get; private set; }
        
        private readonly PlacementManager PlacementManager;
        
        public GameWorld()
        {
            this.PlacementManager = new PlacementManager();
        }

        public void Update(GameTime gameTime, InputState inputState)
        {
            // update placing object
            this.PlacementManager.Update(inputState);

            // place objects
            if (inputState.LeftMouseClicked && this.PlacementManager.IsPlacingObject)
                this.PlacementManager.PlaceObjectIntoLevel(this.Level);

            this.Level.World.TestPointAll(inputState.MousePosition);

            // step in time
            if (inputState.TimeDirection == TimeDirection.Forward)
                this.Level.StepForward(gameTime);
            else if (inputState.TimeDirection == TimeDirection.Backward)
                this.Level.StepBackward(gameTime);
        }

        public void LoadLevel(Level level)
        {
            this.Level = level;
            this.Level.Initialize();

            // TODO remove this
            var go = GameObjectFactory.CreateRectangle(this.Level.World, 4f, 2f);
            this.PlacementManager.StartPlacement(go);
        }
    }

    public static class GameObjectFactory
    {
        public static GameObject CreateRectangle(World world, float width, float height)
        {
            var body = BodyFactory.CreateRectangle(world, width, height, 1f);
            var gameObject = new GameObject { Body = body};
            body.UserData = gameObject;

            return gameObject;
        }
    }
}