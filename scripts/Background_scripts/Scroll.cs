using Godot;

public class Scroll: KinematicBody2D
{
    [Export]
    float scrollSpeed = 50;



    public override void _PhysicsProcess(float delta)
    {
        Vector2 location = Vector2.Zero;
        location.x = 0;
        location.y = 1;

        MoveAndCollide(location * scrollSpeed * delta);
    }

}
