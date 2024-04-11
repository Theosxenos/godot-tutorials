extends CharacterBody2D

enum bat_state {
	IDLE,
	WANDER,
	CHASE
}

const DEATH_SCENE: PackedScene = preload("res://modules/effects/enemy/enemy_death_effect.tscn")

@export var knockback_friction = 200
@export var knockback_force = 110

@export var movement_friction = 200
@export var acceleration = 300
@export var max_speed = 50

@export var stop_distance: float = 2 # 2 == Vector2.ONE.length_squared()

var state: bat_state = bat_state.CHASE as bat_state

@onready var stats = $Stats as Stats
@onready var player_detection_zone: PlayerDetectionZone = $PlayerDetectionZone as PlayerDetectionZone
@onready var animated_sprite: AnimatedSprite2D = $AnimatedSprite

func _ready():
	var frames: SpriteFrames = animated_sprite.sprite_frames
	randomize()
	animated_sprite.frame = randi_range(0, frames.get_frame_count("fly"))


func _physics_process(delta):
	velocity = velocity.move_toward(Vector2.ZERO, knockback_friction * delta)
	move_and_slide()

	match state:
		bat_state.IDLE:
			velocity = get_real_velocity()
			velocity = velocity.move_toward(Vector2.ZERO, movement_friction * delta)
			seek_player()
		bat_state.WANDER:
			pass
		bat_state.CHASE:
			var player = player_detection_zone.player
			if player != null :
				var direction: Vector2 = player.global_position - global_position
				#var direction: Vector2 = position.direction_to(player.global_position)
				if direction.length_squared() > stop_distance:
					velocity = velocity.move_toward(direction.normalized() * max_speed, acceleration * delta)
					animated_sprite.flip_h = velocity.x < 0
				else:
					velocity = Vector2.ZERO
			else:
				state = bat_state.IDLE
			
	move_and_slide()


func seek_player():
	if player_detection_zone.can_see_player():
		state = bat_state.CHASE


func create_hit_effect():
	if stats.health > 0:
		($Hurtbox as Hurtbox).create_hit_effect()


func create_effect():
	$AnimatedSprite.hide()
	var death_instance = DEATH_SCENE.instantiate() as Effect
	add_child(death_instance)
	await death_instance.animation_finished


func _on_hurtbox_area_entered(area: Area2D):
	#velocity = area.knockback_vector * knockback_force
	stats.health -= (area as Hitbox).damage
	var knockback_direction = (position - area.owner.position) as Vector2
	velocity = knockback_direction.normalized() * knockback_force
	create_hit_effect()


func _on_stats_no_health():
	await create_effect()	
	queue_free()
