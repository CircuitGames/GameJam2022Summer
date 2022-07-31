extends KinematicBody2D

export(float) var scroll_speed = 50

func _physics_process(delta):
	
	var location = Vector2.ZERO
	
	location.x = 0
	location.y = 1
	
	
	move_and_slide(location * scroll_speed)
	
