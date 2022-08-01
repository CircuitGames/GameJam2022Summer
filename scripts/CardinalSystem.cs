using System;
using System.Collections.Generic;
using System.Text;
using Godot;

public class CardinalSystem : Node
{
    public enum SceneName
    {
        TITLE = 0,
        NUMBERED_LEVEL
    }

    [Export]
    SceneName m_currentLevel = SceneName.TITLE;
    int m_currentLevelNumber = 0;

    static public CardinalSystem Get( SceneTree currentActiveSceneTree )
    {
        CardinalSystem cardinalSystemNode = currentActiveSceneTree.Root.GetChild<CardinalSystem>( 0 );
        return cardinalSystemNode;
    }

    [Export]
    NodePath m_SceneContainer = new NodePath();
    Node scenes = null;

    [Export]
    List<PackedScene> m_numberedLevels = new List<PackedScene>();

    [Export]
    Dictionary<SceneName, PackedScene> m_sceneMap = new Dictionary<SceneName, PackedScene>()
    {
        {SceneName.TITLE, null}
    };

    public void InitLevel( int levelNumber )
    {
        if( levelNumber < m_numberedLevels.Count )
        {
            m_currentLevelNumber = levelNumber;
            m_currentLevel = SceneName.NUMBERED_LEVEL;
            ActivateLevel( m_numberedLevels[levelNumber].Instance() );
        }
    }
    public void InitLevel( SceneName levelKey )
    {
        PackedScene packedScene;
        if( m_sceneMap.TryGetValue( levelKey, out packedScene ) )
        {
            m_currentLevel = levelKey;
            ActivateLevel( packedScene.Instance() );
        }
    }

    private void ActivateLevel( Node level )
    {
        if( level == null )
        {
            return;
        }

        foreach( Node scene in scenes.GetChildren() )
        {
            scenes.RemoveChild( scene );
            scene.QueueFree();
        }
        scenes.AddChild( level );
    }


    public override void _Ready()
    {
        scenes = GetNode<Node>( m_SceneContainer );
    }

    public override void _Process( float delta )
    {
        if( Input.IsActionJustPressed( "ui_quit" ) )
        {
            GetTree().Quit();
        }
    }
}
