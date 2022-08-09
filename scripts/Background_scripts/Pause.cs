using Godot;
using System;

public class Pause : Control
{
    bool isPaused=false;

    public override void _Ready()
    {
        this.Visible = false;
        //continue running while paused, without this the game will softlock
        this.PauseMode = Node2D.PauseModeEnum.Process;
    }
    //pause and show screen
    public void onPauseButtonPressed()
    {
        if (isPaused != true && Input.IsActionJustPressed("ui_pause"))
        {
            Input.ActionRelease("ui_pause");
            this.Visible = true;
            isPaused = true;
            this.GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
            SetProcess(true);
            GetTree().Paused = true;
        }
        else if (isPaused != false && Input.IsActionJustPressed("ui_pause"))
        {
            GetTree().Paused = false;
            SetProcess(false);
            this.GetNode<AudioStreamPlayer>("AudioStreamPlayer").Stop();
            isPaused = false;
            this.Visible = false;
        }
    }
    public override void _Process(float delta)
    {
            onPauseButtonPressed();
    }
}
