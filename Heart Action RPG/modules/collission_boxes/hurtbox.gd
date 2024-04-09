class_name Hurtbox extends Area2D

const HIT_EFFECT = preload("res://modules/effects/hit/hit_effect.tscn")

func create_hit_effect(position: Vector2 = Vector2.ZERO):
	var effect: Effect = HIT_EFFECT.instantiate() as Effect
	add_child(effect)
	effect.position = position
