extends CharacterBody2D

@export var acceleration = 300
@export var max_speed = 120
@export var roll_speed = 125
@export_range(0.1,1,0.1) var roll_slide_speed_modifier = 0.8
@export var friction = 400

@onready var animation_player = $AnimationPlayer
@onready var animation_tree: AnimationTree = $AnimationTree as AnimationTree
@onready var animation_state = animation_tree.get("parameters/playback")

#@onready var sword_hitbox = $SwordPivot/SwordHitbox

enum player_state {
	MOVE,
	ROLL,
	ATTACK
}

var state: player_state = player_state.MOVE

var roll_vector: Vector2 = Vector2.DOWN

func _ready():
	#sword_hitbox.knockback_vector = roll_vector
	pass

func _physics_process(delta):
	match state:
		player_state.MOVE:
			move_state(delta)
		player_state.ROLL:
			roll_state()
		player_state.ATTACK:
			attack_state(delta)

func move_state(delta):
	var direction: Vector2 = Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down")
	
	if direction.length() > 0:
		#sword_hitbox.knockback_vector = direction
		roll_vector = direction
		animation_tree.set("parameters/Attack/blend_position", direction)
		animation_tree.set("parameters/Idle/blend_position", direction)
		animation_tree.set("parameters/Run/blend_position", direction)
		animation_tree.set("parameters/Roll/blend_position", direction)
		animation_state.travel("Run")

		#velocity += direction * acceleration * delta
		#velocity = velocity.limit_length(max_speed)
		velocity = velocity.move_toward(direction * max_speed, acceleration * delta)
	else:
		velocity = velocity.move_toward(Vector2.ZERO, friction * delta)
		animation_state.travel("Idle")

	#move_and_collide(velocity * delta)
	move_and_slide()
	
	if Input.is_action_just_pressed("attack"):
		state = player_state.ATTACK
	elif Input.is_action_just_pressed("roll"):
		state = player_state.ROLL

func attack_state(_delta):
	velocity = Vector2.ZERO
	animation_state.travel("Attack")

func attack_animation_finished():
	state = player_state.MOVE

func roll_state():
	velocity = roll_vector * roll_speed
	animation_state.travel("Roll")
	move_and_slide()	

func roll_animation_finished():
	#velocity = Vector2.ZERO
	velocity = velocity * roll_slide_speed_modifier
	state = player_state.MOVE
