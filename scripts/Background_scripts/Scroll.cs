using Godot;

public class Scroll: KinematicBody2D
{
    [Export]
    float scrollSpeed = 50;

    Area2D self;


    public override void _Ready()
    {
        self = ((Area2D)GetNode("Area2D"));
        self.Connect("area_entered", this, "OnKillboxBodyEntered");
        self.Connect("body_entered", this, "OnKillboxBodyEntered");

    }

    public void OnKillboxBodyEntered(PhysicsBody2D body)
    {
        QueueFree();
    }



    public override void _PhysicsProcess(float delta)
    {
        Vector2 location = Vector2.Zero;
        location.x = 0;
        location.y = 1;

        MoveAndCollide(location * scrollSpeed * delta);
    }

}
