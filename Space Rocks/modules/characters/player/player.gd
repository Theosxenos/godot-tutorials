extends RigidBody2D

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

var screensize: Vector2 = Vector2.ZERO

func _ready() -> void:
	change_state(player_state.ALIVE)
	screensize = get_viewport_rect().size
	
	$GunCooldown.wait_time = fire_rate

func change_state(new_state : player_state) -> void:
	match new_state:
		player_state.INIT:
			$CollisionShape2D.set_deferred("disabled", true)
		player_state.ALIVE:
			$CollisionShape2D.set_deferred("disabled", false)
		player_state.INVUNERABLE:
			$CollisionShape2D.set_deferred("disabled", true)
		player_state.DEAD:
			$CollisionShape2D.set_deferred("disabled", true)

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

func _integrate_forces(physics_state):
	var xform = physics_state.transform
	xform.origin.x = wrapf(xform.origin.x, 0, screensize.x)
	xform.origin.y = wrapf(xform.origin.y, 0, screensize.y)
	physics_state.transform = xform

func shoot():
	if state == player_state.INVUNERABLE:
		return

	can_shoot = false
	$GunCooldown.start()
	var bullet: Bullet = bullet_scene.instantiate() as Bullet
	get_tree().root.add_child(bullet)
	bullet.start(muzzle.global_transform)

func _on_gun_cooldown_timeout():
	can_shoot = true
