extends CharacterBody2D

# Turning per frame
@export var steer_speed = 100
@export var move_speed = 300

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	
	#var input_direction = Input.get_vector("ui_left","ui_right", "ui_up", "ui_down")
	#var steer_amount = input_direction.x
	#var move_direction = input_direction.y
	
	var steer_amount = Input.get_axis("ui_left", "ui_right")
	var move_direction = Input.get_axis("ui_up", "ui_down")
	
	# Rotate the object
	rotation_degrees += steer_amount * steer_speed * delta

	var x = transform.x
	var test: float = transform.x * move_direction * move_speed * delta
	# Move the object in a circle
	var direction = Vector2(0, test).rotated(rotation)
	velocity = direction #* 0.1
	move_and_slide()
