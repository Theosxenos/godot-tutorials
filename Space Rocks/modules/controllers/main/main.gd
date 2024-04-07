extends Node

@export var enemy_scene : PackedScene
@export var rock_scene : PackedScene

var screensize = Vector2.ZERO

var level : int = 0
var score : int = 0
var playing : bool = false

func _ready():
	screensize = get_viewport().size

func spawn_rock(size: int, pos = null, vel = null) -> void:
	if pos == null:
		$RockPath/RockSpawn.progress = randi()
		pos = $RockPath/RockSpawn.position
	if vel == null:
		vel = Vector2.RIGHT.rotated(randf_range(0, TAU)) * randf_range(50, 125)
	
	var r : Rock = rock_scene.instantiate() as Rock
	r.screensize = screensize
	r.start(pos, vel, size)
	
	call_deferred("add_child", r)
	
	r.exploded.connect(self._on_rock_exploded)

func _on_rock_exploded(size : int, radius : int, pos : Vector2, vel : Vector2) -> void:
	if size <= 1:
		return
	var player : RigidBody2D = $Player as RigidBody2D
	for offset in [-1,1]:
		var dir = player.position.direction_to(pos).orthogonal() * offset
		var newpos = pos + dir * radius
		var newvel = dir * vel.length() * 1.1
		spawn_rock(size - 1, newpos, newvel)

func new_game():
	# remove any old rocks from previous game
	get_tree().call_group("rocks", "queue_free")
	level = 0
	score = 0

	var hud : Hud = $HUD as Hud
	hud.update_score(score)
	hud.show_message("Get Ready!")
	$Player.reset()
	await hud.get_node("Timer").timeout
	playing = true

func new_level():
	level += 1
	($HUD as Hud).show_message("Wave %s" % level)
	for i in level:
		spawn_rock(3)
	
	$EnemyTimer.start(randf_range(5, 10))

func _process(_delta):
	if not playing:
		return
	if get_tree().get_nodes_in_group("rocks").size() == 0:
		new_level()

func game_over():
	playing = false
	$HUD.game_over()

func _input(event):
	if event.is_action_pressed("pause"):
		if not playing:
			return

		get_tree().paused = not get_tree().paused
		var message : Label = $HUD/VBoxContainer/Message as Label
		if get_tree().paused:
			message.text = "Paused"
			message.show()
		else:
			message.text = ""
			message.hide()

func _on_enemy_timer_timeout():
	var e : Enemy = enemy_scene.instantiate() as Enemy
	add_child(e)
	e.target = $Player
	$EnemyTimer.start(randf_range(20, 40))
