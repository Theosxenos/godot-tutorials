extends Control

@onready var heart_ui_empty := $HeartUiEmpty as TextureRect
@onready var heart_ui_full := $HeartUiFull as TextureRect
@onready var heart_width := heart_ui_full.texture.get_width()

func _ready():
	PlayerStats.health_changed.connect(on_health_changed)
	PlayerStats.max_health_changed.connect(on_max_health_changed)

	heart_ui_empty.size.x = heart_width * PlayerStats.max_health
	heart_ui_full.size.x = heart_width * PlayerStats.max_health


func on_health_changed(amount: int):
	heart_ui_full.size.x = heart_width * amount


func on_max_health_changed(amount: int):
	heart_ui_full.size.x = min(heart_width * amount, heart_width * PlayerStats.health)
	heart_ui_empty.size.x = heart_width * amount
