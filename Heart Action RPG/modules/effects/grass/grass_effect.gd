class_name GrassEffect extends Node2D

@onready var animated_sprite = $AnimatedSprite2D

func _ready():
	animated_sprite.play("animate")

func _on_animated_sprite_2d_animation_finished():
	queue_free()
