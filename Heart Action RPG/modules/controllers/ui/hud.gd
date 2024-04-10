extends CanvasLayer

const HEART_IMAGES: Dictionary = {
	"empty": preload("res://assets/UI/HeartUIEmpty.png"),
	"full": preload("res://assets/UI/HeartUIFull.png")
}

@onready var live_counter = $LiveCounter as HBoxContainer

func _ready():
	PlayerStats.health_changed.connect(set_player_lives)
	PlayerStats.max_health_changed.connect(on_max_health_changed)
	update_ui(PlayerStats.max_health, PlayerStats.health)

func update_ui(max_health: int, current_health: int):
	for c in live_counter.get_children():
		c.free()
	var hearts = create_hearts(max_health) # Create max number of hearts
	for heart in hearts:
		live_counter.add_child(heart)
	set_player_lives(current_health) # Update to current health

func set_player_lives(amount: int):
	var children = live_counter.get_children()
	var children_count = live_counter.get_child_count()
	for i in children_count:
		var heart = children[i] as TextureRect
		heart.texture = HEART_IMAGES["full"] if i < amount else HEART_IMAGES["empty"]

func on_max_health_changed(value):
	update_ui(value, PlayerStats.health)

func create_hearts(amount: int) -> Array:
	var hearts = []
	for i in range(amount):
		var heart = TextureRect.new()
		heart.texture = HEART_IMAGES["full"]  # Default to full, adjustment happens in set_player_lives
		hearts.append(heart)
	return hearts
