using FarseerPhysics.Dynamics;
using Playback.Data;
using Playback.Data.Element;
using Playback.Utils;

namespace Playback.Logic
{
    public class PlacementManager
    {
        public bool IsPlacingObject { get; private set; }
        public GameObject CurrentGameObject { get; set; }

        public void StartPlacement(GameObject gameObject)
        {
            this.CurrentGameObject = gameObject;
            this.CurrentGameObject.Body.IsSensor = true;

            this.IsPlacingObject = true;
        }

        public void Update(InputState mouseState)
        {
            if (!this.IsPlacingObject) 
                return;

            this.CurrentGameObject.Body.Position = ConvertUnits.ToSimUnits(mouseState.MousePosition);
        }

        public void PlaceObjectIntoLevel(Level level)
        {
            // TODO "prepare object for placement" setting bodytype, sensor etc
            this.CurrentGameObject.Body.IsSensor = false;
            this.CurrentGameObject.Body.BodyType = BodyType.Static;
            this.CurrentGameObject.Type = GameObjectType.Placed;
            
            level.PlaceGameObject(this.CurrentGameObject);

            this.CurrentGameObject = null;
            this.IsPlacingObject = false;
        }
    }
}