extends KinematicBody2D

export(float) var movement_speed = 750
export(float) var acceleration = 3000
export(float) var friction = 5000

var velocity = Vector2.ZERO

func _physics_process(delta):
	
	var directional_input = Vector2.ZERO
	
	directional_input.x = Input.get_action_strength("p2_ui_right") - Input.get_action_strength("p2_ui_left")
	directional_input.y = Input.get_action_strength("p2_ui_down") - Input.get_action_strength("p2_ui_up")
	
	if (directional_input != Vector2.ZERO):
		velocity = velocity.move_toward(directional_input * movement_speed, acceleration * delta)
	else:
		velocity = velocity.move_toward(Vector2.ZERO, friction * delta)
	
	move_and_slide(velocity)
