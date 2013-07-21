using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Playback.Data.Definition
{
    public class PolygonDefinition
    {
        public List<VertexDefinition> Vertices;

        public PolygonDefinition()
        {
            this.Vertices = new List<VertexDefinition>();
        }
        public static PolygonDefinition Parse(dynamic model)
        {
            var def = new PolygonDefinition();

            var xlist = (JArray)model.vertices.x;
            for (var i = 0; i < xlist.Count; i++)
            {
                var vertex = new VertexDefinition { X = (float)model.vertices.x[i], Y = (float)model.vertices.y[i] };
                def.Vertices.Add(vertex);
            }

            return def;
        }
    }
}