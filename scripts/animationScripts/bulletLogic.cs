using Godot;
using System;

public class bulletLogic : Node2D
{
    [Export]
    float speed = 1000;
	private AnimatedSprite sprite;

    Area2D self;


    public override void _Ready()
    {
        initiateAnimations();
        self = ((Area2D)GetNode("Area2D"));
        self.Connect("area_entered", this, "OnBulletBodyEntered");
        self.Connect("body_entered", this, "OnBulletBodyEntered");

    }

    public override void _Process( float delta )
    {
        float moveAmount = speed * delta;
        //move the object straight up
        Position  -= Transform.y.Normalized() * moveAmount;
    }

    //delete the bullet upon collision with a bounding box or player
    public void OnBulletBodyEntered(PhysicsBody2D body)
    {
        QueueFree();
    }

    public void initiateAnimations()
    {
        //finds the child sprite of this node
        sprite = ((AnimatedSprite)GetNode("AnimatedSprite"));
        //instantiates idle animation loop
		sprite.Play("default");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//
//  }
}
