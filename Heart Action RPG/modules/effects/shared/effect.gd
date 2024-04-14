class_name Effect extends AnimatedSprite2D

func _ready():
	animation_finished.connect(_on_animation_finished)
	play("animate")

func _on_animation_finished():
	queue_free()