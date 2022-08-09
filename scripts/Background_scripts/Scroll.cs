using Godot;

public class Scroll: Area2D
{
    [Export]
    float scrollSpeed = 500;
    Node2D spawnSpot = null;
    Vector2 offset = Vector2.Zero;

    public override void _Ready()
    {
        Connect("area_entered", this, "OnKillboxBodyEntered");
        Connect("body_entered", this, "OnKillboxBodyEntered");
        spawnSpot = (Node2D) GetParent();
        offset.x = 0;
        offset.y = 3240;
    }

    public void OnKillboxBodyEntered(PhysicsBody2D body)
    {
        Position = spawnSpot.Position + offset;
    }


    public override void _PhysicsProcess(float delta)
    {
        Vector2 location = Vector2.Down;

        Position += (location * scrollSpeed * delta);
    }

}
