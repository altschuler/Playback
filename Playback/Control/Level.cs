using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Playback.Data.Definition;
using Playback.Helpers;
using Playback.Logic;

namespace Playback.Control
{
    public class Level
    {
        public World World { get; set; }

        private List<GameObject> GameObjects { get; set; }
        private List<GameObject> PlacedObjects { get; set; }
        private List<TriggerObject> TriggerObjects { get; set; }
        private List<CollectibleObject> CollectibleObjects { get; set; }

        private List<KeyValuePair<GameObject, GameObject>> PlacedObjectCollisions { get; set; }

        public Level()
        {
            this.GameObjects = new List<GameObject>();
            this.CollectibleObjects = new List<CollectibleObject>();
            this.TriggerObjects = new List<TriggerObject>();
            this.PlacedObjects = new List<GameObject>();

            this.PlacedObjectCollisions = new List<KeyValuePair<GameObject, GameObject>>();
        }

        public void Initialize()
        {
            foreach (var collectible in this.CollectibleObjects)
            {
                collectible.Body.OnCollision += this.OnCollectibleCollision;
            }

            foreach (var trigger in this.TriggerObjects)
            {
                trigger.Body.OnCollision += this.OnTriggerCollision;
            }
        }

        private bool OnTriggerCollision(Fixture fixturea, Fixture fixtureb, Contact contact)
        {
            var trigger = (TriggerObject)(fixturea.Body.UserData is TriggerObject
                                                       ? fixturea.Body.UserData
                                                       : fixtureb.Body.UserData);
            trigger.Consequence.Invoke();

            return true;
        }

        private bool OnCollectibleCollision(Fixture fixturea, Fixture fixtureb, Contact contact)
        {
            var collectible = (CollectibleObject)(fixturea.Body.UserData is CollectibleObject
                                                       ? fixturea.Body.UserData
                                                       : fixtureb.Body.UserData);

            collectible.Reward.Invoke();

            this.World.RemoveBody(collectible.Body);

            this.CollectibleObjects.Remove(collectible);

            return false;
        }

        public void StepForward(GameTime gameTime)
        {
            // record state of objects
            foreach (var gameObject in this.GameObjects.Where(o => o is MovingObject))
            {
                var state = HistoryHelper.CreateState(gameObject, gameTime);
                var movingObject = (MovingObject)gameObject;
                movingObject.History.Add(state);
            }

            // step the physics world
            this.World.Step((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void StepBackward(GameTime gameTime)
        {
            var blocked = false;
            foreach (var gameObject in this.GameObjects.Where(o => o is MovingObject))
            {
                var movingObject = (MovingObject)gameObject;
                var contact = movingObject.Body.ContactList;

                while (contact != null)
                {
                    // TODO accurate collision detection here
                    var otherGo = (GameObject)contact.Other.UserData;
                    if (otherGo.Type == GameObjectType.Placed) //&& contact.Contact.IsTouching())
                    {
                        if (!this.PlacedObjectHasCollidedWith(otherGo, movingObject))
                            blocked = true;
                    }

                    contact = contact.Next;
                }
            }

            if (blocked)
                return;

            foreach (var gameObject in this.GameObjects.Where(o => o is MovingObject))
            {
                var movingObject = (MovingObject)gameObject;
                var lastMoveState = movingObject.PopLastHistoryState();
                movingObject.ApplyState(lastMoveState);
            }
        }

        private bool PlacedObjectHasCollidedWith(GameObject placedObject, GameObject movingObject)
        {
            return this.PlacedObjectCollisions.Any(kvp => (kvp.Key == placedObject && kvp.Value == movingObject)
                                                          || (kvp.Key == movingObject && kvp.Value == placedObject));
        }

        public void PlaceGameObject(GameObject placeable)
        {
            placeable.Body.OnCollision += this.OnPlacedObjectCollision;

            this.GameObjects.Add(placeable);
            this.PlacedObjects.Add(placeable);
        }

        private bool OnPlacedObjectCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            var goA = (GameObject)fixtureA.Body.UserData;
            var goB = (GameObject)fixtureB.Body.UserData;

            var placedObject = goA.Type == GameObjectType.Placed ? goA : goB;
            var movingObject = placedObject == goA ? goB : goA;

            if (!this.PlacedObjectHasCollidedWith(placedObject, movingObject))
                this.PlacedObjectCollisions.Add(new KeyValuePair<GameObject, GameObject>(placedObject, movingObject));

            return true;
        }

        // TODO move out of level
        public static Level ParseWorldDefinition(WorldDefinition worldDef)
        {
            var level = new Level();

            level.World = new World(worldDef.Gravity * -Vector2.UnitY);

            foreach (var bodyDef in worldDef.Bodies)
            {
                // TODO implement all properties of the defs
                var body = new Body(level.World);
                body.Position = bodyDef.Position;
                body.BodyType = bodyDef.Type;
                
                foreach (var fixtureDef in bodyDef.Fixtures)
                {
                    if (fixtureDef.Circle != null)
                    {
                        FixtureFactory.AttachCircle(fixtureDef.Circle.Radius, fixtureDef.Density, body, fixtureDef.Circle.Center);
                    }
                    else
                    {
                        FixtureFactory.AttachPolygon(new Vertices(fixtureDef.Polygon.Vertices.Select(v => new Vector2(v.X, v.Y)).ToArray()), fixtureDef.Density, body);
                    }
                }

                GameObject gameObject;
                if (body.BodyType == BodyType.Dynamic)
                    gameObject = new MovingObject { Body = body };
                else
                    gameObject = new GameObject { Body = body };
                gameObject.Role = bodyDef.Role;
                
                body.UserData = gameObject;

                level.AddGameObject(gameObject);
            }

            return level;
        }

        private void AddGameObject(GameObject gameObject)
        {
            this.GameObjects.Add(gameObject);
        }
    }
}