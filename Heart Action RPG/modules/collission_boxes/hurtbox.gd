class_name Hurtbox extends Area2D

@export var invincible: bool = false : set = set_invincibility

const HIT_EFFECT = preload("res://modules/effects/hit/hit_effect.tscn")

func create_hit_effect(position: Vector2 = Vector2.ZERO):
	var effect: Effect = HIT_EFFECT.instantiate() as Effect
	add_child(effect)
	effect.position = position

func set_invincibility(value):
	set_deferred("monitoring", !value)

func start_invincibility(duration: float):
	invincible = true
	$InvincibilityTimer.start(duration)

func _on_invincibility_timer_timeout():
	invincible = false
