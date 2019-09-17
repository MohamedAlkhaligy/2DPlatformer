using UnityEngine;


public class KeyFrame
{
   public Vector2 Position { get; set; }
   public Vector2 Velocity { get; set; }
   public Vector2 Scale { get; set; }
   public AnimatorStateInfo AnimationState { get; set; }
   public bool IsAlive { get; set; }

    public KeyFrame(Vector2 position, Vector2 velocity, Vector2 scale, bool isAlive) {
        this.Position = position;
        this.Velocity = velocity;
        this.Scale = scale;
        this.IsAlive = isAlive;
    }

}
