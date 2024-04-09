extends Node2D

@export var effect_scene : PackedScene

func create_grass_effect():
	$Sprite2D.hide()
	var grass_effect: GrassEffect = effect_scene.instantiate() as GrassEffect
	add_child(grass_effect)
	await (grass_effect.get_node("AnimatedSprite2D") as AnimatedSprite2D).animation_finished

func _on_hurtbox_area_entered(_area):
	$Hurtbox.set_deferred("monitoring", false)
	await create_grass_effect()
	queue_free()
