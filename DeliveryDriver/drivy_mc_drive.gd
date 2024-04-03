extends Area2D


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# Rotate the object
	rotation_degrees += 1

	# Move the object in a circle
	var direction = Vector2(0, -1).rotated(rotation)
	position += direction #* 0.1
