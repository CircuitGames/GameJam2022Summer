using System;
using System.Collections.Generic;
using System.Text;
using Godot;

public class CardinalSystem : Node2D
{
    [Export]
    List<string> m_levelNames = new List<string>();
    int m_nextLevelId = 0;
    StringBuilder m_stringBuilder = new StringBuilder( 64 );
    [Export]
    bool isOnTitle = false;

    PackedScene m_nextLevel = null;
    private void StartLoadingNextLevel ()
    {
        if ( m_nextLevelId < m_levelNames.Count )
        {
            m_stringBuilder.Clear();
            m_stringBuilder.Append( "res://Assets/Scenes/" );
            m_stringBuilder.Append( m_levelNames[m_nextLevelId] );
            m_stringBuilder.Append( ".tscn" );
            m_nextLevelId++;
            m_nextLevel = GD.Load<PackedScene>( m_stringBuilder.ToString() );
        }
    }
    private void InitNextLevel ()
    {
        if ( m_nextLevel != null )
        {
            GetTree().ChangeSceneTo( m_nextLevel );
        }
        StartLoadingNextLevel();
    }

    public override void _Ready ()
    {
        StartLoadingNextLevel();
    }

    public override void _Process ( float delta )
    {
        if ( isOnTitle && Input.IsActionJustPressed( "ui_accept" ) )
        {
            isOnTitle = false;
            InitNextLevel();
        }
        if ( Input.IsActionJustPressed( "ui_quit" ) )
        {
            GetTree().Quit();
        }
    }
}
