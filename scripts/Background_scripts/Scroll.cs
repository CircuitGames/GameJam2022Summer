using Godot;

public class Scroll: Area2D
{
    [Export]
    float scrollSpeed = 50;

    public override void _Ready()
    {
        Connect("area_entered", this, "OnKillboxBodyEntered");
        Connect("body_entered", this, "OnKillboxBodyEntered");

    }

    public void OnKillboxBodyEntered(PhysicsBody2D body)
    {
        QueueFree();
    }



    public override void _PhysicsProcess(float delta)
    {
        Vector2 location = Vector2.Down;

        Position += (location * scrollSpeed * delta);
    }

}
