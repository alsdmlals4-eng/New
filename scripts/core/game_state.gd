extends Node

var selected_mood: String = "평온"
var companion_affection: int = 1
var photos: Array[String] = []
var sceneries: Array[String] = []
var letters: Array[String] = []


## Selects today's mood before entering the sea scene.
func select_mood(mood: String) -> void:
	selected_mood = mood


## Clears one voyage session while keeping the selected mood.
func reset_session() -> void:
	companion_affection = 1
	photos.clear()
	sceneries.clear()
	letters.clear()


## Adds a photo album entry.
func add_photo(entry: String) -> void:
	photos.append(entry)
	_increase_affection()


## Adds a scenery album entry.
func add_scenery(entry: String) -> void:
	sceneries.append(entry)
	_increase_affection()


## Adds a bottle letter entry.
func add_letter(entry: String) -> void:
	letters.append(entry)
	_increase_affection()


func _increase_affection() -> void:
	var collected_count := photos.size() + sceneries.size() + letters.size()
	companion_affection = clampi(1 + int(collected_count / 2), 1, 3)

