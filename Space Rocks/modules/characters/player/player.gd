extends RigidBody2D

signal lives_changed(value : int)
signal dead

@export var engine_power : int = 500
@export var spin_power : int = 8000

@export var bullet_scene : PackedScene
@export_range(0, 5, 0.05) var fire_rate : float = 0.25

@onready var muzzle : Marker2D = $Muzzle

var can_shoot = true

var thrust: Vector2 = Vector2.ZERO
var rotation_dir: float = 0

enum player_state {INIT, ALIVE, INVUNERABLE, DEAD}
var state : player_state = player_state.INIT
var reset_pos : bool = false
var lives = 0 : set = set_lives

var screensize: Vector2 = Vector2.ZERO

func _ready() -> void:
	change_state(player_state.ALIVE)
	screensize = get_viewport_rect().size
	
	$GunCooldown.wait_time = fire_rate

func change_state(new_state : player_state) -> void:
	match new_state:
		player_state.INIT:
			$CollisionShape2D.set_deferred("disabled", true)
			$Sprite2D.modulate.a = 0.5
		player_state.ALIVE:
			$CollisionShape2D.set_deferred("disabled", false)
			$Sprite2D.modulate.a = 1.0
		player_state.INVUNERABLE:
			$CollisionShape2D.set_deferred("disabled", true)
			$Sprite2D.modulate.a = 0.5
			$InvulnerabilityTimer.start()
		player_state.DEAD:
			$CollisionShape2D.set_deferred("disabled", true)
			$Sprite2D.hide()
			linear_velocity = Vector2.ZERO
			dead.emit()

	state = new_state

func _process(delta: float):
	get_input()

func get_input() -> void:
	thrust = Vector2.ZERO
	
	if state in [player_state.DEAD, player_state.INIT]:
		return
	
	if Input.is_action_pressed("thrust"):
		thrust = transform.x * engine_power
	rotation_dir = Input.get_axis("rotate_left","rotate_right")
	
	if Input.is_action_pressed("shoot") and can_shoot:
		shoot()

func _physics_process(delta: float):
	constant_force = thrust
	constant_torque = rotation_dir * spin_power

func _integrate_forces(physics_state : PhysicsDirectBodyState2D):
	var xform = physics_state.transform
	xform.origin.x = wrapf(xform.origin.x, 0, screensize.x)
	xform.origin.y = wrapf(xform.origin.y, 0, screensize.y)
	physics_state.transform = xform

	if reset_pos:
		physics_state.transform.origin = screensize / 2
		reset_pos = false

func shoot() -> void:
	if state == player_state.INVUNERABLE:
		return

	can_shoot = false
	$GunCooldown.start()
	var bullet: Bullet = bullet_scene.instantiate() as Bullet
	get_tree().root.add_child(bullet)
	bullet.start(muzzle.global_transform)

func _on_gun_cooldown_timeout():
	can_shoot = true

func set_lives(value : int) -> void:
	lives = value
	lives_changed.emit(lives)
	if lives <= 0:
		change_state(player_state.DEAD)
	else:
		change_state(player_state.INVUNERABLE)

func reset() -> void:
	reset_pos = true
	$Sprite2D.show()
	lives = 3
	change_state(player_state.ALIVE)


func _on_invulnerability_timer_timeout():
	change_state(player_state.ALIVE)
