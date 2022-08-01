
using System;
using System.Collections.Generic;
using System.Text;
using Godot;

public class TitleScreen : Node
{
    public override void _Process( float delta )
    {
        GD.Print( "Title Running..." );
        if( Input.IsActionJustPressed( "ui_accept" ) )
        {
            GD.Print( "Title CHANGING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" );
            CardinalSystem.Get( GetTree() ).InitLevel( 0 );
        }
    }
}