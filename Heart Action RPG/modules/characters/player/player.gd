extends CharacterBody2D

@export var acceleration = 300
@export var max_speed = 120
@export var friction = 400

@onready var animation_player = $AnimationPlayer

func _ready():
	pass

func _physics_process(delta):
	var direction = Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down")
	
	if direction.length() > 0:
		if direction.x > 0:
			animation_player.play("run_right")
		else:
			animation_player.play("run_left")
		
		#velocity += direction * acceleration * delta
		#velocity = velocity.limit_length(max_speed)
		velocity = velocity.move_toward(direction * max_speed, acceleration * delta)
	else:
		velocity = velocity.move_toward(Vector2.ZERO, friction * delta)
	
	#move_and_collide(velocity * delta)
	move_and_slide()
