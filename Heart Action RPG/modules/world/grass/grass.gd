extends Node2D

const DEATH_SCENE: PackedScene = preload("res://modules/effects/grass/grass_effect.tscn")

func create_grass_effect():
	$Sprite2D.hide()
	var grass_effect: Effect = DEATH_SCENE.instantiate() as Effect
	add_child(grass_effect)
	await grass_effect.animation_finished

func _on_hurtbox_area_entered(_area):
	$Hurtbox.set_deferred("monitoring", false)
	await create_grass_effect()
	queue_free()
