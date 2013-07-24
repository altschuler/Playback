namespace Playback.Data.Definition
{
    public class FixtureDefinition
    {
        public float Restitution { get; set; }
        public float Density { get; set; }
        public float Friction { get; set; }
        public string Name { get; set; }
        public PolygonDefinition Polygon { get; set; }
        public CircleDefinition Circle { get; set; }

        public static FixtureDefinition Parse(dynamic model)
        {
            var def = new FixtureDefinition();
            def.Density = model.density;
            def.Friction = model.friction;
            def.Name = model.name;

            if (model.restitution != null)
                def.Restitution = model.restitution;

            if (model.polygon != null)
                def.Polygon = PolygonDefinition.Parse(model.polygon);

            if (model.circle != null)
                def.Circle = CircleDefinition.Parse(model.circle);

            return def;
        }
    }
}