//using FarseerPhysics.Factories;
//using Playback.Logic;

//namespace Playback.Control
//{
//    public class PlaceableRectangle : IPlaceableObject
//    {
//        public float Width { get; set; }
//        public float Height { get; set; }

//        public PlaceableRectangle(float width, float height)
//        {
//            this.Width = width;
//            this.Height = height;
//        }


//        public GameObject PlaceIntoLevel(Level level)
//        {
//            var body = BodyFactory.CreateRectangle(level.World, this.Width, this.Height, 1f);
//            var obj = new GameObject {Body = body, Name = "Placed", Type = GameObjectType.Placed};
//            body.UserData = obj;
//            return obj;
//        }
//    }
//}